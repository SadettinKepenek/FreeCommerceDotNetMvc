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
    public class PaymentsManager:IDBOperations<Payment>, IDisposable
    {
        public List<Payment> GetAll()
        {
            string sqlQuery = "select * from PaymentGateways";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                var Payments = new List<Payment>();
                Utilities.ExecuteCommand<Payment>(sqlCommand, SqlCommandTypes.Select, ref Payments);
                return Payments;
            }
        }

        public Payment Get(int id)
        {
            string sqlQuery = "select * from PaymentGateways where PaymentId=@Id ";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Id", id);
                var Payments = new List<Payment>();
                Utilities.ExecuteCommand<Payment>(sqlCommand, SqlCommandTypes.Select, ref Payments);
                return Payments.First();
            }
        }

        public int Add(Payment entry)
        {
            using (SqlCommand command = new SqlCommand("sp_payments_insert"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                return Utilities.ExecuteCommand<Payment>(sqlCommand);
            }
        }

        public int Update(Payment entry)
        {
            using (SqlCommand command = new SqlCommand("sp_payments_update"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                Utilities.ExecuteCommand<Payment>(sqlCommand, SqlCommandTypes.Insert);
                return 0;
            }
        }

        public bool Delete(int id)
        {
            string sqlQuery = "DELETE FROM PaymentGateways WHERE PaymentId=@Id";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Id", id);
                return Utilities.ExecuteCommand<Payment>(sqlCommand, SqlCommandTypes.Select);
            }
        }

        public void Dispose()
        {

        }

        public bool CheckIsExist(int id)
        {
            string sqlQuery = "SELECT COUNT(1) FROM PaymentGateways WHERE PaymentId=@Id";
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

        public List<Payment> GetByIntegerKey(int id, string tbl, string key)
        {
            return Utilities.GetByIntegerKey<Payment>(id, tbl, key);

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}