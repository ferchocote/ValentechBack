using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationServices.DTOs.Patient
{
    public class SavePatientDto
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string DocumentNumber { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Disease { get; set; } = null!;

        public DateTime CreationDate { get; set; }

        public Guid CreationUser { get; set; }
    }
}
