using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GlamourTime.Domain.Entities;

public partial class Appointment
{
    [Key]
    [Column("AppointmentID")]
    public int AppointmentId { get; set; }

    [Column("ClientID")]
    public int? ClientId { get; set; }

    [Column("StylistID")]
    public int? StylistId { get; set; }

    [Column("ServiceID")]
    public int? ServiceId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime AppointmentDate { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Status { get; set; }

    [ForeignKey("ClientId")]
    [InverseProperty("Appointments")]
    public virtual Client? Client { get; set; }

    [InverseProperty("Appointment")]
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    [ForeignKey("ServiceId")]
    [InverseProperty("Appointments")]
    public virtual SalonService? Service { get; set; }

    [ForeignKey("StylistId")]
    [InverseProperty("Appointments")]
    public virtual Stylist? Stylist { get; set; }
}
