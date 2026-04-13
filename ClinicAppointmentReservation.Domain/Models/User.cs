using ClinicAppointmentReservation.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ClinicAppointmentReservation.Domain.Models
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = "Anonymous";
        [Required]
        public DateOnly DateOfBirth { get; set; } = new DateOnly(2000, 1, 1);
        [Required]
        public Sex Gender { get; set; } = Sex.Other;
        [NotMapped]
        public int Age => DateOnly.FromDateTime(DateTime.Now).Year - DateOfBirth.Year - (DateOnly.FromDateTime(DateTime.Now).DayOfYear < DateOfBirth.DayOfYear ? 1 : 0);
    }
}
