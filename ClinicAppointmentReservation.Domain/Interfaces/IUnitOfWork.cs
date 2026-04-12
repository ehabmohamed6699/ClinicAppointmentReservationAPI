using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicAppointmentReservation.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IClinicRepository Clinics { get; }
        ISpecializationRepository Specializations { get; }
        IPatientRepository Patients { get; }
        IDoctorRepository Doctors { get; }
        Task<int> SaveChangesAsync();
    }
}
