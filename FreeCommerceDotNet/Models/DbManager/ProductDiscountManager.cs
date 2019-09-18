using FreeCommerceDotNet.Models.DbModels;
using FreeCommerceDotNet.Models.Interfaces;
using FreeCommerceDotNet.Models.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Data;
namespace FreeCommerceDotNet.Models.DbManager
{
    public class ProductDiscountManager : IDBOperations<ProductDiscount>, IDisposable
    {
        public bool Add(ProductDiscount entry)
        {
            using (SqlCommand command = new SqlCommand("sp_productsdiscounts_insert"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                Utilities.ExecuteCommand<ProductDiscount>(sqlCommand, SqlCommandTypes.Insert);
                return true;
            }

        }

        public bool CheckIsExist(int id)
        {
            return Utilities.CheckIsExist("ProductsDiscounts", "DiscountId", id);

        }

        public List<ProductDiscount> GetByIntegerKey(int id, string tbl, string key)
        {
            return Utilities.GetByIntegerKey<ProductDiscount>(id, tbl, key);

        }

        public bool Delete(int id)
        {
            string sqlQuery = "DELETE FROM ProductsDiscounts WHERE DiscountId=@id";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@id", id);
                return Utilities.ExecuteCommand<ProductDiscount>(sqlCommand, SqlCommandTypes.Remove);
            }
        }

        public void Dispose()
        {
        }

        public ProductDiscount Get(int id)
        {
            string sqlQuery = "select * from ProductsDiscounts where DiscountId = @id ";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                var discounts = new List<ProductDiscount>();
                Utilities.ExecuteCommand<ProductDiscount>(sqlCommand, SqlCommandTypes.Select, ref discounts);
                return discounts.First();
            }
        }

        public List<ProductDiscount> GetAll()
        {
            string sqlQuery = "select * from ProductsDiscounts";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                var discounts = new List<ProductDiscount>();
                Utilities.ExecuteCommand<ProductDiscount>(sqlCommand, SqlCommandTypes.Select, ref discounts);
                return discounts;
            }
        }

        public int Update(ProductDiscount entry)
        {
            using (SqlCommand command = new SqlCommand("sp_productsdiscounts_update"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                Utilities.ExecuteCommand<ProductDiscount>(sqlCommand, SqlCommandTypes.Update);
                return 0;
            }
        }
    }
}