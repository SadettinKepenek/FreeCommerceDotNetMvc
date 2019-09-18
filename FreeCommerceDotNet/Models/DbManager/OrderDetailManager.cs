using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using FreeCommerceDotNet.Models.DbModels;
using FreeCommerceDotNet.Models.Interfaces;
using FreeCommerceDotNet.Models.Util;

namespace FreeCommerceDotNet.Models.DbManager
{
    public class OrderDetailManager:IDBOperations<OrderDetail>
    {
        public List<OrderDetail> GetAll()
        {
            string sqlQuery = "select * from OrdersDetail";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                var Products = new List<OrderDetail>();
                Utilities.ExecuteCommand<OrderDetail>(sqlCommand, SqlCommandTypes.Select, ref Products);
                return Products;
            }
        }

        public OrderDetail Get(int id)
        {
            string sqlQuery = "select * from OrdersDetail where OrderDetailId=@Id ";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Id", id);
                var Products = new List<OrderDetail>();
                Utilities.ExecuteCommand<OrderDetail>(sqlCommand, SqlCommandTypes.Select, ref Products);
                return Products.First();
            }
        }

        public bool Add(OrderDetail entry)
        {
            using (SqlCommand command = new SqlCommand("sp_orderdetail_insert"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                Utilities.ExecuteCommand<OrderDetail>(sqlCommand, SqlCommandTypes.Insert);
                return true;
            }
        }

        public int Update(OrderDetail entry)
        {
            using (SqlCommand command = new SqlCommand("sp_orderdetail_update"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                Utilities.ExecuteCommand<OrderDetail>(sqlCommand, SqlCommandTypes.Insert);
                return 0;
            }
        }

        public bool Delete(int id)
        {
            string sqlQuery = "DELETE FROM OrdersDetail WHERE OrderDetailId=@Id";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Id", id);
                return Utilities.ExecuteCommand<OrderDetail>(sqlCommand, SqlCommandTypes.Select);
            }
        }

        public bool CheckIsExist(int id)
        {
            return Utilities.CheckIsExist("OrderMasters", "OrderDetailId", id);
        }

        public List<OrderDetail> GetByIntegerKey(int id, string tbl, string key)
        {
            return Utilities.GetByIntegerKey<OrderDetail>(id, tbl, key);

        }
    }
}