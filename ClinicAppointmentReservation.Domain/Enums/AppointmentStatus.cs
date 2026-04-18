using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicAppointmentReservation.Domain.Enums
{
    public enum AppointmentStatus
    {
        Pending,
        Confirmed,
        CanceledByPatient,
        CanceledByDoctor,
        Completed,
        NoShow
    }
}
