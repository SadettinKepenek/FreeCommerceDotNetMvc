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
    public class ReviewsManager:IDBOperations<Reviews>, IDisposable
    {
        public List<Reviews> GetAll()
        {
            string sqlQuery = "select * from Reviews";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                var Products = new List<Reviews>();
                Utilities.ExecuteCommand<Reviews>(sqlCommand, SqlCommandTypes.Select, ref Products);
                return Products;
            }
        }

        public Reviews Get(int id)
        {
            string sqlQuery = "select * from Reviews where ReviewId=@Id ";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Id", id);
                var Products = new List<Reviews>();
                Utilities.ExecuteCommand<Reviews>(sqlCommand, SqlCommandTypes.Select, ref Products);
                return Products.First();
            }
        }

        public bool Add(Reviews entry)
        {
            using (SqlCommand command = new SqlCommand("sp_review_insert"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                Utilities.ExecuteCommand<Reviews>(sqlCommand, SqlCommandTypes.Insert);
                return true;
            }
        }

        public int Update(Reviews entry)
        {
            using (SqlCommand command = new SqlCommand("sp_review_update"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                Utilities.ExecuteCommand<Reviews>(sqlCommand, SqlCommandTypes.Insert);
                return 0;
            }
        }

        public bool Delete(int id)
        {
            string sqlQuery = "DELETE FROM Reviews WHERE ReviewId=@Id";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Id", id);
                return Utilities.ExecuteCommand<Reviews>(sqlCommand, SqlCommandTypes.Select);
            }
        }

        public bool CheckIsExist(int id)
        {
            return Utilities.CheckIsExist("Reviews", "ReviewId", id);
        }

        public List<Reviews> GetByIntegerKey(int id, string tbl, string key)
        {
            return Utilities.GetByIntegerKey<Reviews>(id, tbl, key);

        }

        public void Dispose()
        {
        }
    }
}