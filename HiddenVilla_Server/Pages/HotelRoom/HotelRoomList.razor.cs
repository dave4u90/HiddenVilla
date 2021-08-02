using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Repository.IRepository;
using HiddenVilla_Server.Helper;
using HiddenVilla_Server.Service.IService;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Models;

namespace HiddenVilla_Server.Pages.HotelRoom
{
    public partial class HotelRoomList
    {
        public HotelRoomList()
        {
        }

        [Inject]
        IHotelRoomRepository HotelRoomRepository { get; set; }
        [Inject]
        IJSRuntime JSRuntime { get; set; }
        [Inject]
        IFileUpload FileUpload { get; set; }
        [Inject]
        NavigationManager NavigationManager { get; set; }

        private IEnumerable<HotelRoomDTO> HotelRooms { get; set; } = new List<HotelRoomDTO>();
        public int? DeleteRoomID { get; set; } = null;
        public bool IsProcessing { get; set; } = false;

        [CascadingParameter]
        public Task<AuthenticationState> AuthenticationState { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var authenticationState = await AuthenticationState;

                if (!authenticationState.User.IsInRole(Common.SD.Role_Admin))
                {
                    var uri = new Uri(NavigationManager.Uri);
                    NavigationManager.NavigateTo($"/identity/account/login?returnUrl={uri.LocalPath}");
                }

                HotelRooms = await HotelRoomRepository.GetAllHotelRooms();
            }
            catch(Exception e)
            {

            }
        }

        private async Task HandleRoomDelete(int roomId)
        {
            DeleteRoomID = roomId;
            await JSRuntime.InvokeVoidAsync("ShowDeleteConfirmationModal");
        }

        public async Task ConfirmDelete_Click(bool isConfirmed)
        {
            IsProcessing = true;

            if (isConfirmed && DeleteRoomID != null)
            {
                HotelRoomDTO HotelRoom = await HotelRoomRepository.GetHotelRoom(DeleteRoomID.Value);

                foreach (var image in HotelRoom.HotelRoomImages)
                {
                    var imageName = image.RoomImageUrl.Replace($"{NavigationManager.BaseUri}RoomImages/", "");
                    FileUpload.DeleteFile(imageName);
                }

                await HotelRoomRepository.DeleteHotelRoom(DeleteRoomID.Value);
                await JSRuntime.ToastrSuccess("Hotel room deleted successfully.");
                HotelRooms = await HotelRoomRepository.GetAllHotelRooms();
            }

            await JSRuntime.InvokeVoidAsync("HideDeleteConfirmationModal");
            IsProcessing = false;
        }
    }
}
