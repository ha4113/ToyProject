using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using WebApiServer.Controllers;

namespace WebApiServer
{
    public class DBConnection : IDisposable
    {
        private ILogger<WakeController> _logger;
        private MySqlConnection _connection;
        private DBConnection(ILogger<WakeController> logger, string dbConfig)
        {
            _logger = logger;
            _connection = new MySqlConnection(dbConfig);
            
        }

        public static async Task<DBConnection> Connect(ILogger<WakeController> logger, string dbConfig)
        {
            var dbConnection = new DBConnection(logger, dbConfig);
            await dbConnection._connection.OpenAsync();
            return dbConnection;
        }
        
        public async Task<T> GetData<T>(long id)
            where T : class, IDBModel, new()
        {
            var dbTableAttr = typeof(T).GetCustomAttribute<DBTable>();

            if (dbTableAttr == null)
            {
                Log($"Not Define Table Attr : {typeof(T)}", LogLevel.Critical);
                return null;
            }

            var dbConfig = dbTableAttr.DBType.GetConfig();
            if (dbConfig == default)
            {
                Log($"Not Define Table Name : {typeof(T)}", LogLevel.Critical);
                return null;
            }

            var command = $"SELECT * FROM {dbTableAttr.TableName}";
            Log("=========================Connection=========================", LogLevel.Information);
            
            var dataTable = new DataTable();
            var dataAdapter = new MySqlDataAdapter(command, _connection);
            //var cmdBuilder = new MySqlCommandBuilder(dataAdapter); // ??????????????
            await dataAdapter.FillAsync(dataTable);

            var tableColumns = new List<int>();
            foreach (DataColumn column in dataTable.Columns)
            {
                tableColumns.Add(column.ColumnName.GetHashCode());
            }

            foreach (DataRow row in dataTable.Rows)
            {
                var selectData = new T();
                for (var i = 0; i < row.ItemArray.Length; ++i)
                {
                    var columnName = tableColumns[i];
                    var data = row.ItemArray[i];
                    var propertyInfos = selectData.GetType().GetProperties();

                    foreach (var propertyInfo in propertyInfos)
                    {
                        var dbColumn = propertyInfo.GetCustomAttribute<DBColumn>();
                        if (dbColumn != null && dbColumn.ColumnNameHash == columnName)
                        {
                            propertyInfo.SetValue(selectData, data);
                        }
                    }
                }

                if (selectData.Id == id)
                {
                    return selectData;
                }
            }
            Log("=========================Disconnect=========================", LogLevel.Information);
            return null;
        }

        private void Log(string log, LogLevel logLevel = LogLevel.None)
        {
            _logger.Log(logLevel, log);
            // _log?.Invoke(log, logType);
        }

        public async void Dispose()
        {
            await _connection.CloseAsync();
            _connection = null;
        }
    }
}