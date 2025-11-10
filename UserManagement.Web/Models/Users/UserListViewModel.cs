using System;
using System.ComponentModel.DataAnnotations;
using UserManagement.Web.Enum;

namespace UserManagement.Web.Models.Users;

public class UserListViewModel
{
    public List<UserListItemViewModel> Items { get; set; } = new();
}

public class UserListItemViewModel
{
    public long Id { get; set; } = -1;
    public string? Forename { get; set; } = string.Empty;
    public string? Surname { get; set; } = string.Empty;
    [Required, EmailAddress]
    public string? Email { get; set; } = string.Empty;
    public DateOnly? DateOfBirth { get; set; } = null;
    public bool IsActive { get; set; } = true;
    public FormMode Mode { get; set; } = FormMode.Create;
    public string Quote { get; set; } = string.Empty;
    public bool IsAdmin { get; set; } = false;
}
