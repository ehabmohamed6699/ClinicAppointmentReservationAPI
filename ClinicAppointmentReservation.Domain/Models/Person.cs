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

        [MaxLength(100)]
        public string Name { get; set; }
        public DateOnly DateOfBirth { get; set; }
        [NotMapped]
        public int Age  => DateOnly.FromDateTime(DateTime.Now).Year - DateOfBirth.Year - (DateOnly.FromDateTime(DateTime.Now).DayOfYear < DateOfBirth.DayOfYear ? 1 : 0);

        public Sex Gender { get; set; }

        [ForeignKey("User")]

        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
