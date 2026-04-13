using ClinicAppointmentReservation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClinicAppointmentReservation.Domain.Models.DTO
{
    public class FormUserRegister
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateOnly DateOfBirth { get; set; }
        [Required]
        [Range((int)Sex.Male, (int)Sex.Other)]
        public Sex Gender { get; set; }
        [Required]
        public string Password { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
