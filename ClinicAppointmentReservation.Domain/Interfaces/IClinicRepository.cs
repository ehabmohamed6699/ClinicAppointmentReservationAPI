using ClinicAppointmentReservation.Domain.Models;
using ClinicAppointmentReservation.Domain.Records;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicAppointmentReservation.Domain.Interfaces
{
    public interface IClinicRepository
    {
        Task<(IEnumerable<Clinic> Clinics, int TotalCount)> GetAllAsync(ClinicSearchParameters parameters);
        Task<Clinic?> GetByIdAsync(int id);
        void Add(Clinic entity);
        void Update(Clinic entity);
        void Delete(Clinic entity);
        void AssignDoctorToClinic(int doctorId, int clinicId);
        void UnassignDoctorFromClinic(int doctorId, int clinicId);
    }
}
