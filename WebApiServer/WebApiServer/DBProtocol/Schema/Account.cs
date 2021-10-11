﻿using WebApiServer.Attribute;

namespace WebApiServer.DBProtocol.Schema
{
    [DBTable(DB.MAIN, Table.ACCOUNT)]
    public class Account : IDBModel
    {
        [DBColumn("id", ColumnType.PRIMARY_KEY)]
        public long Id { get; set; }
        
        [DBColumn("wake_time_type")] 
        public byte WakeTimeType { get; set; }
        
        [DBColumn("work_type")] 
        public byte WakeType { get; set; }

        public IDBModel DeepCopy()
        {
            return new Account
            {
                Id = Id, 
                WakeTimeType = WakeTimeType,
                WakeType = WakeType,
            };
        }
    }
}