using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CsvHelper;

namespace Common.Core.Table.Util
{
    public static class TableReader
    {
        private const string TABLE_FORMAT = "*.csv";
        
        public static void Read(string tablePath, TableReadCategory readCategory)
        {
            var csvTypes = typeof(ICsv).Assembly.GetTypes();
            foreach (var csvType in csvTypes)
            {
                if (!csvType.IsClass || csvType.GetInterfaces().All(i => i != typeof(ICsv)))
                {
                    continue;
                }

                var tableInfoAttr = csvType.GetCustomAttribute<TableInfoAttribute>();
                if (tableInfoAttr == null)
                {
                    continue;
                }

                if ((readCategory & tableInfoAttr.TableReadCategory) == 0)
                {
                    continue;
                }
                
                var tableName = tableInfoAttr.TableName;
                var tables = Directory.GetFiles(tablePath, $"{tableName}{TABLE_FORMAT}", SearchOption.AllDirectories);
                
                foreach (var fileName in tables)
                {
                    using var reader = new StreamReader(fileName);
                    using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                    var records = csv.GetRecords(csvType);
                    Parallel.ForEach(records, record =>
                    {
                        if (record is ICsv row)
                        {
                            row.Build();
                        }
                    });
                }
            }

            var tableTypes = typeof(ITable).Assembly.GetTypes();
            var generic = typeof(Table<>);
            var validCheckMethods = new List<MethodInfo>();
            foreach (var tableType in tableTypes)
            {
                if (!tableType.IsClass || tableType.GetInterfaces().All(i => i != typeof(ITable)))
                {
                    continue;
                }
                
                var typeArgs = new[]{ tableType };
                var constructed = generic.MakeGenericType(typeArgs);
                var postProcessMethod = constructed.GetMethod("PostProcess", BindingFlags.NonPublic | BindingFlags.Static);
                var validCheckMethod = constructed.GetMethod("ValidCheck", BindingFlags.NonPublic | BindingFlags.Static);
                
                if (postProcessMethod == null || validCheckMethod == null)
                {
                    continue;
                }

                postProcessMethod.Invoke(null, null);
                validCheckMethods.Add(validCheckMethod);
            }
            
            foreach (var method in validCheckMethods)
            {
                method.Invoke(null, null);
            }
        }
    }
}