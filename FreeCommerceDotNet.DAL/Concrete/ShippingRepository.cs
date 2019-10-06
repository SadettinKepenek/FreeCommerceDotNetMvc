using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.DAL.Abstract;
using FreeCommerceDotNet.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCommerceDotNet.DAL.Concrete
{
    public class ShippingRepository : IShippingDal
    {
        MsSQLDatabase database = new MsSQLDatabase();

        public DBResult Insert(Shipping entity)
        {
            string query = "ShippingInsertUpdateDelete";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "INSERT");
                command.Parameters.AddWithValue("@ShippingName", entity.ShippingName);
                command.Parameters.AddWithValue("@ShippingDescription", entity.ShippingDescription);
                DataTable datatable = database.DoQuery(command: command);
                if (datatable.Rows.Count != 0)
                {
                    DBResult result = new DBResult();
                    result.Id = (int)datatable.Rows[0]["ReturnValue"];
                    result.Message = datatable.Rows[0]["Message"].ToString();
                    return result;
                }
            }

            return null;
        }
        public DBResult Update(Shipping entity)
        {
            string query = "ShippingInsertUpdateDelete";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "UPDATE");
                command.Parameters.AddWithValue("@ShippingName", entity.ShippingName);
                command.Parameters.AddWithValue("@ShippingId", entity.ShippingId);
                command.Parameters.AddWithValue("@ShippingDescription", entity.ShippingDescription);
                DataTable datatable = database.DoQuery(command: command);
                if (datatable.Rows.Count != 0)
                {
                    DBResult result = new DBResult();
                    result.Id = (int)datatable.Rows[0]["ReturnValue"];
                    result.Message = datatable.Rows[0]["Message"].ToString();
                    return result;
                }
            }

            return null;
        }
        public DBResult Delete(int id)
        {
            string query = "ShippingInsertUpdateDelete";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "DELETE");
                command.Parameters.AddWithValue("@ShippingId", id);
                DataTable datatable = database.DoQuery(command: command);
                if (datatable.Rows.Count != 0)
                {
                    DBResult result = new DBResult();
                    result.Id = (int)datatable.Rows[0]["ReturnValue"];
                    result.Message = datatable.Rows[0]["Message"].ToString();
                    return result;
                }
            }
            return null;
        }

        public Shipping SelectById(int id)
        {
            string query = "SP_GetShipping";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                DataTable datatable = database.DoQuery(command: command);
                var rows = datatable.Rows;
                if (rows.Count != 0)
                {
                    Shipping shipping = new Shipping();
                    shipping.ShippingId = (int)rows[0]["ShippingId"];
                    shipping.ShippingName = rows[0]["ShippingName"] as string;
                    shipping.ShippingDescription = rows[0]["ShippingDescription"] as string;
                    return shipping;
                }
                return null;
            }
        }

        public List<Shipping> SelectAll()
        {
            string query = "SP_GetShipping";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                DataTable datatable = database.DoQuery(command: command);
                return GetShippings(datatable);
            }

        }

        public List<Shipping> SelectByFilter(List<DBFilter> filters)
        {
            string query = "SP_GetShipping";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                foreach (var item in filters)
                {
                    command.Parameters.AddWithValue(item.ParamName, item.ParamValue);
                }
                DataTable datatable = database.DoQuery(command: command);
                return GetShippings(datatable);
            }
        }
        private List<Shipping> GetShippings(DataTable table)
        {
            var rows = table.Rows;
            if (rows.Count != 0)
            {
                List<Shipping> shippings = new List<Shipping>();
                foreach (DataRow row in rows)
                {
                    Shipping shipping = new Shipping();
                    shipping.ShippingId = (int)row["ShippingId"];
                    shipping.ShippingName = row["ShippingName"] as string;
                    shipping.ShippingDescription = row["ShippingDescription"] as string;
                    shippings.Add(shipping);
                }
                return shippings;
            }
            return null;
        }



    }
}
