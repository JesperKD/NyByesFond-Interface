using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Database
{
    public interface IDatabase
    {
        Task<DbDataReader> GetDataReaderAsync(string cmdText = null, IDictionary<string, object> sqlParams = null);

        Task ExecuteNonQueryAsync(string cmdText = null, IDictionary<string, object> sqlParams = null);
    }
}
