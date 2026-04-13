using ClinicAppointmentReservation.Domain.Models;
using ClinicAppointmentReservation.Domain.Models.DTO;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClinicAppointmentReservation.Infrastructure.Data.Mapper
{
    [Mapper]
    public partial class DoctorMapper
    {
        [MapProperty(nameof(Doctor.Specialization.Name), nameof(DoctorDTO.SpecializationName))]
        [MapProperty(nameof(Doctor.DoctorClinics), nameof(DoctorDTO.Clinics))]
        [MapProperty(nameof(Doctor.User.Name), nameof(DoctorDTO.Name))]
        [MapProperty(nameof(Doctor.User.Gender), nameof(DoctorDTO.Gender))]
        [MapProperty(nameof(Doctor.User.DateOfBirth), nameof(DoctorDTO.DateOfBirth))]
        public partial DoctorDTO MapToDoctorDTO(Doctor doctor);
        public partial IEnumerable<DoctorDTO> MapToDoctorDTOs(IEnumerable<Doctor> doctors);
        [MapProperty(new[] { nameof(DoctorClinic.Clinic), nameof(Clinic.Name) }, new[] { nameof(DoctorClinicDTO.Name) })]
        [MapProperty(new[] { nameof(DoctorClinic.Clinic), nameof(Clinic.Address) }, new[] { nameof(DoctorClinicDTO.Address) })]
        [MapProperty(new[] { nameof(DoctorClinic.Clinic), nameof(Clinic.PhoneNumber) }, new[] { nameof(DoctorClinicDTO.PhoneNumber) })]
        protected partial DoctorClinicDTO MapToDoctorClinicDTO(DoctorClinic doctorClinic);
    }
}
