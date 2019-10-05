using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.DAL.Abstract;
using FreeCommerceDotNet.Entities.Concrete;

namespace FreeCommerceDotNet.DAL.Concrete
{
    public class ProductDiscountRepository:IProductDiscountDal
    {
        MsSQLDatabase db=new MsSQLDatabase();
        public DBResult Insert(ProductDiscount entity)
        {
            string query = "ProductDiscountInsertUpdateDelete";
            using (var conn=db.CreateConnection())
            {
                SqlCommand cmd=new SqlCommand(query,conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "INSERT");
                cmd.Parameters.AddWithValue("@ProductId", entity.ProductId);
                cmd.Parameters.AddWithValue("@StartDate", entity.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", entity.EndDate);
                cmd.Parameters.AddWithValue("@NewPrice", entity.NewPrice);
                cmd.Parameters.AddWithValue("@Quantity", entity.Quantity);
                cmd.Parameters.AddWithValue("@Segment", entity.SegmentId);
                var result = db.DoQuery(command: cmd);
                return db.ReadResultFromDataTable(result);
            }
        }

        public DBResult Update(ProductDiscount entity)
        {
            string query = "ProductDiscountInsertUpdateDelete";
            using (var conn = db.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "UPDATE");
                cmd.Parameters.AddWithValue("@DiscountId", entity.DiscountId);
                cmd.Parameters.AddWithValue("@ProductId", entity.ProductId);
                cmd.Parameters.AddWithValue("@StartDate", entity.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", entity.EndDate);
                cmd.Parameters.AddWithValue("@NewPrice", entity.NewPrice);
                cmd.Parameters.AddWithValue("@Quantity", entity.Quantity);
                cmd.Parameters.AddWithValue("@Segment", entity.SegmentId);
                var result = db.DoQuery(command: cmd);
                return db.ReadResultFromDataTable(result);
            }
        }

        public DBResult Delete(int id)
        {
            string query = "ProductDiscountInsertUpdateDelete";
            using (var conn = db.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@DiscountId", id);
                var result = db.DoQuery(command: cmd);
                return db.ReadResultFromDataTable(result);
            }
        }

        public ProductDiscount SelectById(int id)
        {
            string query = "SP_GetProductDiscount";
            using (var conn = db.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                var result = db.DoQuery(command: cmd);
                if (result.Rows.Count!=0)
                {
                    DataRow dr = result.Rows[0];
                    ProductDiscount discount=new ProductDiscount();
                    discount.DiscountId = (int) dr["DiscountId"];
                    discount.EndDate = dr["EndDate"] as string;
                    discount.StartDate= dr["StartDate"] as string;
                    discount.Quantity = (int) dr["Quantity"];
                    discount.NewPrice = Convert.ToDouble(dr["NewPrice"]);
                    discount.SegmentId = (int) dr["SegmentId"];
                    discount.SegmentEntity=new Segment()
                    {
                        SegmentId = (int) dr["SegmentId"],
                        SegmentName = dr["SegmentName"] as string,
                        Priorty = dr["Priorty"] as string
                    };
                    return discount;

                }
            }
            return null;
        }

        public List<ProductDiscount> SelectByFilter(List<DBFilter> filters)
        {
            string query = "SP_GetProductDiscount";
            using (var conn = db.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (DBFilter dbFilter in filters)
                {
                    cmd.Parameters.AddWithValue(dbFilter.ParamName, dbFilter.ParamValue);
                }
                var result = db.DoQuery(command: cmd);
                if (result.Rows.Count != 0)
                {
                    var discounts = ParseProductDiscounts(result);
                    return discounts;
                }
            }
            return null;
        }

        public List<ProductDiscount> SelectAll()
        {
            string query = "SP_GetProductDiscount";
            using (var conn = db.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                var result = db.DoQuery(command: cmd);
                if (result.Rows.Count != 0)
                {
                    var discounts = ParseProductDiscounts(result);
                    return discounts;
                }
            }
            return null;
        }

        private static List<ProductDiscount> ParseProductDiscounts(DataTable result)
        {
            List<ProductDiscount> discounts = new List<ProductDiscount>();
            foreach (DataRow dr in result.Rows)
            {
                ProductDiscount discount = new ProductDiscount();
                discount.DiscountId = (int) dr["DiscountId"];
                discount.EndDate = dr["EndDate"] as string;
                discount.StartDate = dr["StartDate"] as string;
                discount.Quantity = (int) dr["Quantity"];
                discount.NewPrice = Convert.ToDouble(dr["NewPrice"]);
                discount.SegmentId = (int) dr["SegmentId"];
                discount.SegmentEntity = new Segment()
                {
                    SegmentId = (int) dr["SegmentId"],
                    SegmentName = dr["SegmentName"] as string,
                    Priorty = dr["Priorty"] as string
                };
                discounts.Add(discount);
            }

            return discounts;
        }
    }
}