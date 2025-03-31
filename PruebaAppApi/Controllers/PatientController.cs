using AplicationServices.Application.Contracts.Patient;
using AplicationServices.DTOs.Generics;
using AplicationServices.DTOs.Patient;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PruebaAppApi.Controllers
{
    [ApiController]
    [Route("Api/Patient")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PatientController : ControllerBase
    {
        private readonly IPatientAppServices _patientAppServices;

        public PatientController(IPatientAppServices patientAppServices) 
        {
            _patientAppServices = patientAppServices;
        }

        /// <summary>
        /// Obtiene todos los pacientes del sistema
        /// </summary>
        /// <param name="GetAllPatientDto"></param>
        /// <returns></returns>
        /// <author>Diego Molina</author>
        [HttpGet("GetAllPatient")]
        public async Task<RequestResult<List<GetAllPatientDto>>> Get(string? name, string? Disease)
        {
            return await _patientAppServices.GetAllPatient(name, Disease);
        }

        /// <summary>
        /// Guarda la informacion de un paciente
        /// </summary>
        /// <param name="SavePatient"></param>
        /// <returns></returns>
        /// <author>Diego Molina</author>
        [HttpPost("SavePatient")]
        public async Task<RequestResult<SavePatientDto>> SavePatient(SavePatientDto patientDto)
        {
            return await _patientAppServices.SavePatient(patientDto);
        }
    }
}
