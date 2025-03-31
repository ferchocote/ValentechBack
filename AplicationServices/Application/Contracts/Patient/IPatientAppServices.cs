using AplicationServices.DTOs.Generics;
using AplicationServices.DTOs.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationServices.Application.Contracts.Patient
{
    public interface IPatientAppServices
    {
        Task<RequestResult<List<GetAllPatientDto>>> GetAllPatient(string? name, string? Disease);
        Task<RequestResult<SavePatientDto>> SavePatient(SavePatientDto patientDto);
    }
}
