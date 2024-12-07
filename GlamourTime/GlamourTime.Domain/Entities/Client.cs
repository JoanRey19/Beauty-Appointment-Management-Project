using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GlamourTime.Domain.Entities;

public partial class Client
{
    [Key]
    [Column("ClientID")]
    public int ClientId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string FirstName { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string LastName { get; set; } = null!;

    [StringLength(15)]
    [Unicode(false)]
    public string? Phone { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Email { get; set; }

    public DateOnly? RegistrationDate { get; set; }

    [InverseProperty("Client")]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
