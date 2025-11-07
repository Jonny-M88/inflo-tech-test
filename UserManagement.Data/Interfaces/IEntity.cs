namespace UserManagement.Data.Interfaces;
public interface IEntity
{
    public long Id { get; set; }
    public bool IsActive { get; set; }
}
