using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UserManagement.Data.Interfaces;

namespace UserManagement.Models;

public class User : IEntity
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string Forename { get; set; } = default!;
    public string Surname { get; set; } = default!;
    [Required, EmailAddress]
    public string Email { get; set; } = default!;
    public DateOnly DateOfBirth { get; set; }
    public bool IsActive { get; set; }
    long IEntity.EntityId {get => Id; set => Id = value;}
    public string Quote { get; set; } = string.Empty;
    public bool IsAdmin { get; set; } = false;
}
