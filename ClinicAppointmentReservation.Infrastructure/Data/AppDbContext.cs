using ClinicAppointmentReservation.Domain.Enums;
using ClinicAppointmentReservation.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicAppointmentReservation.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<DoctorClinic> DoctorClinics { get; set; }
        public DbSet<Patient> Patients { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Specialization>().HasData(
                new Specialization { Id = 1, Name = "Cardiology" },
                new Specialization { Id = 2, Name = "Dermatology" },
                new Specialization { Id = 3, Name = "Neurology" },
                new Specialization { Id = 4, Name = "Pediatrics" },
                new Specialization { Id = 5, Name = "Orthopedics" }
            );
        }
    }
}
