using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Data.Interfaces;

namespace UserManagement.Data;

public interface IDataContext
{
    /// <summary>
    /// Get a list of entities
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    Task<List<TEntity>> GetAllAsync<TEntity>() where TEntity : class, IEntity;
    /// <summary>
    /// Get active entities
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    Task<List<TEntity>> GetActiveAsync<TEntity>() where TEntity : class, IEntity;
    /// <summary>
    /// Get inactive entites
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    Task<List<TEntity>> GetInactiveAsync<TEntity>() where TEntity : class, IEntity;
    /// <summary>
    /// Get an entity by Id
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<TEntity?> GetByIdAsync<TEntity>(long id) where TEntity : class, IEntity;
    /// <summary>
    /// Get a list of Entities by EntityId
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<List<TEntity>> GetByEntityIdAsync<TEntity>(long id) where TEntity : class, IEntity;
    /// <summary>
    /// Create a new entity
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<long> CreateAsync<TEntity>(TEntity entity) where TEntity : class, IEntity;
    /// <summary>
    /// Updates an entity
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entity"></param>
    /// <returns>Id of the affected entity</returns>
    Task<long> UpdateAsync<TEntity>(TEntity entity) where TEntity : class, IEntity;
    /// <summary>
    /// Deletes and entity
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entity"></param>
    Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class, IEntity;
}
