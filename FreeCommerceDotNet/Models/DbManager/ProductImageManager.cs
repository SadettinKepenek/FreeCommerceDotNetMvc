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
    public class ProductImageManager : IOperations<ProductImage>, IDisposable
    {
        public bool Add(ProductImage entry)
        {
            using (SqlCommand command = new SqlCommand("sp_productsimages_insert"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                Utilities.ExecuteCommand<ProductImage>(sqlCommand, SqlCommandTypes.Insert);
                return true;
            }

        }

        public bool CheckIsExist(int id)
        {
            return Utilities.CheckIsExist("ProductsImages", "ImageId", id);

        }

        public bool Delete(int id)
        {
            string sqlQuery = "DELETE FROM ProductsImages WHERE ImageId=@id";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@id", id);
                return Utilities.ExecuteCommand<ProductImage>(sqlCommand, SqlCommandTypes.Remove);
            }
        }

        public void Dispose()
        {
        }

        public ProductImage Get(int id)
        {
            string sqlQuery = "select * from ProductsImages where ImageId = @id ";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                var images = new List<ProductImage>();
                Utilities.ExecuteCommand<ProductImage>(sqlCommand, SqlCommandTypes.Select, ref images);
                return images.First();
            }
        }

        public List<ProductImage> GetAll()
        {
            string sqlQuery = "select * from ProductsImages";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                var images = new List<ProductImage>();
                Utilities.ExecuteCommand<ProductImage>(sqlCommand, SqlCommandTypes.Select, ref images);
                return images;
            }
        }

        public int Update(ProductImage entry)
        {
            using (SqlCommand command = new SqlCommand("sp_productsimages_update"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                Utilities.ExecuteCommand<ProductImage>(sqlCommand, SqlCommandTypes.Update);
                return 0;
            }
        }
    }
}