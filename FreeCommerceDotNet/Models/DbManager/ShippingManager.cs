using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using FreeCommerceDotNet.Models.DbModels;
using FreeCommerceDotNet.Models.Interfaces;
using FreeCommerceDotNet.Models.Util;

namespace FreeCommerceDotNet.Models.DbManager
{
    public class ShippingManager:IOperations<Shipping>
    {
        public List<Shipping> GetAll()
        {
            string sqlQuery = "select * from Shippings";
            using (SqlCommand command = new SqlCommand())
            {
                var sqlCommand = command;
                var shippings = new List<Shipping>();
                Utilities.ExecuteCommand<Shipping>(sqlCommand, SqlCommandTypes.Select, ref shippings);
                return shippings;
            }
        }

        public Shipping Get(int id)
        {
            string sqlQuery = "select * from Shippings where ShippingId=@Id ";
            using (SqlCommand command = new SqlCommand())
            {
                var sqlCommand = command;
                sqlCommand.Parameters.AddWithValue("@Id", id);
                var shippings = new List<Shipping>();
                Utilities.ExecuteCommand<Shipping>(sqlCommand, SqlCommandTypes.Select, ref shippings);
                return shippings.First();
            }
        }

        public bool Add(Shipping entry)
        {

            throw new System.NotImplementedException();
        }

        public int Update(Shipping entry)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool CheckIsExist(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}