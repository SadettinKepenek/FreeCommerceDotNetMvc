using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.DAL.Abstract;
using FreeCommerceDotNet.Entities.Concrete;

namespace FreeCommerceDotNet.DAL.Concrete
{
    public class BrandRepository:IBrandDal
    {
        MsSQLDatabase database=new MsSQLDatabase();
        public DBResult Insert(Brand entity)
        {
            string query = "BrandInsertUpdateDelete";
            using (var conn=database.CreateConnection())
            {
                SqlCommand command=new SqlCommand(query,conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "INSERT");
                command.Parameters.AddWithValue("@BrandName", entity.BrandName);
                command.Parameters.AddWithValue("@BrandDescription", entity.BrandDescription);
                DataTable queryResult = database.DoQuery(command: command);
                if (queryResult.Rows.Count!=0)
                {
                    DBResult result=new DBResult()
                    {
                        Id = (int) queryResult.Rows[0]["ReturnValue"],
                        Message = queryResult.Rows[0]["Message"] as string
                    };
                    return result;
                }

            }
            return null;
        }

        public DBResult Update(Brand entity)
        {
            return null;
        }

        public DBResult Delete(int id)
        {
            return null;
        }

        public Brand SelectById(int id)
        {
            return null;
        }

        public List<Brand> SelectAll()
        {
            return null;
        }
    }
}