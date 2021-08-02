using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Blazored.TextEditor;
using Business.Repository.IRepository;
using HiddenVilla_Server.Helper;
using HiddenVilla_Server.Service.IService;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Models;

namespace HiddenVilla_Server.Pages.HotelRoom
{
    public partial class HotelRoomUpsert : ComponentBase
    {
        public HotelRoomUpsert()
        {
        }
        
        [Inject]
        IHotelRoomRepository HotelRoomRepository { get; set; }
        [Inject]
        NavigationManager NavigationManager { get; set; }
        [Inject]
        IJSRuntime JsRuntime { get; set; }
        [Inject]
        IFileUpload FileUpload { get; set; }

        [Parameter]
        public int? Id { get; set; }

        private HotelRoomDTO HotelRoomModel { get; set; } = new HotelRoomDTO();
        private string Title { get; set; } = "Create";
        public HotelRoomImageDTO RoomImage { get; set; } = new HotelRoomImageDTO();
        public List<string> DeletedImageNames { get; set; } = new List<string>();
        public bool IsImageUploadProcessStarted { get; set; } = false;
        public BlazoredTextEditor QuillHtml { get; set; } = new BlazoredTextEditor();
        public IEnumerable<HotelRoomDTO> HotelRooms { get; set; } = new List<HotelRoomDTO>();

        [CascadingParameter]
        public Task<AuthenticationState> AuthenticationState { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authenticationState = await AuthenticationState;

            if (!authenticationState.User.IsInRole(Common.SD.Role_Admin))
            {
                var uri = new Uri(NavigationManager.Uri);
                NavigationManager.NavigateTo($"/identity/account/login?returnUrl={uri.LocalPath}");
            }

            if (Id != null)
            {
                Title = "Update";
                HotelRoomModel = await HotelRoomRepository.GetHotelRoom(Id.Value);

                if (HotelRoomModel?.HotelRoomImages != null)
                {
                    HotelRoomModel.ImageUrls = HotelRoomModel.HotelRoomImages.Select(x => x.RoomImageUrl).ToList();
                }
            }
            else
            {
                Title = "Create";
                HotelRoomModel = new HotelRoomDTO();
                HotelRoomModel.HotelRoomImages = new List<HotelRoomImageDTO>(); 
            }
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
                return;

            bool loading = true;

            while (loading)
            {
                try
                {
                    if (!string.IsNullOrEmpty(HotelRoomModel.Details))
                    {
                        await this.QuillHtml.LoadHTMLContent(HotelRoomModel.Details);
                        HotelRoomModel.Details = await this.QuillHtml.GetHTML();
                        await InvokeAsync(StateHasChanged);
                    }
                    loading = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    await Task.Delay(100);
                    loading = true;
                }
            }
        }

        private async Task HandleHotelRoomUpsert()
        {
            try
            {
                var roomDetailsByName = await HotelRoomRepository.IsRoomUnique(HotelRoomModel.Name, HotelRoomModel.Id);
                List<HotelRoomImageDTO> roomImageDTOs = new List<HotelRoomImageDTO>();

                if (roomDetailsByName != null)
                {
                    await JsRuntime.ToastrError("Room name already exists.");
                    return;
                }

                if (HotelRoomModel.Id != 0 && Title == "Update")
                {
                    HotelRoomModel.Details = await this.QuillHtml.GetHTML();

                    if (DeletedImageNames != null && DeletedImageNames.Any())
                    {
                        foreach (var deletedImageName in DeletedImageNames)
                        {
                            var imageName = deletedImageName.Replace($"{NavigationManager.BaseUri}RoomImages/", "");
                            HotelRoomImageDTO imageDTO = HotelRoomModel.HotelRoomImages.Single(x => x.RoomImageUrl == imageName);
                            FileUpload.DeleteFile(imageName);
                            HotelRoomModel.HotelRoomImages.Remove(imageDTO);
                        }
                    }

                    if (HotelRoomModel.ImageUrls != null && HotelRoomModel.ImageUrls.Any())
                    {
                        AddHotelRoomImages(HotelRoomModel);
                    }


                    var updateRoomResult = await HotelRoomRepository.UpdateHotelRoom(HotelRoomModel);

                    await JsRuntime.ToastrSuccess("Hotel room updated successfully.");

                }
                else
                {
                    HotelRoomModel.Details = await this.QuillHtml.GetHTML();
                    HotelRoomModel = AddHotelRoomImages(HotelRoomModel);
                    var createdResult = await HotelRoomRepository.CreateHotelRoom(HotelRoomModel);
                    await JsRuntime.ToastrSuccess("Hotel room created successfully.");

                }
            }
            catch (Exception ex)
            {

            }
            
            NavigationManager.NavigateTo("hotel-room");
        }

        private async Task HandleImageUpload(InputFileChangeEventArgs e)
        {
            IsImageUploadProcessStarted = true;
            try
            {
                var images = new List<string>();

                if (e.GetMultipleFiles().Count > 0)
                {
                    foreach (var file in e.GetMultipleFiles())
                    {
                        FileInfo fileInfo = new FileInfo(file.Name);

                        if (fileInfo.Extension.ToLower() == ".jpg" ||
                            fileInfo.Extension.ToLower() == ".png" ||
                            fileInfo.Extension.ToLower() == ".jpeg")
                        {
                            var uploadedImagePath = await FileUpload.UploadFile(file);
                            images.Add(uploadedImagePath);
                        }
                        else
                        {
                            await JsRuntime.ToastrError("Please select .jpg/.jpeg/.png files only.");
                            return;
                        }

                    }

                    if (images.Any())
                    {
                        if (HotelRoomModel.ImageUrls != null && HotelRoomModel.ImageUrls.Any())
                        {
                            HotelRoomModel.ImageUrls.AddRange(images);
                        }
                        else
                        {
                            HotelRoomModel.ImageUrls = new List<string>();
                            HotelRoomModel.ImageUrls.AddRange(images);
                        }
                    }
                    else
                    {
                        await JsRuntime.ToastrError("Image uploading failed");
                        return;
                    }
                }
                IsImageUploadProcessStarted = false;
            }
            catch (Exception ex)
            {
                await JsRuntime.ToastrError(ex.Message);
            }
        }

        private HotelRoomDTO AddHotelRoomImages(HotelRoomDTO roomDetails)
        {
            foreach (var imageUrl in HotelRoomModel.ImageUrls)
            {
                if (HotelRoomModel.HotelRoomImages == null || HotelRoomModel.HotelRoomImages.Where(x => x.RoomImageUrl == imageUrl).Count() == 0)
                {
                    roomDetails.HotelRoomImages.Add(new HotelRoomImageDTO()
                        {
                            RoomID = roomDetails.Id,
                            RoomImageUrl = imageUrl,

                        }
                    );
                }
            }

            return roomDetails;
        }

        internal async Task DeletePhoto(string imageUrl)
        {
            try
            {
                var imageIndex = HotelRoomModel.ImageUrls.FindIndex(x => x == imageUrl);
                var imageName = imageUrl.Replace($"{NavigationManager.BaseUri}RoomImages/", "");

                if (HotelRoomModel.Id == 0 && Title == "Create")
                {
                    var result = FileUpload.DeleteFile(imageName);
                }
                else
                {
                    DeletedImageNames ??= new List<string>();
                    DeletedImageNames.Add(imageUrl);
                }

                HotelRoomModel.ImageUrls.RemoveAt(imageIndex);
            }
            catch (Exception ex)
            {
                await JsRuntime.ToastrError(ex.Message);
            }
        }
    }
    
}
