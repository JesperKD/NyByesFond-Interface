using DataAccess.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface ILegatRepository : IRepository<Legat>
    {
        /// <summary>
        /// Checks for any updates to the database records
        /// </summary>
        /// <param name="existingDataCount"></param>
        /// <returns></returns>
        public bool CheckForNewRecords(int existingDataCount);
    }
}
