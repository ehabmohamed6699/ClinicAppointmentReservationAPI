using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClinicAppointmentReservation.Domain.Models
{
    public class Clinic
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Address { get; set; }
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        public virtual ICollection<DoctorClinic> DoctorClinics { get; set; }
    }
}
