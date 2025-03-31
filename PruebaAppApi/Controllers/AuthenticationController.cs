using AplicationServices.Application.Contracts.Authentication;
using AplicationServices.DTOs.Authentication;
using AplicationServices.DTOs.Generics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace PruebaAppApi.Controllers
{
    [ApiController]
    [Route("Api/Authentication")]
    public class AuthenticationController : ControllerBase
    {
        /// <summary>
        /// Instancia al servicio de aplicación
        /// </summary>
        private readonly IAuthenticationAppServices _AuthenticationServices;
        private readonly IMapper _mapper;

        public AuthenticationController(IAuthenticationAppServices authenticationServices, IMapper mapper)
        {

            _AuthenticationServices = authenticationServices;
            _mapper = mapper;
        }

        [HttpPost("login", Name = "loginUsuario")]
        public async Task<RequestResult<LoginResponseDto>> Login(LoginRequestDto loginRequestDto)
        {
            return await _AuthenticationServices.Login(loginRequestDto);
        }
    }
}
