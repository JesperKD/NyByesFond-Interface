using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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
            try
            {
                if (_configuration == null) throw new NullReferenceException(message: "Configuration was null.");

                string connectionString = _configuration.GetConnectionString(databaseName);

                if (string.IsNullOrEmpty(connectionString)) throw new NullReferenceException(message: "Connection string could not be loaded.");

                return connectionString;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public abstract Task CloseConnectionAsync();
        public abstract Task OpenConnectionAsync();




    }
}
