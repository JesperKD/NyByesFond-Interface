using DataAccess.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Creates new entity
        /// </summary>
        /// <param name="createEntity"></param>
        /// <returns>A Task representing the asynchronous process.</returns>
        public Task CreateAsync(T createEntity);

        /// <summary>
        /// Deletes entity
        /// </summary>
        /// <param name="deleteEntity"></param>
        /// <returns>A Task representing the asynchronous process.</returns>
        public Task DeleteAsync(T deleteEntity);

        /// <summary>
        /// get entity by identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A Task representing the asynchronous process.</returns>
        public Task<T> GetByIdAsync(long id);

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>A Task representing the asynchronous process.</returns>
        public Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// update entity
        /// </summary>
        /// <param name="updateEntity"></param>
        /// <returns>A Task representing the asynchronous process.</returns>
        public Task UpdateAsync(T updateEntity);

    }
}
