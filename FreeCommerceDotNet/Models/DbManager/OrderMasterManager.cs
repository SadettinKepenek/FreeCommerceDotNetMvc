using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using FreeCommerceDotNet.Models.DbModels;
using FreeCommerceDotNet.Models.Interfaces;
using FreeCommerceDotNet.Models.Util;

namespace FreeCommerceDotNet.Models.DbManager
{
    public class OrderMasterManager:IOperations<OrderMaster>
    {
        public List<OrderMaster> GetAll()
        {
            string sqlQuery = "select * from OrdersMaster";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                var Products = new List<OrderMaster>();
                Utilities.ExecuteCommand<OrderMaster>(sqlCommand, SqlCommandTypes.Select, ref Products);
                return Products;
            }
        }

        public OrderMaster Get(int id)
        {
            string sqlQuery = "select * from OrdersMaster where OrderId=@Id ";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Id", id);
                var Products = new List<OrderMaster>();
                Utilities.ExecuteCommand<OrderMaster>(sqlCommand, SqlCommandTypes.Select, ref Products);
                return Products.First();
            }
        }

        public bool Add(OrderMaster entry)
        {
            using (SqlCommand command = new SqlCommand("sp_ordermaster_insert"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                Utilities.ExecuteCommand<OrderMaster>(sqlCommand, SqlCommandTypes.Insert);
                return true;
            }
        }

        public int Update(OrderMaster entry)
        {
            using (SqlCommand command = new SqlCommand("sp_ordermaster_update"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                Utilities.ExecuteCommand<OrderMaster>(sqlCommand, SqlCommandTypes.Insert);
                return 0;
            }
        }

        public bool Delete(int id)
        {
            string sqlQuery = "DELETE FROM OrdersMaster WHERE OrderId=@Id";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Id", id);
                return Utilities.ExecuteCommand<OrderMaster>(sqlCommand, SqlCommandTypes.Select);
            }
        }

        public bool CheckIsExist(int id)
        {
            return Utilities.CheckIsExist("OrderMasters", "OrderId", id);
        }
    }
}