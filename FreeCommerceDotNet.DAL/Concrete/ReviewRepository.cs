using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using FreeCommerceDotNet.DAL.Abstract;
using FreeCommerceDotNet.Entities.Concrete;

namespace FreeCommerceDotNet.DAL.Abstract
{
    public class ReviewRepository:IReviewDal
    {
        string connectionString = "server=94.73.144.8;Database=u8206796_dbF1B;User Id=u8206796_userF1B;Password=SPlt16S0;";

        public DBResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        public DBResult Insert(Reviews entity)
        {
            string query = "ReviewInsertUpdateDelete";
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query,connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action","INSERT");
                command.Parameters.AddWithValue("@CustomerId", entity.CustomerId);
                command.Parameters.AddWithValue("@ProductId", entity.ProductId);
                command.Parameters.AddWithValue("@Title", entity.Title);
                command.Parameters.AddWithValue("@Comment", entity.Text);
                command.Parameters.AddWithValue("@Rating", entity.Rating);
                command.Parameters.AddWithValue("@PublishDate", entity.Date);
                command.Parameters.AddWithValue("@LikeCount", entity.LikeCount);
                command.Parameters.AddWithValue("@DislikeCount", entity.DislikeCount);
                command.Parameters.AddWithValue("@Status", entity.Status);
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                try
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int Id = (int)reader[0];
                        string message = reader[1] as string;
                        Debug.WriteLine(message);
                        return new DBResult() { Id = Id, Message = message };
                    }

                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.StackTrace);
                }
            }
            return null;
        }

        public List<Reviews> SelectAll()
        {
            throw new NotImplementedException();
        }

        public Reviews SelectById(int id)
        {
            throw new NotImplementedException();
        }

        public DBResult Update(Reviews entity)
        {
            string query = "ReviewInsertUpdateDelete";

        }
    }
}
