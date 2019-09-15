using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace FreeCommerceDotNet.Models
{
    public class DbManager
    {
        SqlConnection connection = new SqlConnection("Server=94.73.144.8;Database=u8206796_dbF1B;User Id=u8206796_userF1B;Password=SPlt16S0;");
        public void GetProducts() {
            connection.Open();
            SqlCommand command=new SqlCommand("select * from Products",connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    Debug.WriteLine(reader["ProductId"]);
                }
            }
            connection.Close();
        }
        
    }
}