using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.DAL.Abstract;
using FreeCommerceDotNet.Entities.Concrete;

namespace FreeCommerceDotNet.DAL.Concrete
{
    public class WishlistRepository:IWishlistDal
    {
        MsSQLDatabase db=new MsSQLDatabase();
        string crudQuery = "WishlistInsertUpdateDelete";

        public DBResult Insert(Wish entity)
        {
            using (var con=db.CreateConnection())
            {
                SqlCommand command=new SqlCommand(crudQuery,con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductId", entity.ProductId);
                command.Parameters.AddWithValue("@Action", "INSERT");
                command.Parameters.AddWithValue("@CustomerId", entity.CustomerId);
                command.Parameters.AddWithValue("@WishDate", entity.WishDate);
                return db.ReadResultFromDataTable(db.DoQuery(command: command));
            }
        }

        public DBResult Update(Wish entity)
        {
            using (var con = db.CreateConnection())
            {
                SqlCommand command = new SqlCommand(crudQuery, con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "UPDATE");
                command.Parameters.AddWithValue("@WishlistId", entity.WishId);
                command.Parameters.AddWithValue("@ProductId", entity.ProductId);
                command.Parameters.AddWithValue("@CustomerId", entity.CustomerId);
                command.Parameters.AddWithValue("@WishDate", entity.WishDate);
                return db.ReadResultFromDataTable(db.DoQuery(command: command));
            }
        }

        public DBResult Delete(int id)
        {
            using (var con = db.CreateConnection())
            {
                SqlCommand command = new SqlCommand(crudQuery, con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "DELETE");
                command.Parameters.AddWithValue("@WishlistId",id);
                return db.ReadResultFromDataTable(db.DoQuery(command: command));
            }
        }

        public Wish SelectById(int id)
        {
            string query = "SP_GetWishlist";

            using (var con = db.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@WishlistId", id);
                var queryResult = db.DoQuery(command: command);
                if (queryResult.Rows.Count!=0)
                {
                    var dr = queryResult.Rows[0];
                    return ParseWishList(dr);
                }

            }
            return null;
        }


        public List<Wish> SelectByFilter(List<DBFilter> filters)
        {
            string query = "SP_GetWishlist";
            using (var con = db.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, con);
                command.CommandType = CommandType.StoredProcedure;
                foreach (DBFilter filter in filters)
                {
                    command.Parameters.AddWithValue(filter.ParamName, filter.ParamValue);
                }
                var queryResult = db.DoQuery(command: command);
                if (queryResult.Rows.Count != 0)
                {
                    List<Wish> wishes=new List<Wish>();
                    foreach (DataRow row in queryResult.Rows)
                    {
                        wishes.Add(ParseWishList(row));
                    }

                    return wishes;
                }

            }
            return null;
        }

        public List<Wish> SelectAll()
        {
            string query = "SP_GetWishlist";

            using (var con = db.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, con);
                command.CommandType = CommandType.StoredProcedure;
                var queryResult = db.DoQuery(command: command);
                if (queryResult.Rows.Count != 0)
                {
                    List<Wish> wishes = new List<Wish>();
                    foreach (DataRow row in queryResult.Rows)
                    {
                        wishes.Add(ParseWishList(row));
                    }

                    return wishes;
                }

            }
            return null;
        }

        private static Wish ParseWishList(DataRow dr)
        {
            Wish wish = new Wish();
            wish.CustomerId = (int)dr["CustomerId"];
            wish.WishId = (int)dr["WhisId"];
            wish.ProductId = (int)dr["ProductId"];
            wish.WishDate = dr["WishDate"] as string;
            wish.customer = new Customer() { CustomerId = wish.CustomerId };
            wish.product = new Product()
            {
                ProductId = (int)dr["ProductId"],
                ProductName = dr["ProductName"] as string,
                ImageUrl = dr["ProductImageUrl"] as string,
                Quantity = (int)dr["ProductQuantity"],
                Status = (bool)dr["ProductStatus"]
            };
            return wish;
        }

    }
}