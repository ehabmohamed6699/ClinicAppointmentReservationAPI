using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicAppointmentReservation.Domain.Models.DTO
{
    public class DoctorDTO : PersonDTO
    {
        public string Bio { get; set; }
        public byte[]? Image { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public bool IsRejected { get; set; }
        public int SpecializationId { get; set; }
        public string SpecializationName { get; set; }
        public List<DoctorClinicDTO> Clinics { get; set; } = new();
    }
    public class DoctorClinicDTO
    {
        public int ClinicId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
