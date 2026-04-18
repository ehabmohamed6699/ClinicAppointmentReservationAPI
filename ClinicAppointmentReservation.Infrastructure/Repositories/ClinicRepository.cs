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
    public class ClinicRepository : IClinicRepository
    {
        private readonly AppDbContext _context;
        public ClinicRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Add(Clinic entity)
        {
            _context.Clinics.Add(entity);
        }

        public void AssignDoctorToClinic(int doctorId, int clinicId)
        {
            var existingAssignment = _context.DoctorClinics.FirstOrDefault(dc => dc.DoctorId == doctorId && dc.ClinicId == clinicId);
            if (existingAssignment != null) { 
                throw new InvalidOperationException("Doctor is already assigned to the clinic.");
            }
            _context.DoctorClinics.Add(new DoctorClinic { DoctorId = doctorId, ClinicId = clinicId });
        }

        public void Delete(Clinic entity)
        {
            _context.Clinics.Remove(entity);
        }

        public async Task<(IEnumerable<Clinic> Clinics, int TotalCount)> GetAllAsync(ClinicSearchParameters parameters)
        {
            var query = _context.Clinics.AsNoTracking().AsQueryable();
            if (!string.IsNullOrWhiteSpace(parameters.Search))
            {
                query = query.Where(c => c.Name.Contains(parameters.Search) || c.Address.Contains(parameters.Search));
            }
            int page = Math.Max(parameters.Page, 1);
            var totalCount = await query.CountAsync();
            var clinics = await query.Skip((page - 1) * parameters.PageSize)
                                     .Take(parameters.PageSize)
                                     .ToListAsync();

            return (clinics, totalCount);
        }

        public async Task<Clinic?> GetByIdAsync(int id)
        {
            return await _context.Clinics.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public void UnassignDoctorFromClinic(int doctorId, int clinicId)
        {
            var doctorClinic = _context.DoctorClinics.FirstOrDefault(dc => dc.DoctorId == doctorId && dc.ClinicId == clinicId);
            if (doctorClinic != null)
            {
                _context.DoctorClinics.Remove(doctorClinic);
            }
            else
            {
                throw new InvalidOperationException("Doctor is not assigned to the clinic.");
            }
        }

        public void Update(Clinic entity)
        {
            _context.Clinics.Update(entity);
        }
    }
}
