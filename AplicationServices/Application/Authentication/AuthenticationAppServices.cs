using AplicationServices.Application.Contracts.Authentication;
using AplicationServices.DTOs.Authentication;
using AplicationServices.DTOs.Generics;
using AplicationServices.Helpers.HashResource;
using AplicationServices.Helpers.TextResorce;
using AutoMapper;
using DomainServices.Domain.Contracts.User;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PruebaAppApi.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AplicationServices.Application.Authentication
{
    public class AuthenticationAppServices : IAuthenticationAppServices
    {
        /// <summary>
        /// Instancia al servicio de Dominio
        /// </summary>
        private readonly IUserDomain _userDomain;

        /// <summary>
        /// Mapper
        /// </summary>
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly Hash _hash;

        public AuthenticationAppServices(IUserDomain userDomain, IConfiguration configuration, IMapper mapper)
        {
            _userDomain = userDomain;
            _configuration = configuration;
            _mapper = mapper;
            _hash = new Hash();
        }

        public async Task<RequestResult<LoginResponseDto>> Login(LoginRequestDto loginRequestDto)
        {
            try
            {
                LoginResponseDto respuestaAutenticacionDto = new LoginResponseDto();

                #region Validaciones
                List<string> errorMessageValidations = new List<string>();
                LoginValidations(ref errorMessageValidations, loginRequestDto);
                if (errorMessageValidations.Any())
                    return RequestResult<LoginResponseDto>.CreateUnsuccessful(null, errorMessageValidations);


                var user = await _userDomain.GetUser(loginRequestDto.User);
                if (user == null)
                {
                    errorMessageValidations.Add(ResourceUserMsm.CredentialsInvalidate);
                    return RequestResult<LoginResponseDto>.CreateUnsuccessful(null, errorMessageValidations);
                }
                #endregion
                /*Construccion de token*/
                respuestaAutenticacionDto = ConstruirToken(loginRequestDto, user);
                /*comparacion de hash*/
                bool isHash = _hash.GetHash(loginRequestDto.Password, Convert.FromBase64String(user.Salt)).Hash.Equals(user.Password);
                if (!isHash)
                {
                    return RequestResult<LoginResponseDto>.CreateUnsuccessful(null, new string[] { ResourceUserMsm.CredentialsInvalidate });
                }

                return RequestResult<LoginResponseDto>.CreateSuccessful(respuestaAutenticacionDto);

            }
            catch (Exception ex)
            {
                return RequestResult<LoginResponseDto>.CreateError(ex.Message);

            }
        }

        #region Private Methods

        /// <summary>
        ///     valida los datos para crear un usuario.
        /// </summary>
        /// <author>Ariel Bejarano</author>
        /// <param name="userDto">objeto para guardar orden de trabajo</param>
        private void LoginValidations(ref List<string> errorMessageValidations, LoginRequestDto loginRequestDto)
        {
            if (string.IsNullOrEmpty(loginRequestDto.User))
            {
                errorMessageValidations.Add(ResourceUserMsm.NotExistUser);
            }
            if (string.IsNullOrEmpty(loginRequestDto.Password))
            {
                errorMessageValidations.Add(ResourceUserMsm.PasswordNotExist);
            }
        }

        private LoginResponseDto ConstruirToken(LoginRequestDto loginRequestDto, User User)
        {
            var claims = new List<Claim>()
            {
                new Claim("user", loginRequestDto.User??""),
                new Claim("id", User.ID.ToString())
            };

            //var usuario = await userManager.FindByEmailAsync(credencialesUsuario.Email);
            //var claimsDB = await userManager.GetClaimsAsync(usuario);

            //claims.AddRange(claimsDB);

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["KeyJwt"]));
            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddDays(1);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expiracion, signingCredentials: creds);

            return new LoginResponseDto()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                UserID = User.ID,
                NameUser = string.Concat(User.FirstName ?? "", " ", User.LastName ?? ""),
            };
        }

        #endregion
    }
}
