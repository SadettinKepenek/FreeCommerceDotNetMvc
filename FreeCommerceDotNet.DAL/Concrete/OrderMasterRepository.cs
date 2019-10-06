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
    public class OrderMasterRepository : IOrderMasterDal
    {
        MsSQLDatabase database = new MsSQLDatabase();
        public DBResult Delete(int id)
        {
            string query = "OrderMasterInsertUpdateDelete";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "DELETE");
                command.Parameters.AddWithValue("@OrderId", id);
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

        public DBResult Insert(OrderMaster entity)
        {
            string query = "OrderMasterInsertUpdateDelete";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "INSERT");
                command.Parameters.AddWithValue("@OrderDate", entity.OrderDate);
                command.Parameters.AddWithValue("@CustomerId", entity.CustomerId);
                command.Parameters.AddWithValue("@PaymentGatewayId", entity.PaymentGatewayId);
                command.Parameters.AddWithValue("@ShippingId", entity.ShippingId);
                command.Parameters.AddWithValue("@TrackNumber", entity.TrackNumber);
                command.Parameters.AddWithValue("@DeliveryDate", entity.DeliveryDate);
                command.Parameters.AddWithValue("@DeliveryComment", entity.DeliveryComment);
                command.Parameters.AddWithValue("@DeliveryStatus", entity.DeliveryStatus);
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

        public List<OrderMaster> SelectAll()
        {
            string query = "SP_GetOrder";

            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                DataTable datatable = database.DoQuery(command: command);
                List<OrderMaster> ordermaster = new List<OrderMaster>();
                if (datatable.Rows.Count != 0)
                {
                    var groupedData = LinqHelper.GroupDataTableByField<int>(queryResult: datatable, fieldName:"OrderId");
                    foreach (var group in groupedData)
                    {
                        ordermaster.Add(GetOrderMaster(group));
                    }
                    return ordermaster;
                }
            }
            return null;
        }

        public List<OrderMaster> SelectByFilter(List<DBFilter> filters)
        {
            string query = "SP_GetOrder";

            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                foreach (var item in filters)
                {
                    command.Parameters.AddWithValue(item.ParamName,item.ParamValue);
                }
                
                DataTable datatable = database.DoQuery(command: command);
                List<OrderMaster> ordermaster = new List<OrderMaster>();
                if (datatable.Rows.Count != 0)
                {
                    var groupedData = LinqHelper.GroupDataTableByField<int>(queryResult: datatable, fieldName:"OrderId");
                    foreach (var group in groupedData)
                    {
                        ordermaster.Add(GetOrderMaster(group));
                    }
                    return ordermaster;
                }
            }
            return null;
        }

        public OrderMaster SelectById(int id)
        {
            string query = "SP_GetOrder";

            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                DataTable datatable = database.DoQuery(command: command);
                OrderMaster orders = getOrder(datatable, id);
                return orders;
            }
        }

        public DBResult Update(OrderMaster entity)
        {
            string query = "OrderMasterInsertUpdateDelete";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "UPDATE");
                command.Parameters.AddWithValue("@OrderDate", entity.OrderDate);
                command.Parameters.AddWithValue("@OrderId", entity.OrderId);
                command.Parameters.AddWithValue("@CustomerId", entity.CustomerId);
                command.Parameters.AddWithValue("@PaymentGatewayId", entity.PaymentGatewayId);
                command.Parameters.AddWithValue("@ShippingId", entity.ShippingId);
                command.Parameters.AddWithValue("@TrackNumber", entity.TrackNumber);
                command.Parameters.AddWithValue("@DeliveryDate", entity.DeliveryDate);
                command.Parameters.AddWithValue("@DeliveryComment", entity.DeliveryComment);
                command.Parameters.AddWithValue("@DeliveryStatus", entity.DeliveryStatus);
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

        public OrderMaster getOrder(DataTable table, int id = 0)
        {

            if (table.Rows.Count != 0)
            {
                var groupedData = LinqHelper.GroupDataTableByField<int>(queryResult: table, fieldName:"OrderId");
                var group = groupedData.FirstOrDefault(x => x.Key == id);
                if (group != null)
                {
                    return GetOrderMaster(group);

                }



            }
            return null;
        }

        private static OrderMaster GetOrderMaster(IGrouping<int, DataRow> group)
        {
            OrderMaster ordermaster = new OrderMaster();
            int orderId = group.Key;
            DataRow dr = group.FirstOrDefault();
            ordermaster.OrderId = (int)dr["OrderId"];
            ordermaster.OrderDate = dr["OrderDate"] as string;
            ordermaster.TrackNumber = dr["TrackNumber"] as string;
            ordermaster.DeliveryDate = dr["DeliveryDate"] as string;
            ordermaster.DeliveryComment = dr["DeliveryComment"] as string;
            ordermaster.DeliveryStatus = dr["DeliveryStatus"] as string;
            ordermaster.CustomerId = (int)dr["CustomerId"];
            Customer customer = new Customer();
            customer.Firstname = dr["CustomerFirstname"] as string;
            customer.Lastname = dr["CustomerLastname"] as string;
            customer.Email = dr["CustomerEmail"] as string;
            customer.Address1 = dr["CustomerAddress1"] as string;
            customer.Address2 = dr["CustomerAddress2"] as string;
            customer.TaxAddress = dr["CustomerTaxAddress"] as string;
            customer.Status = (bool)dr["CustomerStatus"];
            ordermaster.CustomerBm = customer;
            Payment payment = new Payment();
            payment.PaymentId = (int)dr["PaymentId"];
            payment.PaymentDescription = dr["PaymentDescription"] as string;
            payment.PaymentName = dr["PaymentName"] as string;
            ordermaster.PaymentBm = payment;
            Shipping shipping = new Shipping();
            shipping.ShippingId = (int)dr["ShippingId"];
            shipping.ShippingName = dr["ShippingName"] as string;
            shipping.ShippingDescription = dr["ShippingDescription"] as string;
            ordermaster.ShippingBm = shipping;
            List<OrderDetail> orderDetails = new List<OrderDetail>();

            foreach (DataRow row in group)
            {
                if (row["OrderDetailId"] != DBNull.Value)
                {
                    int orderDetailID = (int)row["OrderDetailId"];
                    if (orderDetails.FirstOrDefault(x => x.OrderDetailId == orderDetailID) == null)
                    {
                        OrderDetail orderdetail = new OrderDetail();
                        orderdetail.OrderId = (int)row["OrderId"];
                        orderdetail.ProductId = (int)row["ProductId"];
                        orderdetail.Quantity = (int)row["ProductQuantity"];
                        orderdetail.OrderDetailId = (int)row["OrderDetailId"];
                        orderdetail.ProductPrice = Convert.ToDouble(row["ProductPrice"]);
                        orderdetail.isDiscountedPrice = (bool)row["IsDiscountedPrice"];
                        Product product = new Product();
                        product.ProductName = row["ProductName"] as string;
                        product.ProductId = (int)row["ProductId"];
                        product.ProductCode = row["ProductCode"] as string;
                        product.Brand = (int)row["ProductBrandId"];
                        Brand brand = new Brand();
                        brand.BrandId = (int)row["ProductBrandId"];
                        brand.BrandName = row["ProductBrandName"] as string;
                        brand.BrandUrl = row["BrandUrl"] as string;
                        product.brand = brand;

                        orderdetail.ProductBm = product;

                    }
                }
            }
            return ordermaster;
        }

       
    }
}
