using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.DAL.Abstract;
using FreeCommerceDotNet.Entities.Concrete;

namespace FreeCommerceDotNet.DAL.Concrete
{
    public class ReviewRepository:IReviewDal
    {
        MsSQLDatabase database = new MsSQLDatabase();
        public DBResult Delete(int id)
        {
            string query = "ReviewInsertUpdateDelete";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "DELETE");
                command.Parameters.AddWithValue("@ReviewId", id);
                DataTable datatable = database.DoQuery(command: command);
                if (datatable.Rows.Count != 0)
                {
                    return database.ReadResultFromDataTable(datatable);

                }
            }
            return null;
        }

        public DBResult Insert(Reviews entity)
        {
            string query = "ReviewInsertUpdateDelete";
            using (var connection=database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "INSERT");
                command.Parameters.AddWithValue("@CustomerId", entity.CustomerId);
                command.Parameters.AddWithValue("@ProductId", entity.ProductId);
                command.Parameters.AddWithValue("@Title", entity.Title);
                command.Parameters.AddWithValue("@Comment", entity.Text);
                command.Parameters.AddWithValue("@Rating", entity.Rating);
                command.Parameters.AddWithValue("@PublishDate", entity.Date);
                command.Parameters.AddWithValue("@LikeCount", entity.LikeCount);
                command.Parameters.AddWithValue("@DislikeCount", entity.DislikeCount);
                command.Parameters.AddWithValue("@Status", entity.Status);
                DataTable datatable=database.DoQuery(command: command);
                if (datatable.Rows.Count!=0)
                {
                    return database.ReadResultFromDataTable(datatable);

                }
            }
            
            return null;
        }

        public List<Reviews> SelectByFilter(List<DBFilter> filters)
        {
            return null;
        }

        public List<Reviews> SelectAll()
        {
            string query = "SP_GetReview";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;    
                DataTable datatable = database.DoQuery(command: command);
                if (datatable.Rows.Count != 0)
                {
                                     
                    List<Reviews> list = new List<Reviews>();                   
                    foreach (DataRow dr in datatable.Rows)
                    {
                        Reviews review = new Reviews();
                        review.customer = new Customer();
                        int reviewId = (int)dr["ReviewId"];
                        string reviewTitle = dr["ReviewTitle"].ToString();
                        string reviewComment = dr["ReviewComment"].ToString();
                        int reviewRating = (int)dr["ReviewRating"];                      
                        string reviewPublishDate = dr["ReviewPublishDate"].ToString();
                        bool reviewStatus = (bool)dr["ReviewStatus"];
                        int reviewLike = (int)dr["ReviewLike"];
                        int reviewDislike = (int)dr["ReviewDislike"];
                        int customerId = (int)dr["CustomerId"];
                        string customerFirstname = dr["CustomerFirstname"].ToString();
                        string customerLastname = dr["CustomerLastname"].ToString();
                        string customerEmail = dr["CustomerEmail"].ToString();
                        review.CustomerId = customerId;
                        review.ProductId = (int)dr["ProductId"];
                        review.customer.Firstname = customerFirstname;
                        review.customer.Lastname = customerLastname;
                        review.customer.Email = customerEmail;
                        review.ReviewId = reviewId;
                        review.Title = reviewTitle;
                        review.Text = reviewComment;
                        review.Status = reviewStatus;
                        review.Rating = reviewRating;
                        review.Date = reviewPublishDate;
                        review.LikeCount = reviewLike;
                        review.DislikeCount = reviewDislike;
                        list.Add(review);
                    }
                    return list;
                }
                
            }
            return null;
        }

        public Reviews SelectById(int id)
        {
            string query = "SP_GetReview";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query,connection);
                command.Parameters.AddWithValue("@ReviewId",id);
                DataTable datatable = database.DoQuery(command: command);
                if (datatable.Rows.Count != 0)
                {
                    Reviews reviews = new Reviews();
                    reviews.customer = new Customer();
                    reviews.ReviewId = (int)datatable.Rows[0]["ReviewId"];
                    reviews.Title = datatable.Rows[0]["ReviewTitle"].ToString();
                    reviews.Text = datatable.Rows[0]["ReviewComment"].ToString();
                    reviews.Rating = (int)datatable.Rows[0]["ReviewRating"];
                    reviews.ProductId = (int)datatable.Rows[0]["ProductId"];
                    reviews.Date = datatable.Rows[0]["ReviewPublishDate"].ToString();
                    reviews.Status = (bool)datatable.Rows[0]["ReviewStatus"];
                    reviews.LikeCount = (int)datatable.Rows[0]["ReviewLike"];
                    reviews.DislikeCount = (int)datatable.Rows[0]["ReviewDislike"];
                    reviews.CustomerId = (int)datatable.Rows[0]["CustomerId"];
                    reviews.customer.Firstname = datatable.Rows[0]["CustomerFirstname"].ToString();
                    reviews.customer.Lastname = datatable.Rows[0]["CustomerLastname"].ToString();
                    reviews.customer.Email = datatable.Rows[0]["CustomerEmail"].ToString();
                    return reviews;


                }
            }
                return null;
        }

        public DBResult Update(Reviews entity)
        {
            string query = "ReviewInsertUpdateDelete";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "UPDATE");
                command.Parameters.AddWithValue("@CustomerId", entity.CustomerId);
                command.Parameters.AddWithValue("@ProductId", entity.ProductId);
                command.Parameters.AddWithValue("@ReviewId", entity.ReviewId);
                command.Parameters.AddWithValue("@Title", entity.Title);
                command.Parameters.AddWithValue("@Comment", entity.Text);
                command.Parameters.AddWithValue("@Rating", entity.Rating);
                command.Parameters.AddWithValue("@PublishDate", entity.Date);
                command.Parameters.AddWithValue("@LikeCount", entity.LikeCount);
                command.Parameters.AddWithValue("@DislikeCount", entity.DislikeCount);
                command.Parameters.AddWithValue("@Status", entity.Status);
                DataTable datatable = database.DoQuery(command: command);
                if (datatable.Rows.Count != 0)
                {
                    return database.ReadResultFromDataTable(datatable);

                }
            }
            return null;
        }
    }
}
