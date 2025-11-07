using System.Collections.Generic;
using System.Linq;
using UserManagement.Models;

namespace UserManagement.Services.Domain.Interfaces;

public interface IUserService 
{
    /// <summary>
    /// Return users by active state
    /// </summary>
    /// <param name="isActive"></param>
    /// <returns></returns>
    IEnumerable<User> FilterByActive(bool isActive);
    /// <summary>
    /// Returns all users
    /// </summary>
    /// <returns></returns>
    IEnumerable<User> GetAll();
    /// <summary>
    /// Returns a user by Id or null if none exists
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    IQueryable<User> GetById(long id);
    /// <summary>
    /// Create a new user
    /// </summary>
    public void Create(User entity);
    /// <summary>
    /// Update an existing user
    /// </summary>
    public void Update(User entity);
    /// <summary>
    /// Delete a user
    /// </summary>
    public void Delete(User entity);
}
