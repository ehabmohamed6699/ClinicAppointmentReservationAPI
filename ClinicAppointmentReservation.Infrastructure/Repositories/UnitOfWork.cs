using ClinicAppointmentReservation.Domain.Interfaces;
using ClinicAppointmentReservation.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicAppointmentReservation.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IClinicRepository Clinics { get; private set; }
        public ISpecializationRepository Specializations { get; private set; }
        public UnitOfWork(AppDbContext context) {
            _context = context;
            this.Clinics = new ClinicRepository(_context);
            this.Specializations = new SpecializationRepository(_context);
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
