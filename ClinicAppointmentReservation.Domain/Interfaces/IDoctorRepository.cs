using ClinicAppointmentReservation.Domain.Models;
using ClinicAppointmentReservation.Domain.Records;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicAppointmentReservation.Domain.Interfaces
{
    public interface IDoctorRepository
    {
        Task<(IEnumerable<Doctor> doctors , int TotalCount)> GetAllAsync(DoctorSearchParameters parameters);
        Task<Doctor?> GetByIdAsync(int id);
        void Add(Doctor entity);
        void Update(Doctor entity);
        void Delete(Doctor entity);

        Task<Doctor?> GetDetailsAsync(int id);
        Task<(IEnumerable<Doctor> doctors, int TotalCount)> GetByClinicIdAsync(int clinicId, DoctorSearchParameters parameters);
    }
}
