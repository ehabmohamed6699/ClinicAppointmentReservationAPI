using ClinicAppointmentReservation.Domain.Interfaces;
using ClinicAppointmentReservation.Domain.Models;
using ClinicAppointmentReservation.Domain.Records;
using ClinicAppointmentReservation.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicAppointmentReservation.Infrastructure.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppDbContext _context;
        public DoctorRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Add(Doctor entity)
        {
            _context.Doctors.Add(entity);
        }

        public void Delete(Doctor entity)
        {
            _context.Doctors.Remove(entity);
        }

        public async Task<(IEnumerable<Doctor> doctors, int TotalCount)> GetAllAsync(DoctorSearchParameters parameters)
        {
            var query = _context.Doctors.AsNoTracking().AsQueryable().Where(d => d.IsApproved == true);
            if (!string.IsNullOrWhiteSpace(parameters.Search))
            {
                query = query.Where(d => d.User.Name.Contains(parameters.Search));
            }
            if(parameters.SpecializationId.HasValue)
            {
                query = query.Where(d => d.SpecializationId == parameters.SpecializationId.Value);
            }
            if(parameters.ClinicId.HasValue)
            {
                query = query.Where(d => d.DoctorClinics.Any(dc => dc.ClinicId == parameters.ClinicId.Value));
            }
            int page = Math.Max(parameters.Page, 1);
            var totalCount = await query.CountAsync();
            var doctors = await query.Skip((page - 1) * parameters.PageSize)
                                      .Take(parameters.PageSize)
                                      .ToListAsync();
            return (doctors, totalCount);
        }

        public async Task<(IEnumerable<Doctor> doctors, int TotalCount)> GetByClinicIdAsync(int clinicId, DoctorSearchParameters parameters)
        {
            var query = _context.Clinics.Where(c => c.Id == clinicId)
                                        .SelectMany(c => c.DoctorClinics)
                                        .Select(dc => dc.Doctor)
                                        .AsNoTracking()
                                        .AsQueryable();
            if (!string.IsNullOrWhiteSpace(parameters.Search))
            {
                query = query.Where(d => d.User.Name.Contains(parameters.Search));
            }
            if(parameters.SpecializationId.HasValue)
            {
                query = query.Where(d => d.SpecializationId == parameters.SpecializationId.Value);
            }
            if(parameters.ClinicId.HasValue)
            {
                query = query.Where(d => d.DoctorClinics.Any(dc => dc.ClinicId == parameters.ClinicId.Value));
            }
            int page = Math.Max(parameters.Page, 1);
            var totalCount = await query.CountAsync();
            var doctors = await query.Skip((page - 1) * parameters.PageSize)
                                      .Take(parameters.PageSize)
                                      .ToListAsync();
            return (doctors, totalCount);
        }

        public async Task<Doctor?> GetByIdAsync(int id)
        {
            return await _context.Doctors.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);
        }

        public Task<Doctor?> GetByUserIdAsync(string userId)
        {
            var doctor = _context.Doctors.AsNoTracking().FirstOrDefaultAsync(d => d.UserId == userId);
            return doctor;
        }

        public async Task<Doctor?> GetDetailsAsync(int id)
        {
            return await _context.Doctors.AsNoTracking().Include(x => x.Specialization)
                                            .Include(d => d.User)
                                            .Include(d => d.DoctorClinics)
                                            .ThenInclude(dc => dc.Clinic)
                                            .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<(IEnumerable<Doctor> doctors, int TotalCount)> GetPendingDoctors(DoctorSearchParameters parameters)
        {
            var query = _context.Doctors.AsNoTracking().Where(d => !d.IsApproved && !d.IsRejected);
            if (!string.IsNullOrWhiteSpace(parameters.Search))
            {
                query = query.Where(d => d.User.Name.Contains(parameters.Search));
            }
            if (parameters.SpecializationId.HasValue)
            {
                query = query.Where(d => d.SpecializationId == parameters.SpecializationId.Value);
            }
            int page = Math.Max(parameters.Page, 1);
            var totalCount = await query.CountAsync();
            var doctors = await query
                                     .Include(d => d.User)
                                     .Skip((page - 1) * parameters.PageSize)
                                     .Take(parameters.PageSize)
                                     .ToListAsync();
            return (doctors, totalCount);
        }

        public void Update(Doctor entity)
        {
            _context.Doctors.Update(entity);
        }
    }
}
