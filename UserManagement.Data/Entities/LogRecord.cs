using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UserManagement.Data.Enum;
using UserManagement.Data.Interfaces;

namespace UserManagement.Models;

public class LogRecord : IEntity
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public long EntityId { get; set; }
    public string PerformedBy { get; set; } = string.Empty;
    public DateTime ActionDate { get; set; }
    public LogAction Action { get; set; }
    public bool IsActive { get; set; }
}
