using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.DAL.Abstract;
using FreeCommerceDotNet.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCommerceDotNet.DAL.Concrete
{
    public class OrderDetailRepository : IOrderDetailDal
    {
        MsSQLDatabase database = new MsSQLDatabase();
        public DBResult Delete(int id)
        {
            string query = "OrderDetailDiscountInsertUpdateDelete";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "DELETE");
                command.Parameters.AddWithValue("@OrderDetailId", id);
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

        public DBResult Insert(OrderDetail entity)
        {
            string query = "OrderDetailDiscountInsertUpdateDelete";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "INSERT");
                command.Parameters.AddWithValue("@OrderId", entity.OrderId);
                command.Parameters.AddWithValue("@Quantity", entity.Quantity);
                command.Parameters.AddWithValue("@ProductPrice", entity.ProductBm.ProductPrices.FirstOrDefault().Price);
                command.Parameters.AddWithValue("@ProductId", entity.ProductId);
                command.Parameters.AddWithValue("@IsDiscountedPrice", entity.isDiscountedPrice);

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

        public List<OrderDetail> SelectAll()
        {
            string query = "SP_GetOrder";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                DataTable datatable = database.DoQuery(command: command);
                if (datatable.Rows.Count != 0)
                {
                    List<OrderDetail> list = new List<OrderDetail>();
                    foreach (DataRow dr in datatable.Rows)
                    {
                        OrderDetail detail = GetOrderDetail(dr);
                        list.Add(detail);
                    }
                    return list;
                }
            }
            return null;
        }

        private static OrderDetail GetOrderDetail(DataRow dr)
        {
            OrderDetail detail = new OrderDetail();
            int detailQuantity = (int)dr["ProductQuantity"];
            int detailOrderId = (int)dr["OrderId"];
            double detailProductPrice = Convert.ToDouble(dr["ProductPrice"]);
            bool detailIsDiscountedPrice = (bool)dr["IsDiscountedPrice"];

            detail.ProductBm = new Product()
            {
                ProductId = (int)dr["ProductId"],
                ProductName = dr["ProductName"] as string,
                ProductCode = dr["ProductCode"] as string,
                Brand = (int)dr["ProductBrandId"],
                brand = new Brand()
                {
                    BrandName = dr["ProductBrandName"] as string,
                    BrandId = (int)dr["ProductBrandId"],
                    BrandUrl = dr["BrandUrl"] as string

                },

            };
            detail.ProductPrice = detailProductPrice;
            detail.isDiscountedPrice = detailIsDiscountedPrice;
            detail.Quantity = detailQuantity;
            detail.OrderId = detailOrderId;
            return detail;
        }

        public OrderDetail SelectById(int id)
        {
            string query = "SP_GetOrder";
            using (var conn = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@OrderDetailId", id);
                DataTable datatable = database.DoQuery(command: command);
                if (datatable.Rows.Count != 0)
                {
                    return GetOrderDetail(datatable.Rows[0]);
                }


            }
            return null;
        }



        public DBResult Update(OrderDetail entity)
        {
            string query = "OrderDetailDiscountInsertUpdateDelete";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "UPDATE");
                command.Parameters.AddWithValue("@OrderId", entity.OrderId);
                command.Parameters.AddWithValue("@OrderDetailId", entity.OrderDetailId);
                command.Parameters.AddWithValue("@Quantity", entity.Quantity);
                command.Parameters.AddWithValue("@ProductPrice", entity.ProductBm.ProductPrices.FirstOrDefault().Price);
                command.Parameters.AddWithValue("@ProductId", entity.ProductId);
                command.Parameters.AddWithValue("@IsDiscountedPrice", entity.isDiscountedPrice);

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
        public List<OrderDetail> SelectByFilter(List<DBFilter> filters)
        {
           
            string query = "SP_GetOrder";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                foreach (var item in filters)
                {
                    command.Parameters.AddWithValue(item.ParamName,item.ParamValue);

                }
                command.CommandType = CommandType.StoredProcedure;
                DataTable datatable = database.DoQuery(command: command);
                if (datatable.Rows.Count != 0)
                {
                    List<OrderDetail> list = new List<OrderDetail>();
                    foreach (DataRow dr in datatable.Rows)
                    {
                        OrderDetail detail = GetOrderDetail(dr);
                        list.Add(detail);
                    }
                    return list;
                }
            }
            
            return null;
        }
    }
}
