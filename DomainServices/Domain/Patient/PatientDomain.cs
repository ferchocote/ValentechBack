using DomainServices.Domain.Contracts.Patient;
using Microsoft.EntityFrameworkCore;
using PruebaAppApi.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices.Domain.Patient
{
    public class PatientDomain : IPatientDomain
    {
        private readonly ValentechCoreContext _context;
        public PatientDomain(ValentechCoreContext context)
        {
            _context = context;
        }

        #region Method

        public async Task<List<PruebaAppApi.DataAccess.Entities.Patient>> GetAllPatient(string? name, string? disease)
        {
            var query = _context.Patient.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.FirstName.Contains(name) || p.LastName.Contains(name));
            }

            if (!string.IsNullOrEmpty(disease))
            {
                query = query.Where(p => p.Disease.Contains(disease));
            }

            return await query.ToListAsync();
        }

        public bool SavePatient(PruebaAppApi.DataAccess.Entities.Patient patient)
        {
            bool exists = _context.Patient.Any(p =>
                        (p.FirstName == patient.FirstName && p.LastName == patient.LastName) ||
                        (p.Email == patient.Email)
    );

            if (exists)
            {
                return false;
            }

            _context.Patient.Add(patient);
            _context.SaveChanges();
            return true;
        }

        #endregion|
    }
}
