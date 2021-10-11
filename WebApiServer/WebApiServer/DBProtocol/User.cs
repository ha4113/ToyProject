using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using WebApiServer.Attribute;

namespace WebApiServer.DBProtocol
{
    public class User : IDisposable
    {
        private readonly long _id;
        private readonly Dictionary<DB, DBConnection> _dbConnections = new Dictionary<DB, DBConnection>();
        
        public User(long id) { _id = id; }

        public void RegistDB(DB db, DBConnection conn)
        {
            _dbConnections.Add(db, conn);
        } 
        
        public async Task<T> GetModel<T>() where T : class, IDBModel, new()
        {
            var dbTableAttr = typeof(T).GetCustomAttribute<DBTable>();
            if (dbTableAttr == null)
            {
                return null;
            }

            if (false == _dbConnections.TryGetValue(dbTableAttr.DBType, out var conn))
            {
                return null;
            }

            return await conn.GetData<T>(_id);
        }

        public async Task CommitAsync()
        {
            
        }
        
        public void Dispose()
        {
            foreach (var kvp in _dbConnections)
            {
                kvp.Value.Dispose();
            }
            
            _dbConnections.Clear();
        }
    }
}