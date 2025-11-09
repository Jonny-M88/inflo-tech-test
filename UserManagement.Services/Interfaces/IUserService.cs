using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Data.Interfaces;
using UserManagement.Models;

namespace UserManagement.Services.Domain.Interfaces;

public interface IUserService
{
    /// <summary>
    /// Returns all user entities
    /// </summary>
    /// <returns></returns>
    Task<List<User>> GetAllUsersAsync();
    /// <summary>
    /// Return users by active state
    /// </summary>
    /// <param name="isActive"></param>
    /// <returns></returns>
    Task<List<User>> FilterUsersByActiveAsync(bool isActive);
    /// <summary>
    /// Returns a user by Id or null if none exists
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<User?> GetUserByIdAsync(long id);
    /// <summary>
    /// Returns a list of LogRecords by EntityId
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<List<LogRecord>> GetLogRecordsByEntityIdAsync(long id);
    /// <summary>
    /// Creates a new Entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>Created Entity's Id</returns>
    Task<long> CreateAsync(IEntity entity);
    /// <summary>
    /// Updates an existing Entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>Updated Entity's Id</returns>
    Task<long> UpdateAsync(IEntity entity);
    /// <summary>
    /// Delete a user
    /// </summary>
    Task DeleteAsync(IEntity entity);
}
