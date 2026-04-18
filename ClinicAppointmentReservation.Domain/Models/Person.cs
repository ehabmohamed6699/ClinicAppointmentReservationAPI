using ClinicAppointmentReservation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ClinicAppointmentReservation.Domain.Models
{
    public abstract class Person
    {
        [Key]
        public int Id { get; set; }

        public virtual IEnumerable<Appointment> Appointments { get; set; }

        [ForeignKey("User")]

        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
