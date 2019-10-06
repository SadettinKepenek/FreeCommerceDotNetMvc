using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.DAL.Abstract;
using FreeCommerceDotNet.Entities.Concrete;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FreeCommerceDotNet.DAL.Concrete
{
    public class OrderReturnRepository : IOrderReturnDal
    {
        MsSQLDatabase db = new MsSQLDatabase();
        private string crudquery = "OrderReturnInsertUpdateDelete";
        private string query = "SP_GetOrderReturn";
        public DBResult Insert(OrderReturn entity)
        {
            using (var conn = db.CreateConnection())
            {
                SqlCommand command = new SqlCommand(crudquery, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "INSERT");
                command.Parameters.AddWithValue("@OrderId", entity.OrderId);
                command.Parameters.AddWithValue("@ProductId", entity.ProductId);
                command.Parameters.AddWithValue("@BoxOpened", entity.BoxOpened);
                command.Parameters.AddWithValue("@ReturnStatus", entity.ReturnStatus);
                command.Parameters.AddWithValue("@ReturnResponse", entity.ReturnResponse);
                command.Parameters.AddWithValue("@ReturnReason", entity.ReturnReason);
                command.Parameters.AddWithValue("@Comment", entity.Comment);
                return db.ReadResultFromDataTable(db.DoQuery(command: command));

            }
        }

        public DBResult Update(OrderReturn entity)
        {
            using (var conn = db.CreateConnection())
            {
                SqlCommand command = new SqlCommand(crudquery, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "UPDATE");
                command.Parameters.AddWithValue("@ReturnId", entity.ReturnId);
                command.Parameters.AddWithValue("@OrderId", entity.OrderId);
                command.Parameters.AddWithValue("@ProductId", entity.ProductId);
                command.Parameters.AddWithValue("@BoxOpened", entity.BoxOpened);
                command.Parameters.AddWithValue("@ReturnStatus", entity.ReturnStatus);
                command.Parameters.AddWithValue("@ReturnResponse", entity.ReturnResponse);
                command.Parameters.AddWithValue("@ReturnReason", entity.ReturnReason);
                command.Parameters.AddWithValue("@Comment", entity.Comment);
                return db.ReadResultFromDataTable(db.DoQuery(command: command));

            }
        }

        public DBResult Delete(int id)
        {
            using (var conn = db.CreateConnection())
            {
                SqlCommand command = new SqlCommand(crudquery, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "DELETE");
                command.Parameters.AddWithValue("@ReturnId", id);
                return db.ReadResultFromDataTable(db.DoQuery(command: command));
            }
        }

        public OrderReturn SelectById(int id)
        {
            using (var conn = db.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ReturnId", id);
                var queryResult = db.DoQuery(command: command);
                if (queryResult.Rows.Count != 0)
                {
                    var dr = queryResult.Rows[0];
                    // dr ise Data Row
                    return ParseOrderReturn(dr);
                }
            }

            return null;
        }

        private static OrderReturn ParseOrderReturn(DataRow dr)
        {
            var customer = new Customer()
            {
                CustomerId = (int) dr["CustomerId"],
                Firstname = dr["CustomerFirstname"] as string,
                Lastname = dr["CustomerLastname"] as string,
                Email = dr["CustomerEmail"] as string,
                Telephone = dr["CustomerPhone"] as string,
                Address1 = dr["CustomerAddress1"] as string,
                Address2 = dr["CustomerAddress2"] as string,
                TaxAddress = dr["CustomerTaxAddress"] as string,
                UserId = (int) dr["CustomerUserId"]
            };
            var paymentBm = new Payment()
            {
                PaymentId = (int) dr["PaymentId"],
                PaymentName = dr["PaymentName"] as string
            };
            var shippingBm = new Shipping()
            {
                ShippingId = (int) dr["ShippingId"],
                ShippingName = dr["ShippingName"] as string
            };
            var brand = new Brand()
            {
                BrandId = (int) dr["BrandId"],
                BrandName = dr["BrandName"] as string
            };
            var productBm = new Product()
            {
                ProductId = (int) dr["ProductId"],
                ProductName = dr["ProductName"] as string,
                ProductCode = dr["ProductCode"] as string,
                Brand = (int) dr["BrandId"],
                brand = brand
            };
            var orderMaster = new OrderMaster()
            {
                OrderDate = dr["OrderDate"] as string,
                DeliveryDate = dr["DeliveryDate"] as string,
                DeliveryComment = dr["DeliveryComment"] as string,
                DeliveryStatus = dr["DeliveryStatus"] as string,
                TrackNumber = dr["TrackNumber"] as string,
                PaymentGatewayId = (int) dr["PaymentId"],
                ShippingId = (int) dr["ShippingId"],
                PaymentBm = paymentBm,
                ShippingBm = shippingBm,
                CustomerBm = customer,
                CustomerId = customer.CustomerId,
                OrderId = (int) (dr["OrderId"]),
            };

            OrderReturn orderReturn = new OrderReturn()
            {
                ProductId = (int) dr["ProductId"],
                OrderId = (int) dr["OrderId"],
                BoxOpened = (bool) dr["BoxOpened"],
                Comment = dr["Comment"] as string,
                ReturnId = (int) dr["ReturnId"],
                ReturnReason = dr["ReturnReason"] as string,
                ReturnResponse = dr["ReturnResponse"] as string,
                ReturnStatus = (bool) dr["ReturnStatus"],
                ProductBm = productBm,
                CustomerBm = customer,
                OrderBM = orderMaster
            };
            return orderReturn;
        }

        public List<OrderReturn> SelectByFilter(List<DBFilter> filters)
        {
            List<OrderReturn> orderReturns = new List<OrderReturn>();
            using (var conn = db.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                foreach (DBFilter dbFilter in filters)
                {
                    command.Parameters.AddWithValue(dbFilter.ParamName, dbFilter.ParamValue);
                }
                var queryResult = db.DoQuery(command: command);
                if (queryResult.Rows.Count != 0)
                {
                    foreach (DataRow row in queryResult.Rows)
                    {
                        orderReturns.Add(ParseOrderReturn(row));
                    }

                    return orderReturns;
                }
            }
            return null;
        }

        public List<OrderReturn> SelectAll()
        {

            List<OrderReturn> orderReturns=new List<OrderReturn>();
            using (var conn = db.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                var queryResult = db.DoQuery(command: command);
                if (queryResult.Rows.Count != 0)
                {
                    foreach (DataRow row in queryResult.Rows)
                    {
                        orderReturns.Add(ParseOrderReturn(row));
                    }

                    return orderReturns;
                }
            }
            return null;
        }
    }
}