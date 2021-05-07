using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Database
{
    public class MySqlDatabase : Database
    {
        private readonly MySqlConnection _mySqlConnection;

        public MySqlDatabase(IConfiguration configuration) : base(configuration)
        {
            string connString = GetConnectionString(configuration.GetSection("UseConnection").Value);
            _mySqlConnection = new(connString);
        }

        public override async Task ExecuteNonQueryAsync(string cmdText = null, IDictionary<string, object> sqlParams = null)
        {
            using MySqlCommand commandObj = new()
            {
                CommandText = cmdText,
                CommandType = CommandType.Text,
                Connection = _mySqlConnection
            };

            if (SqlParamsIsNotNull(sqlParams))
            {
                AddSqlParamsToSqlCommand(sqlParams, commandObj);
            }

            await OpenConnectionAsync();

            await commandObj.ExecuteNonQueryAsync();
        }

        public override async Task<DbDataReader> GetDataReaderAsync(string cmdText = null, IDictionary<string, object> sqlParams = null)
        {
            using MySqlCommand commandObj = new()
            {
                CommandText = cmdText,
                CommandType = CommandType.Text,
                Connection = _mySqlConnection
            };

            if (SqlParamsIsNotNull(sqlParams))
            {
                AddSqlParamsToSqlCommand(sqlParams, commandObj);
            }

            await OpenConnectionAsync();

            return await commandObj.ExecuteReaderAsync(CommandBehavior.CloseConnection);
        }

        public override async Task CloseConnectionAsync()
        {
            if (_mySqlConnection.State == ConnectionState.Closed) return;

            await _mySqlConnection.CloseAsync();
        }

        public override async Task OpenConnectionAsync()
        {
            if (_mySqlConnection.State == ConnectionState.Open) return;

            await _mySqlConnection.OpenAsync();
        }

        private static bool SqlParamsIsNotNull(IDictionary<string, object> sqlParams)
        {
            return sqlParams != null;
        }

        private static void AddSqlParamsToSqlCommand(IDictionary<string, object> sqlParams, MySqlCommand commandObj)
        {
            foreach (var param in sqlParams)
            {
                commandObj.Parameters.AddWithValue(param.Key, param.Value);
            }
        }
    }
}
