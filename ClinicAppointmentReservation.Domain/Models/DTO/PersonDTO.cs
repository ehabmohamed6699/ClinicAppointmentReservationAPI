using ClinicAppointmentReservation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicAppointmentReservation.Domain.Models.DTO
{
    public abstract class PersonDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public int Age  => DateOnly.FromDateTime(DateTime.Now).Year - DateOfBirth.Year - (DateOnly.FromDateTime(DateTime.Now).DayOfYear < DateOfBirth.DayOfYear ? 1 : 0);
        public Sex Gender { get; set; }
        
    }
}
