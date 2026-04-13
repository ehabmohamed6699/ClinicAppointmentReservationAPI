using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ClinicAppointmentReservation.Domain.Models
{
    public class Doctor : Person
    {
        [MaxLength(500)]
        public string Bio { get; set; }
        public byte[]? Image { get; set; }
        public bool IsApproved { get; set; } = false;

        [ForeignKey("Specialization")]
        [DisplayName("Specialization")]
        public int SpecializationId { get; set; }
        public virtual Specialization Specialization { get; set; }
        public virtual ICollection<DoctorClinic> DoctorClinics { get; set; }
    }
}
