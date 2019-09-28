using FreeCommerceDotNet.Models.DbModels;
using FreeCommerceDotNet.Models.Interfaces;
using FreeCommerceDotNet.Models.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FreeCommerceDotNet.Models.DbManager
{
    public class BrandManager : IDBOperations<Brand>, IDisposable
    {
        public int Add(Brand entry)
        {
            using (SqlCommand command = new SqlCommand("sp_brand_insert"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                return Utilities.ExecuteCommand<Brand>(sqlCommand);
            }
        }

        public bool CheckIsExist(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            string sqlQuery = "DELETE FROM Brands WHERE BrandId = @id";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                sqlCommand.Parameters.AddWithValue("@id", id);
                return Utilities.ExecuteCommand<Brand>(sqlCommand, SqlCommandTypes.Remove);
            }
        }

        public void Dispose()
        {
            
        }

        public Brand Get(int id)
        {
            string query = "select * from Brands where BrandId = @id";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                var sqlCommand = cmd;
                sqlCommand.Parameters.AddWithValue("@id", id);
                var brands = new List<Brand>();
                Utilities.ExecuteCommand<Brand>(sqlCommand, SqlCommandTypes.Select, ref brands);
                return brands.FirstOrDefault();
            }
        }

        public List<Brand> GetAll()
        {
            string query = "select * from Brands";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                var sqlCommand = cmd;
                var brands = new List<Brand>();
                Utilities.ExecuteCommand<Brand>(sqlCommand, SqlCommandTypes.Select, ref brands);
                return brands;
            }
        }

        public List<Brand> GetByIntegerKey(int id, string tbl, string key)
        {
            return Utilities.GetByIntegerKey<Brand>(id, tbl, key);
        }

        public int Update(Brand entry)
        {
            using (SqlCommand command = new SqlCommand("sp_brand_update"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                Utilities.ExecuteCommand<DbModels.Users>(sqlCommand, SqlCommandTypes.Update);
                return 0;
            }
        }
    }
}