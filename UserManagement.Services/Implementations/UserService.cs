using System.Collections.Generic;
using System.Linq;
using UserManagement.Data;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Services.Domain.Implementations;

public class UserService : IUserService
{
    private readonly IDataContext _dataAccess;
    public UserService(IDataContext dataAccess) => _dataAccess = dataAccess;

    /// <summary>
    /// Return users by active state
    /// </summary>
    /// <param name="isActive"></param>
    /// <returns></returns>
    public IEnumerable<User> FilterByActive(bool isActive)
    {
        return isActive ? _dataAccess.GetActive<User>() : _dataAccess.GetInactive<User>();
    }

    /// <summary>
    /// Returns all users
    /// </summary>
    /// <returns></returns>
    public IEnumerable<User> GetAll() => _dataAccess.GetAll<User>();

    /// <summary>
    /// Returns a user by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public IQueryable<User> GetById(long id) => _dataAccess.GetById<User>(id);

    /// <summary>
    /// Creates a new user
    /// </summary>
    /// <param name="entity"></param>
    public void Create(User entity)
    {
        _dataAccess.Create(entity);
    }

    /// <summary>
    /// Updates an existing  user
    /// </summary>
    /// <param name="entity"></param>
    public void Update(User entity)
    {
        _dataAccess.Update(entity);
    }

    /// <summary>
    /// Deletes a user
    /// </summary>
    /// <param name="entity"></param>
    public void Delete(User entity)
    {
        _dataAccess.Delete(entity);
    }


}
