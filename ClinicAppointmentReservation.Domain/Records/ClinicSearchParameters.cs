using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicAppointmentReservation.Domain.Records
{
    public record ClinicSearchParameters(
        string? Search,
        int Page = 1,
        int PageSize = 10
    );
}
