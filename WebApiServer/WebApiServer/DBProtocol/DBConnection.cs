using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Server.Attribute;
using Server.DBProtocol.Schema;
using Server.Enums;
using Enum = System.Enum;

namespace Server.DBProtocol
{
    public class DBConnection : IDisposable
    {
        private MySqlConnection _connection;

        public DBConnection(DB dbType)
        {
            _connection = new MySqlConnection(dbType.GetConfig());
        }

        public static User Connect(long id)
        {
            // TODO : 샤딩 체크
            // 필요한 DB 연결
            
            var user = new User(id);
            var dbList = Enum.GetValues<DB>();
            
            foreach (var db in dbList)
            {
                user.RegistDB(db, new DBConnection(db));
            }

            return user;
        }
        
        public async Task<T> GetData<T>(long id)
            where T : class, IDBModel, new()
        {
            var dbTableAttr = typeof(T).GetCustomAttribute<DBTableAttribute>();

            if (dbTableAttr == null)
            {
                return null;
            }

            var dbConfig = dbTableAttr.DBType.GetConfig();
            if (dbConfig == default)
            {
                return null;
            }

            var command = $"SELECT * FROM {dbTableAttr.TableName} WHERE id={id}";
            var dataTable = new DataTable();
            
            await _connection.OpenAsync();
            
            var dataAdapter = new MySqlDataAdapter(command, _connection);
            
            await dataAdapter.FillAsync(dataTable);

            var tableColumns = new List<int>();
            foreach (DataColumn column in dataTable.Columns)
            {
                tableColumns.Add(column.ColumnName.GetHashCode());
            }
            
            var selectData = new T();
            var propertyInfos = selectData.GetType().GetProperties();

            // TODO : Row 가 여러줄일때 바인딩 방법 생각해야함
            foreach (DataRow row in dataTable.Rows)
            {
                for (var i = 0; i < tableColumns.Count; ++i)
                {
                    var columnName = tableColumns[i];
                    var data = row.ItemArray[i];
                    foreach (var propertyInfo in propertyInfos)
                    {
                        var dbColumn = propertyInfo.GetCustomAttribute<DBColumnAttribute>();
                        if (dbColumn != null && dbColumn.ColumnNameHash == columnName)
                        {
                            switch (dbColumn.ColumnType)
                            {
                            case ColumnType.NONE:
                                {
                                    propertyInfo.SetValue(selectData, data);
                                }        
                                break;
                            case ColumnType.KEY:
                                {
                                    propertyInfo.SetValue(selectData, data);
                                }         
                                break;
                            case ColumnType.PRIMARY_KEY:
                                {
                                    propertyInfo.SetValue(selectData, data);
                                } 
                                break;
                            }
                        }
                    }
                }
            }

            return selectData;
            // throw new ApiException(ResponseResult.InvalidAccessToken);
        }

        public async void Dispose()
        {
            await _connection.CloseAsync();
            _connection = null;
        }
    }
}