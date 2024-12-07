using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GlamourTime.Domain.Entities;

public partial class SalonService
{
    [Key]
    [Column("ServiceID")]
    public int ServiceId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string ServiceName { get; set; } = null!;

    [Column(TypeName = "text")]
    public string? ServiceDescription { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Price { get; set; }

    [InverseProperty("Service")]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
