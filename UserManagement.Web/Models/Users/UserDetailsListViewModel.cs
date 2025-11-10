using System;
using UserManagement.Web.Enum;
using UserManagement.Web.Models.Logs;

namespace UserManagement.Web.Models.Users;

public class UserDetailsListViewModel
{
    public long Id { get; set; }
    public string Forename { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateOnly? DateOfBirth { get; set; } = null;
    public bool IsActive { get; set; } = true;
    public FormMode Mode { get; set; } = FormMode.Create;
    public string Quote { get; set; } = string.Empty;

    public List<LogRecordListItemViewModel> Logs { get; set; } = new();
}
