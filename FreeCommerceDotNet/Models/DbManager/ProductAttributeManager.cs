using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using FreeCommerceDotNet.Models.DbModels;
using FreeCommerceDotNet.Models.Interfaces;
using FreeCommerceDotNet.Models.Util;

namespace FreeCommerceDotNet.Models.DbManager
{
    public class ProductAttributeManager:IOperations<ProductAttribute>
    {
        public List<ProductAttribute> GetAll()
        {
            string sqlQuery = "select * from ProductsAttributes";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                var Products = new List<ProductAttribute>();
                Utilities.ExecuteCommand<ProductAttribute>(sqlCommand, SqlCommandTypes.Select, ref Products);
                return Products;
            }
        }

        public ProductAttribute Get(int id)
        {
            string sqlQuery = "select * from ProductsAttributes where RelationId=@Id ";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Id", id);
                var Products = new List<ProductAttribute>();
                Utilities.ExecuteCommand<ProductAttribute>(sqlCommand, SqlCommandTypes.Select, ref Products);
                return Products.First();
            }
        }

        public bool Add(ProductAttribute entry)
        {
            using (SqlCommand command = new SqlCommand("sp_productattribute_insert"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                Utilities.ExecuteCommand<ProductAttribute>(sqlCommand, SqlCommandTypes.Insert);
                return true;
            }
        }

        public int Update(ProductAttribute entry)
        {
            using (SqlCommand command = new SqlCommand("sp_productattribute_update"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                Utilities.ExecuteCommand<ProductAttribute>(sqlCommand, SqlCommandTypes.Insert);
                return 0;
            }
        }

        public bool Delete(int id)
        {
            string sqlQuery = "DELETE FROM ProductsAttributes WHERE RelationId=@Id";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Id", id);
                return Utilities.ExecuteCommand<ProductAttribute>(sqlCommand, SqlCommandTypes.Select);
            }
        }

        public bool CheckIsExist(int id)
        {
            return Utilities.CheckIsExist("ProductAttributes", "PriceId", id);
        }
    }
}