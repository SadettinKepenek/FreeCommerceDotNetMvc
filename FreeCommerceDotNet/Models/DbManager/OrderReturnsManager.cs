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
    public class OrderReturnsManager:IDBOperations<OrderReturn>, IDisposable
    {
        public List<OrderReturn> GetAll()
        {
            string sqlQuery = "select * from OrdersReturns";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                var Products = new List<OrderReturn>();
                Utilities.ExecuteCommand<OrderReturn>(sqlCommand, SqlCommandTypes.Select, ref Products);
                return Products;
            }
        }

        public OrderReturn Get(int id)
        {
            string sqlQuery = "select * from OrdersReturns where ReturnId=@Id ";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Id", id);
                var Products = new List<OrderReturn>();
                Utilities.ExecuteCommand<OrderReturn>(sqlCommand, SqlCommandTypes.Select, ref Products);
                return Products.First();
            }
        }

        public int Add(OrderReturn entry)
        {
            using (SqlCommand command = new SqlCommand("sp_orderreturn_insert"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
               return Utilities.ExecuteCommand<OrderReturn>(sqlCommand);
            }
        }

        public int Update(OrderReturn entry)
        {
            using (SqlCommand command = new SqlCommand("sp_orderreturn_update"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                Utilities.ExecuteCommand<OrderReturn>(sqlCommand, SqlCommandTypes.Insert);
                return 0;
            }
        }

        public bool Delete(int id)
        {
            string sqlQuery = "DELETE FROM OrdersReturns WHERE ReturnId=@Id";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Id", id);
                return Utilities.ExecuteCommand<OrderReturn>(sqlCommand, SqlCommandTypes.Select);
            }
        }

        public bool CheckIsExist(int id)
        {
            return Utilities.CheckIsExist("OrdersReturns", "ReturnId", id);
        }

        public List<OrderReturn> GetByIntegerKey(int id, string tbl, string key)
        {
            return Utilities.GetByIntegerKey<OrderReturn>(id, tbl, key);

        }

        public void Dispose()
        {
        }
    }
}