using System;
using System.ComponentModel;

namespace TestServer
{
    public interface IRowData
    {
    }

    public class MoneyRowData : IRowData
    {
        public int id { get; set; }
        public string unique_name { get; set; }
        public int money_count { get; set; }

        public override string ToString()
        {
            return $"Id = {id} / UniqueName = {unique_name} / MoneyCount = {money_count}";
        }
    }
    //public class ItemRowData : IRowData
    //{
    //    public int id { get; set; }
    //    public string unique_name { get; set; }
    //    public int item_count { get; set; }

    //    public override string ToString()
    //    {
    //        return $"Id = {id} / UniqueName = {unique_name} / ItemCount = {item_count}";
    //    }
    //}
}
