using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Database
{
    public class MySqlDatabase : Database
    {
        private readonly MySqlConnection _sqlConnection;

        public MySqlDatabase(IConfiguration configuration) : base(configuration)
        {
            _sqlConnection = new MySqlConnection(GetConnectionString("NyeByesConnection"));
        }

        public override async Task CloseConnectionAsync()
        {
            try
            {
                if (_sqlConnection.State == ConnectionState.Closed) return;

                await _sqlConnection.CloseAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override async Task OpenConnectionAsync()
        {
            try
            {
                if (_sqlConnection.State == ConnectionState.Open) return;

                await _sqlConnection.OpenAsync();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public MySqlConnection SqlConnection { get { return _sqlConnection; } }
    }
}
