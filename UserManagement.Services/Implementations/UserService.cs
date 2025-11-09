using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Data;
using UserManagement.Data.Interfaces;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Services.Domain.Implementations;

public class UserService : IUserService
{

    private readonly IDataContext _dataAccess;
    public UserService(IDataContext dataAccess) => _dataAccess = dataAccess;

    /// <summary>
    /// Return entites by active state
    /// </summary>
    /// <param name="isActive"></param>
    /// <returns></returns>
    public async Task<List<User>> FilterUsersByActiveAsync(bool isActive)
    {
        return isActive ? await _dataAccess.GetActiveAsync<User>() : await _dataAccess.GetInactiveAsync<User>();
    }
    /// <summary>
    /// Returns all entities of type
    /// </summary>
    /// <returns></returns>
    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _dataAccess.GetAllAsync<User>();
    }
    /// <summary>
    /// Returns an entity by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<User?> GetUserByIdAsync(long id)
    {
        return await _dataAccess.GetByIdAsync<User>(id);
    }
    /// <summary>
    /// Returns a list of entity by EntityId
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<List<LogRecord>> GetLogRecordsByEntityIdAsync(long id)
    {
        return await _dataAccess.GetByEntityIdAsync<LogRecord>(id);
    } 
    /// <summary>
    /// Creates a new entity
    /// </summary>
    /// <param name="entity"></param>
    public async Task<long> CreateAsync(IEntity entity)
    {
        return await _dataAccess.CreateAsync(entity);
    }
    /// <summary>
    /// Updates an existing entity
    /// </summary>
    /// <param name="entity"></param>
    public async Task<long> UpdateAsync(IEntity entity)
    {
        return await _dataAccess.UpdateAsync(entity);
    }
    /// <summary>
    /// Deletes an entity
    /// </summary>
    /// <param name="entity"></param>
    public async Task DeleteAsync(IEntity entity)
    {
        await _dataAccess.DeleteAsync(entity);
    }
}
