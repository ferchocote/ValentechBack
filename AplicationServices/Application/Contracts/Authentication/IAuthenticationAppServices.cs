using AplicationServices.DTOs.Authentication;
using AplicationServices.DTOs.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationServices.Application.Contracts.Authentication
{
    public interface IAuthenticationAppServices
    {
        Task<RequestResult<LoginResponseDto>> Login(LoginRequestDto loginRequestDto);
    }
}
