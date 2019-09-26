using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web.Script.Serialization;

namespace FreeCommerceDotNet.Models.Util
{
    public enum SqlCommandTypes
    {
        Insert,
        Update,
        Remove,
        Select,
        StoredProcedure,
        Check
    }
    public static class Utilities
    {
        public static string connectionString { get; set; } = "server=94.73.144.8;Database=u8206796_dbF1B;User Id=u8206796_userF1B;Password=SPlt16S0;";

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
                    command.Connection = connection;
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
                            Debug.WriteLine(e.StackTrace);
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
                    command.Connection = connection;

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
        public static int ExecuteCommand<T>(SqlCommand command) where T : new()
        {
            using (SqlConnection connection = new SqlConnection(Utilities.connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                using (command)
                {
                    command.Connection = connection;

                    try
                    {


                        int modified = (int)command.ExecuteScalar();
                        return modified;

                    }
                    catch (Exception e)
                    {
                        return -1;
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
                        info.SetValue(entry, Convert.ToDouble(reader[propertyName].ToString()));
                    else if (info.PropertyType == typeof(Boolean))
                        info.SetValue(entry, Convert.ToBoolean(reader[propertyName]));
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
        public static bool CheckIsExist(string tbl, string pk, int pkValue)
        {
            string sqlQuery = "SELECT COUNT(1) FROM " + tbl + " WHERE " + pk + "=@Id";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                sqlCommand.Parameters.AddWithValue("@Id", pkValue);
                using (SqlConnection connection = new SqlConnection(Utilities.connectionString))
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    using (command)
                    {
                        try
                        {
                            var reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                int c = (int)reader[0];
                                return c != 0;
                            }

                        }
                        catch (Exception e)
                        {
                            return false;
                        }

                    }
                }
            }
            return true;
        }
        public static List<T> GetByIntegerKey<T>(int id, string tbl, string key) where T : new()
        {
            string sqlQuery = "select * from " + tbl + " where " + key + "=@Id ";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                sqlCommand.Parameters.AddWithValue("@Id", id);
                var Entries = new List<T>();
                Utilities.ExecuteCommand<T>(sqlCommand, SqlCommandTypes.Select, ref Entries);
                return Entries;
            }
        }

        public static bool CheckIsRemovable<T>(T entry)
        {

            return false;
        }

        public static List<DbForeignKeyModel> GetTablesForeignKeys(string tblName)
        {
            var list = new List<DbForeignKeyModel>();
            SqlCommand func = new SqlCommand("select * from dbo.getTablesForeignKeys(@tableName)");
            using (SqlConnection connection = new SqlConnection(Utilities.connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                using (func)
                {
                    func.Connection = connection;
                    func.Parameters.AddWithValue("@tableName", tblName);
                    var reader = func.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            list.Add(new DbForeignKeyModel() { tblName = reader[0].ToString(), colName = reader[1].ToString() });
                        }
                    }


                }
            }
            return list;
        }
        public static DeleteResponseModel isRemovable(string tblName, int id)
        {
            var booleanList = new List<bool>();
            var foreignKeys = GetTablesForeignKeys(tblName);
            DeleteResponseModel deleteResponseModel=new DeleteResponseModel();
            foreach (DbForeignKeyModel keyModel in foreignKeys)
            {
                string sqlQuery = "Select COUNT(*) from "+keyModel.tblName+" where "+keyModel.colName+"=@id";
                SqlCommand cmd = new SqlCommand(sqlQuery);
                cmd.Parameters.AddWithValue("@tableName", keyModel.tblName);
                //string --> nvarchar ''
                // int --> 3
                cmd.Parameters.AddWithValue("@colName", keyModel.colName);
                cmd.Parameters.AddWithValue("@id", id);

                using (SqlConnection connection = new SqlConnection(Utilities.connectionString))
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    using (cmd)
                    {
                        cmd.Connection = connection;
                        var result = cmd.ExecuteScalar();
                        int count = Convert.ToInt32(result);
                        var message = string.Format("{0} Tablosunda {1} columnu için {2} id kayıdı sonucu  Count : {3} Result {4}", keyModel.tblName, keyModel.colName, id.ToString(),count.ToString(),count!=0);
                        if (count != 0) deleteResponseModel.msg.Add(message);
                        booleanList.Add(count!=0);
                    }
                }
            }

            deleteResponseModel.removable = booleanList.All(x => x == false);
            return deleteResponseModel;
        }
    }
}