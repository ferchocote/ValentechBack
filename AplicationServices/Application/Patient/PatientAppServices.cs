using AplicationServices.Application.Contracts.Patient;
using AplicationServices.DTOs.Generics;
using AplicationServices.DTOs.Patient;
using AplicationServices.Helpers.HashResource;
using AplicationServices.Helpers.TextResorce;
using AutoMapper;
using DomainServices.Domain.Contracts.Patient;
using PruebaAppApi.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationServices.Application.Patient
{
    public class PatientAppServices : IPatientAppServices
    {
        /// <summary>
        /// Instancia al servicio de Dominio
        /// </summary>
        private readonly IPatientDomain _patientDomain;
        /// <summary>
        /// Mapper
        /// </summary>
        private readonly IMapper _mapper;
        public PatientAppServices(IPatientDomain patientDomain, IMapper mapper)
        {
            _patientDomain = patientDomain;
            _mapper = mapper;
        }
        #region Method

        public async Task<RequestResult<List<GetAllPatientDto>>> GetAllPatient(string? name, string? Disease)
        {
            try
            {
                return RequestResult<List<GetAllPatientDto>>.CreateSuccessful(_mapper.Map<List<PruebaAppApi.DataAccess.Entities.Patient>, List<GetAllPatientDto>>(await _patientDomain.GetAllPatient(name, Disease)));


            }
            catch (Exception ex)
            {
                return RequestResult<List<GetAllPatientDto>>.CreateError(ex.Message);
            }
        }

        public async Task<RequestResult<SavePatientDto>> SavePatient(SavePatientDto patientDto)
        {
            try
            {
                List<string> errorMessageValidations = new List<string>();
                var patient = _mapper.Map<SavePatientDto, PruebaAppApi.DataAccess.Entities.Patient>(patientDto);
                SaveUserValidations(ref errorMessageValidations, patient);
                if (errorMessageValidations.Any())
                    return RequestResult<SavePatientDto>.CreateUnsuccessful(null, errorMessageValidations);

                bool response = _patientDomain.SavePatient(patient);

                if (!response)
                    return RequestResult<SavePatientDto>.CreateUnsuccessful(null, new[] { ResourceUserMsm.NotExistPatient });

                return RequestResult<SavePatientDto>.CreateSuccessful(patientDto);

            }
            catch (Exception ex)
            {
                return RequestResult<SavePatientDto>.CreateError(ex.Message);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     valida los datos para crear un paciente.
        /// </summary>
        /// <author>Diego Molina</author>
        /// <param name="userDto">objeto para guardar orden de trabajo</param>
        private void SaveUserValidations(ref List<string> errorMessageValidations, PruebaAppApi.DataAccess.Entities.Patient patient)
        {

            if (string.IsNullOrEmpty(patient.FirstName))
            {
                errorMessageValidations.Add(ResourceUserMsm.InvalidValueString);
            }

            if (string.IsNullOrEmpty(patient.LastName))
            {
                errorMessageValidations.Add(ResourceUserMsm.InvalidValueString);
            }

            if (string.IsNullOrEmpty(patient.Email))
            {
                errorMessageValidations.Add(ResourceUserMsm.InvalidValueString);
            }

        }

        #endregion
    }
}
