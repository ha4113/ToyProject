using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;

namespace Common.Core.Table.Util
{
    public static class TableReader
    {
        public const string TABLE_PATH = "TableData/";
        private const string TABLE_FORMAT = "*.csv";
        
        public static void Read(string tablePath)
        {
            var tableTypes = typeof(ITable).Assembly.GetTypes();
            foreach (var tableType in tableTypes)
            {
                if (!tableType.IsClass || tableType.GetInterface(nameof(ITable)) == null)
                {
                    continue;
                }

                var tables = Directory.GetFiles(tablePath, $"{tableType.Name}{TABLE_FORMAT}", SearchOption.AllDirectories);
                
                foreach (var fileName in tables)
                {
                    using var reader = new StreamReader(fileName);
                    using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                    var records = csv.GetRecords(tableType);
                    Parallel.ForEach(records, record =>
                    {
                        if (record is ITable row)
                        {
                            row.Build();
                        }
                    });
                }
            }
        }
    }
}