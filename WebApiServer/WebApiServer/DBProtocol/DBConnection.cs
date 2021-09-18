using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using WebApiServer.Attribute;
using WebApiServer.Controllers;

namespace WebApiServer.DBProtocol
{
    public class DBConnection : IDisposable
    {
        private ILogger<WakeController> _logger;
        private MySqlConnection _connection;
        
        private DBConnection(ILogger<WakeController> logger, DB dbType)
        {
            _logger = logger;
            _connection = new MySqlConnection(dbType.GetConfig());
        }

        public static async Task<T> Connect<T>(ILogger<WakeController> logger, long id)
            where T : class, IDBSchema, new()
        {
            var dbTableAttr = typeof(T).GetCustomAttribute<DBTable>();

            if (dbTableAttr == null)
            {
                logger.Log(LogLevel.Critical, $"Not Define Table Attr : {typeof(T)}");
                return null;
            }

            var dbConnection = new DBConnection(logger, dbTableAttr.DBType);
            await dbConnection._connection.OpenAsync();
            return await dbConnection.GetData<T>(id);
        }

        private async Task<T> GetData<T>(long id)
            where T : class, IDBSchema, new()
        {
            var dbTableAttr = typeof(T).GetCustomAttribute<DBTable>();

            if (dbTableAttr == null)
            {
                Log(LogLevel.Critical, $"Not Define Table Attr : {typeof(T)}");
                return null;
            }

            var dbConfig = dbTableAttr.DBType.GetConfig();
            if (dbConfig == default)
            {
                Log(LogLevel.Critical, $"Not Define Table Name : {typeof(T)}");
                return null;
            }

            var command = $"SELECT * FROM {dbTableAttr.TableName} WHERE id={id}";
            Log(LogLevel.Information,"=========================Connection=========================");
            
            var dataTable = new DataTable();
            var dataAdapter = new MySqlDataAdapter(command, _connection);
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

            Log(LogLevel.Information, "=========================Disconnect=========================");
            return null;
        }

        private void Log(LogLevel logLevel, string log)
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