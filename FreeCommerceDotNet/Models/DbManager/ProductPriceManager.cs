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
    public class ProductPriceManager:IDBOperations<ProductPrice>, IDisposable
    {

        public List<ProductPrice> GetAll()
        {
            string sqlQuery = "select * from ProductsPrices";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                var Products = new List<ProductPrice>();
                Utilities.ExecuteCommand<ProductPrice>(sqlCommand, SqlCommandTypes.Select, ref Products);
                return Products;
            }
        }

        public ProductPrice Get(int id)
        {
            string sqlQuery = "select * from ProductsPrices where PriceId=@Id ";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Id", id);
                var Products = new List<ProductPrice>();
                Utilities.ExecuteCommand<ProductPrice>(sqlCommand, SqlCommandTypes.Select, ref Products);
                return Products.First();
            }
        }

        public int Add(ProductPrice entry)
        {
            using (SqlCommand command = new SqlCommand("sp_productprice_insert"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                return Utilities.ExecuteCommand<ProductPrice>(sqlCommand);
                
            }
        }

        public int Update(ProductPrice entry)
        {
            using (SqlCommand command = new SqlCommand("sp_productprice_update"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                Utilities.ExecuteCommand<ProductPrice>(sqlCommand, SqlCommandTypes.Insert);
                return 0;
            }
        }

        public bool Delete(int id)
        {
            string sqlQuery = "DELETE FROM ProductsPrices WHERE PriceId=@Id";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Id", id);
                return Utilities.ExecuteCommand<ProductPrice>(sqlCommand, SqlCommandTypes.Select);
            }
        }

        public bool CheckIsExist(int id)
        {
            return Utilities.CheckIsExist("ProductPrices", "PriceId", id);
        }

        public List<ProductPrice> GetByIntegerKey(int id, string tbl, string key)
        {
            return Utilities.GetByIntegerKey<ProductPrice>(id, tbl, key);

        }

        public void Dispose()
        {
        }
    }
}