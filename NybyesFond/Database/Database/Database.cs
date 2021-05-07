using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Database
{
    public abstract class Database : IDatabase
    {
        private readonly IConfiguration _configuration;

        protected Database(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnectionString(string databaseName)
        {
            if (_configuration == null) throw new NullReferenceException(message: "Configuration was null.");

            string connectionString = _configuration.GetConnectionString(databaseName);

            if (string.IsNullOrEmpty(connectionString)) throw new NullReferenceException(message: "Connection could not be loaded.");

            return connectionString;
        }

        public abstract Task CloseConnectionAsync();
        public abstract Task OpenConnectionAsync();
        public abstract Task<DbDataReader> GetDataReaderAsync(string cmdText = null, IDictionary<string, object> sqlParams = null);
        public abstract Task ExecuteNonQueryAsync(string cmdText = null, IDictionary<string, object> sqlParams = null);
    }
}
