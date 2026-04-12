using ClinicAppointmentReservation.Domain.Interfaces;
using ClinicAppointmentReservation.Domain.Models;
using ClinicAppointmentReservation.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicAppointmentReservation.Infrastructure.Repositories
{
    public class SpecializationRepository : ISpecializationRepository
    {
        private readonly AppDbContext _context;
        public SpecializationRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Create(Specialization specialization)
        {
            _context.Specializations.Add(specialization);
        }

        public void Delete(Specialization specialization)
        {
            _context.Specializations.Remove(specialization);
        }

        public async Task<IEnumerable<Specialization>> GetAllAsync()
        {
            return await _context.Specializations.AsNoTracking().ToListAsync();
        }

        public async Task<Specialization?> GetByIdAsync(int id)
        {
            return await _context.Specializations.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        }

        public void Update(Specialization specialization)
        {
            _context.Specializations.Update(specialization);
        }
    }
}
