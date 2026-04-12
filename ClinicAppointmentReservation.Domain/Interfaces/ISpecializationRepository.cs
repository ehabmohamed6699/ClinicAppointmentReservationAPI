using ClinicAppointmentReservation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicAppointmentReservation.Domain.Interfaces
{
    public interface ISpecializationRepository
    {
        Task<IEnumerable<Specialization>> GetAllAsync();
        Task<Specialization?> GetByIdAsync(int id);
        void Create(Specialization specialization);
        void Update(Specialization specialization);
        void Delete(Specialization specialization);
    }
}
