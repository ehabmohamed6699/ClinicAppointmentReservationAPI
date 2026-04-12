using ClinicAppointmentReservation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicAppointmentReservation.Domain.Interfaces
{
    public interface IPatientRepository
    {
        void Add(Patient entity);
    }
}
