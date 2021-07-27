using System;
using System.Threading.Tasks;
using Models;

namespace HiddenVilla_Client.Service.IService
{
    public interface IAuthenticationService
    {
        Task<RegistrationResponseDTO> RegisterUser(UserRequestDTO userRequestDTO);
        Task<AuthenticationResponseDTO> Login(AuthenticationDTO authenticationDTO);
        Task Logout();

    }
}
