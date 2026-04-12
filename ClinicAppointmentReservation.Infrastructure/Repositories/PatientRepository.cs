using ClinicAppointmentReservation.Domain.Interfaces;
using ClinicAppointmentReservation.Domain.Models;
using ClinicAppointmentReservation.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicAppointmentReservation.Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly AppDbContext _context;
        public PatientRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Add(Patient entity)
        {
            _context.Patients.Add(entity);
        }
    }
}
