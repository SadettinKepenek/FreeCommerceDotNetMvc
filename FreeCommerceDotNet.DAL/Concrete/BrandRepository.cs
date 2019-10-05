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
            string query = "BrandInsertUpdateDelete";
            using (var conn = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "UPDATE");
                command.Parameters.AddWithValue("@BrandName", entity.BrandName);
                command.Parameters.AddWithValue("@BrandDescription", entity.BrandDescription);
                command.Parameters.AddWithValue("@BrandId", entity.BrandId);
                DataTable queryResult = database.DoQuery(command: command);
                if (queryResult.Rows.Count != 0)
                {
                    DBResult result = new DBResult()
                    {
                        Id = (int)queryResult.Rows[0]["ReturnValue"],
                        Message = queryResult.Rows[0]["Message"] as string
                    };
                    return result;
                }

            }
            return null;
        }

        public DBResult Delete(int id)
        {
            string query = "BrandInsertUpdateDelete";
            using (var conn = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "DELETE");
                command.Parameters.AddWithValue("@BrandId", id);
                DataTable queryResult = database.DoQuery(command: command);
                if (queryResult.Rows.Count != 0)
                {
                    DBResult result = new DBResult()
                    {
                        Id = (int)queryResult.Rows[0]["ReturnValue"],
                        Message = queryResult.Rows[0]["Message"] as string
                    };
                    return result;
                }

            }
            return null;
        }

        public Brand SelectById(int id)
        {
            string query = "SP_GetBrand";
            using (var conn=database.CreateConnection())
            {
                SqlCommand command=new SqlCommand(query,conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@BrandId", id);
                DataTable queryResult = database.DoQuery(command: command);
                if (queryResult.Rows.Count!=0)
                {
                    DataRow row = queryResult.Rows[0];
                    Brand brand=new Brand();
                    brand.BrandId = (int) row["BrandId"];
                    brand.BrandName = row["BrandName"].ToString();
                    brand.BrandDescription = row["BrandDescription"].ToString();
                    brand.BrandUrl = row["BrandUrl"].ToString();
                    brand.BrandImageUrl= row["BrandImageUrl"].ToString();
                    return brand;
                }
            }
            return null;
        }

        public List<Brand> SelectAll()
        {
            string query = "SP_GetBrand";
            using (var conn = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                DataTable queryResult = database.DoQuery(command: command);
                if (queryResult.Rows.Count != 0)
                {
                    List<Brand> brands=new List<Brand>();
                    foreach (DataRow row in queryResult.Rows)
                    {
                        Brand brand = new Brand();
                        brand.BrandId = (int)row["BrandId"];
                        brand.BrandName = row["BrandName"].ToString();
                        brand.BrandDescription = row["BrandDescription"].ToString();
                        brand.BrandUrl = row["BrandUrl"].ToString();
                        brand.BrandImageUrl = row["BrandImageUrl"].ToString();
                        brands.Add(brand);
                    }
                   
                    return brands;
                }
            }
            return null;
        }
    }
}