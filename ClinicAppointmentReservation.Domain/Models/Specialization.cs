using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClinicAppointmentReservation.Domain.Models
{
    public class Specialization
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}
