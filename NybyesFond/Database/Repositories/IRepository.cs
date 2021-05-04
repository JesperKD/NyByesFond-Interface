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
        /// <returns></returns>
        public Task Create(T createEntity);

        /// <summary>
        /// Deletes entity
        /// </summary>
        /// <param name="deleteEntity"></param>
        /// <returns></returns>
        public Task Delete(T deleteEntity);

        /// <summary>
        /// get entity by identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<T> GetById(long id);

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<T>> GetAll();

        /// <summary>
        /// update entity
        /// </summary>
        /// <param name="updateEntity"></param>
        /// <returns></returns>
        public Task Update(T updateEntity);

    }
}
