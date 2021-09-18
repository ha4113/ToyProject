using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using WebApiServer.Attribute;

namespace WebApiServer.DBProtocol
{
    public class User
    {
        private readonly long _id;
        private readonly Dictionary<DB, DBConnection> _dbConnections = new Dictionary<DB, DBConnection>();
        private readonly Dictionary<Table, IDBSchema> _currModel = new Dictionary<Table, IDBSchema>();
        private readonly Dictionary<Table, IReadOnlyDBSchema> _prevModel = new Dictionary<Table, IReadOnlyDBSchema>();
        
        public User(long id) { _id = id; }

        public void RegistDB(DB db, DBConnection conn)
        {
            _dbConnections.Add(db, conn);
        } 
        
        public async Task<T> Get<T>() where T : class, IDBSchema, new()
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

            if (!_currModel.TryGetValue(dbTableAttr.Table, out var model))
            {
                model = await conn.GetData<T>(_id);
                _currModel.Add(dbTableAttr.Table, model);
                _prevModel.Add(dbTableAttr.Table, model.DeepCopy());
            }

            return model as T;
        }
    }
}