using System.Data;

namespace MyLib.Extensions.Data
{
    public static class DataReaderExtensions
    {
        public static DataTable ToDataTable(this IDataReader reader)
        {
            DataTable dataTable = new DataTable();

            for (int i = 0; i < reader.FieldCount; i++)
            {
                dataTable.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
            }

            while (reader.Read())
            {
                DataRow dataRow = dataTable.NewRow();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    dataRow[i] = reader[i];
                }
                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }
    }
}
