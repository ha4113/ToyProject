using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace TestServer
{
    public class ConnectionManager
    {
        public ConnectionState? State => _connection?.State;

        private MySqlConnection _connection;
        private readonly Action<string, LogType> _log;

        public ConnectionManager(Action<string, LogType> log)
        {
            _log = log;
        }

        public void Connect(string connectionString)
        {
            if (State.HasValue && State.Value == ConnectionState.Open)
            {
                Close();
            }

            var timeTaken = DateTime.UtcNow.Millisecond;

            _connection = new MySqlConnection(connectionString);
            _connection.Open();
            timeTaken = DateTime.UtcNow.Millisecond - timeTaken;
            Log($"TimeTaken : {timeTaken * 0.001f} sec");
            Log("Complete!", LogType.HIGHLIGHT);

            var moneyDataList = MakeData<MoneyRowData>();
            //var itemDataList = MakeData<ItemRowData>();

            foreach (var data in moneyDataList)
            {
                Log(data.ToString(), LogType.HIGHLIGHT);
            }
        }

        private List<T> MakeData<T>() 
            where T : class, IRowData, new()
        {
            if (ServerConst.TryGetTableName<T>(out var tableName) == false)
            {
                Log($"Not Define Table Name : {typeof(T)}", LogType.WARNING);
                return null;
            }
            var command = $"SELECT * FROM {tableName}";
            var dataTable = new DataTable();
            var dataAdapter = new MySqlDataAdapter(command, _connection);
            //var cmdBuilder = new MySqlCommandBuilder(dataAdapter); // ??????????????
            dataAdapter.Fill(dataTable);

            var tableColumns = new List<string>();
            foreach (DataColumn column in dataTable.Columns)
            {
                tableColumns.Add(column.ColumnName);
            }

            var result = new List<T>();
            foreach (DataRow row in dataTable.Rows)
            {
                var selectData = new T();
                for (var i = 0; i < row.ItemArray.Length; ++i)
                {
                    var columnName = tableColumns[i];
                    var data = row.ItemArray[i];
                    var propertyInfo = selectData.GetType().GetProperty(columnName);

                    if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(selectData, data);
                    }
                }

                result.Add(selectData);
            }

            return result;
        }

        public void Close()
        {
            _connection.Close();
            _connection = null;
            Log("=========================Disconnect=========================", LogType.WARNING);
        }

        private void Log(string log, LogType logType = LogType.NONE)
        {
            _log?.Invoke(log, logType);
        }
    }
}
