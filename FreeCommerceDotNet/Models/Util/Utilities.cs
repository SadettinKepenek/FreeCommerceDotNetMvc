using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Web.Script.Serialization;

namespace FreeCommerceDotNet.Models.Util
{
    public static class Utilities
    {
        public static string connectionString { get; set; } =
            "\"Server=94.73.144.8;Database=u8206796_dbF1B;User Id=u8206796_userF1B;Password=SPlt16S0;\"";

        public static T FromJson<T>(string jsonObject)
        {
            return new JavaScriptSerializer().Deserialize<T>(jsonObject); ;
        }
        public static string ToJson<T>(T entry)
        {
            var json = new JavaScriptSerializer().Serialize(entry);
            return json;
        }
        public static bool ExecuteCommand<T>(SqlCommand command, SqlCommandTypes type, ref List<T> emptyList) where T : new()
        {
            /// emptyList eğer select yapılacaksa boş liste olarak gönderilir select sonucunda doldurulur.
            using (SqlConnection connection = new SqlConnection(Utilities.connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                using (command)
                {
                    if (type == SqlCommandTypes.Insert || type == SqlCommandTypes.Update || type == SqlCommandTypes.Remove)
                    {
                        try
                        {
                            command.ExecuteNonQuery();
                            return true;

                        }
                        catch (Exception e)
                        {
                            return false;
                        }
                    }

                    else
                    {
                        try
                        {
                            var reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                T entry = fromReader<T>(reader);
                                emptyList.Add(entry);
                            }

                            return true;
                        }
                        catch (Exception e)
                        {
                            return false;
                        }

                    }
                }
            }
        }
        public static bool ExecuteCommand<T>(SqlCommand command, SqlCommandTypes type) where T : new()
        {
            using (SqlConnection connection = new SqlConnection(Utilities.connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                using (command)
                {

                    try
                    {
                        command.ExecuteNonQuery();
                        return true;

                    }
                    catch (Exception e)
                    {
                        return false;
                    }

                }
            }
        }

        public static bool ColumnExists(SqlDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
        public static T fromReader<T>(SqlDataReader reader) where T : new()
        {
            T entry = new T();
            foreach (PropertyInfo info in entry.GetType().GetProperties())
            {
                string propertyName = info.Name;
                if (ColumnExists(reader, propertyName) == false)
                    continue;

                var value = reader[propertyName];
                if (value != null && value != DBNull.Value)
                {

                    if (info.PropertyType == typeof(Double))
                        info.SetValue(entry, Convert.ToDouble(reader[propertyName]));
                    else
                        info.SetValue(entry, reader[propertyName]);

                }
            }
            return entry;
        }
        public static SqlCommand CreateUpdateSqlParameters<T>(SqlCommand cmd, T entry, PropertyInfo[] propertyInfos)
        {
            foreach (PropertyInfo info in propertyInfos)
            {
                string parameterName = "@" + info.Name;
                cmd.Parameters.AddWithValue(parameterName, info.GetValue(entry));
            }

            return cmd;
        }
    }
}