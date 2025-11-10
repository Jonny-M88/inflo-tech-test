using System;
using UserManagement.Data.Enum;

namespace UserManagement.Web.Models.Logs;

public class LogRecordListViewModel
{
    public List<LogRecordListViewModel> Items { get; set; } = new();
}

public class LogRecordListItemViewModel
{
    public long Id { get; set; }
    public long EntityId { get; set; }
    public string PerformedBy { get; set; } = string.Empty;
    public DateTime ActionDate { get; set; }
    public CommitAction Action { get; set; }
    public bool IsActive { get; set; }
}
