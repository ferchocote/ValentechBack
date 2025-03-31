using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices.Domain.Contracts.Patient
{
    public interface IPatientDomain
    {
        Task<List<PruebaAppApi.DataAccess.Entities.Patient>> GetAllPatient(string? name, string? Disease);
        bool SavePatient(PruebaAppApi.DataAccess.Entities.Patient patient);
    }
}
