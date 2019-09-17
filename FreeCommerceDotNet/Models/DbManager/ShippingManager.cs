using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using FreeCommerceDotNet.Models.DbModels;
using FreeCommerceDotNet.Models.Interfaces;
using FreeCommerceDotNet.Models.Util;

namespace FreeCommerceDotNet.Models.DbManager
{
    public class ShippingManager:IOperations<Shipping>
    {
        public List<Shipping> GetAll()
        {
            string sqlQuery = "select * from Shippings";
            using (SqlCommand command = new SqlCommand())
            {
                var sqlCommand = command;
                var shippings = new List<Shipping>();
                Utilities.ExecuteCommand<Shipping>(sqlCommand, SqlCommandTypes.Select, ref shippings);
                return shippings;
            }
        }

        public Shipping Get(int id)
        {
            string sqlQuery = "select * from Shippings where ShippingId=@Id ";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Id", id);
                var shippings = new List<Shipping>();
                Utilities.ExecuteCommand<Shipping>(sqlCommand, SqlCommandTypes.Select, ref shippings);
                return shippings.First();
            }
        }

        public bool Add(Shipping entry)
        {
            using (SqlCommand command = new SqlCommand("sp_shipping_insert"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                Utilities.ExecuteCommand<Shipping>(sqlCommand, SqlCommandTypes.Insert);
                return true;
            }
        }

        public int Update(Shipping entry)
        {
            using (SqlCommand command = new SqlCommand("sp_shipping_update"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                Utilities.ExecuteCommand<Shipping>(sqlCommand, SqlCommandTypes.Insert);
                return 0;
            }
        }

        public bool Delete(int id)
        {
            string sqlQuery = "DELETE FROM Shippings WHERE ShippingId=@Id";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Id", id);
                return Utilities.ExecuteCommand<Shipping>(sqlCommand, SqlCommandTypes.Select);
            }
        }

        public bool CheckIsExist(int id)
        {
            string sqlQuery = "SELECT COUNT(1) FROM Shippings WHERE ShippingId=@Id";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                sqlCommand.Parameters.AddWithValue("@Id", id);
                using (SqlConnection connection = new SqlConnection(Utilities.connectionString))
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    using (command)
                    {
                        try
                        {
                            var reader=command.ExecuteReader();
                            while (reader.Read())
                            {
                                int c = (int) reader[0];
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
    }
}