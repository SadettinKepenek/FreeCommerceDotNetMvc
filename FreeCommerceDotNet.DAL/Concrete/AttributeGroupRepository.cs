using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using FreeCommerceDotNet.DAL.Abstract;
using FreeCommerceDotNet.Entities.Concrete;

namespace FreeCommerceDotNet.DAL.Concrete
{
    public class AttributeGroupRepository:IAttributeGroupDal
    {
        string connectionString = "server=94.73.144.8;Database=u8206796_dbF1B;User Id=u8206796_userF1B;Password=SPlt16S0;";
        public DBResult Insert(AttributeGroup entity)
        {
            string query = "AttributeGroupInsertUpdateDelete";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command=new SqlCommand(query,connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "INSERT");
                command.Parameters.AddWithValue("@AttributeGroupName", entity.AttributeGroupName);

                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                try
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int Id = (int) reader[0];
                        string message = reader[1] as string;
                        Debug.WriteLine(message);
                        return new DBResult(){Id = Id,Message = message};
                    }

                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.StackTrace);
                }

            }

            return null;
        }

        public DBResult Update(AttributeGroup entity)
        {
            string query = "AttributeGroupInsertUpdateDelete";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "UPDATE");
                command.Parameters.AddWithValue("@AttributeGroupName", entity.AttributeGroupName);
                command.Parameters.AddWithValue("@AttributeGroupId", entity.AttributeGroupId);

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

        public DBResult Delete(int id)
        {
            string query = "AttributeGroupInsertUpdateDelete";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "DELETE");
                command.Parameters.AddWithValue("@AttributeGroupId", id);

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

        public AttributeGroup SelectById(int id)
        {
            return null;
        }

        public List<AttributeGroup> SelectAll()
        {
            return null;
        }
    }
}