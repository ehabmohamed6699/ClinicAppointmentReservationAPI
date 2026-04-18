using ClinicAppointmentReservation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClinicAppointmentReservation.Domain.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }


        public DateTime AppointmentDate { get; set; }
        public int DurationInMinutes { get; set; } 

        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;

        public decimal Price { get; set; }
        public string? PatientNotes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }

        public int DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }

        public int ClinicId { get; set; }
        public virtual Clinic Clinic { get; set; }
    }
}
