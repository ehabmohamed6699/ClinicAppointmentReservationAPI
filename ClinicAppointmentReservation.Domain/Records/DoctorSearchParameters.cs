using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicAppointmentReservation.Domain.Records
{
    public record DoctorSearchParameters(
        string? Search,
        int? SpecializationId,
        int? ClinicId,
        int Page = 1,
        int PageSize = 10
    );
}
