using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.DAL.Abstract;
using FreeCommerceDotNet.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FreeCommerceDotNet.DAL.Concrete
{
    public class ProductPriceRepository : IProductPriceDal
    {
        MsSQLDatabase db = new MsSQLDatabase();
        public DBResult Insert(ProductPrice entity)
        {
            string query = "ProductPriceInsertUpdateDelete";
            using (var conn = db.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "INSERT");
                command.Parameters.AddWithValue("@ProductId", entity.ProductId);
                command.Parameters.AddWithValue("@Price", entity.Price);
                command.Parameters.AddWithValue("@Segment", entity.SegmentId);
                return db.ReadResultFromDataTable(db.DoQuery(command: command));
            }
        }

        public DBResult Update(ProductPrice entity)
        {
            string query = "ProductPriceInsertUpdateDelete";
            using (var conn = db.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "UPDATE");
                command.Parameters.AddWithValue("@PriceId", entity.PriceId);
                command.Parameters.AddWithValue("@ProductId", entity.ProductId);
                command.Parameters.AddWithValue("@Price", entity.Price);
                command.Parameters.AddWithValue("@Segment", entity.SegmentId);
                return db.ReadResultFromDataTable(db.DoQuery(command: command));
            }
        }

        public DBResult Delete(int id)
        {
            string query = "ProductPriceInsertUpdateDelete";
            using (var conn = db.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "DELETE");
                command.Parameters.AddWithValue("@PriceId", id);
                return db.ReadResultFromDataTable(db.DoQuery(command: command));
            }
        }

        public ProductPrice SelectById(int id)
        {
            string query = "ProductPriceInsertUpdateDelete";
            using (var conn = db.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);
                var result = db.DoQuery(command: command);
                if (result.Rows.Count != 0)
                {
                    var dr = result.Rows[0];
                    ProductPrice price = new ProductPrice();
                    price.PriceId = (int)dr["PriceId"];
                    price.Price = Convert.ToDouble(dr["Price"]);
                    price.ProductId = (int)dr["ProductId"];
                    price.SegmentId = (int)dr["SegmentId"];
                    price.Segment = new Segment()
                    {
                        SegmentId = (int)dr["SegmentId"],
                        SegmentName = dr["SegmentName"] as string,
                        Priorty = dr["Priorty"] as string
                    };
                    return price;
                }
            }
            return null;
        }

        public List<ProductPrice> SelectByFilter(List<DBFilter> filters)
        {
            string query = "ProductPriceInsertUpdateDelete";
            using (var conn = db.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, conn);
                foreach (DBFilter filter in filters)
                {
                    command.Parameters.AddWithValue(filter.ParamName, filter.ParamValue);
                }
                command.CommandType = CommandType.StoredProcedure;
                var result = db.DoQuery(command: command);
                if (result.Rows.Count != 0)
                {
                    var productPrices = ParseProductPrices(result);
                    return productPrices;
                }
            }
            return null;
        }
        public List<ProductPrice> SelectAll()
        {
            string query = "ProductPriceInsertUpdateDelete";
            using (var conn = db.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                var result = db.DoQuery(command: command);
                if (result.Rows.Count != 0)
                {
                    var productPrices = ParseProductPrices(result);
                    return productPrices;
                }
            }
            return null;
        }
        private static List<ProductPrice> ParseProductPrices(DataTable result)
        {
            List<ProductPrice> productPrices = new List<ProductPrice>();
            foreach (DataRow dr in result.Rows)
            {
                ProductPrice price = new ProductPrice();
                price.PriceId = (int)dr["PriceId"];
                price.Price = Convert.ToDouble(dr["Price"]);
                price.ProductId = (int)dr["ProductId"];
                price.SegmentId = (int)dr["SegmentId"];
                price.Segment = new Segment()
                {
                    SegmentId = (int)dr["SegmentId"],
                    SegmentName = dr["SegmentName"] as string,
                    Priorty = dr["Priorty"] as string
                };
                productPrices.Add(price);
            }

            return productPrices;
        }


    }
}