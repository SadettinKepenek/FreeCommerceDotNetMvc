using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.DAL.Abstract;
using FreeCommerceDotNet.Entities.Concrete;

namespace FreeCommerceDotNet.DAL.Concrete
{
    public class ProductAttributeRepository:IProductAttributeDal
    {
        MsSQLDatabase db=new MsSQLDatabase();

        public DBResult Insert(ProductAttribute entity)
        {
            string query = "ProductAttributeInsertUpdateDelete";
            using (var conn=db.CreateConnection())
            {
                SqlCommand cmd=new SqlCommand(query,conn);
                cmd.Parameters.AddWithValue("@Action", "INSERT");
                cmd.Parameters.AddWithValue("@ProductId", entity.ProductId);
                cmd.Parameters.AddWithValue("@AttributeId", entity.AttributeId);
                cmd.Parameters.AddWithValue("@AttributeDescription", entity.AttributeDescription);
                var result = db.DoQuery(command: cmd);
                return db.ReadResultFromDataTable(result);
            }
            
        }

        public DBResult Update(ProductAttribute entity)
        {
            string query = "ProductAttributeInsertUpdateDelete";
            using (var conn = db.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Action", "UPDATE");
                cmd.Parameters.AddWithValue("@RelationId", entity.RelationId);
                cmd.Parameters.AddWithValue("@ProductId", entity.ProductId);
                cmd.Parameters.AddWithValue("@AttributeId", entity.AttributeId);
                cmd.Parameters.AddWithValue("@AttributeDescription", entity.AttributeDescription);
                var result = db.DoQuery(command: cmd);
                return db.ReadResultFromDataTable(result);
            }
        }

        public DBResult Delete(int id)
        {
            string query = "ProductAttributeInsertUpdateDelete";
            using (var conn = db.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.Parameters.AddWithValue("@RelationId", id);
                var result = db.DoQuery(command: cmd);
                return db.ReadResultFromDataTable(result);
            }
        }

        public ProductAttribute SelectById(int id)
        {
            string query = "SP_GetProductAttribute";
            using (var conn = db.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@RelationId", id);
                var result = db.DoQuery(command: cmd);
                if (result.Rows.Count!=0)
                {
                    var dr = result.Rows[0];
                    ProductAttribute productAttribute = new ProductAttribute();
                    productAttribute.AttributeId = (int) dr["AttributeId"];
                    productAttribute.RelationId = (int) dr["RelationId"];
                    productAttribute.ProductId = (int) dr["ProductId"];
                    productAttribute.AttributeDescription = dr["AttributeDescription"] as string;
                    productAttribute.AttributeBm=new Attribute()
                    {
                        AttributeId = (int) dr["AttributeId"],
                        AttributeGroupId = (int) dr["AttributeGroupId"],
                        AttributeName = dr["AttributeName"] as string,
                        AttributeGroup = new AttributeGroup()
                        {
                            AttributeGroupId = (int) dr["AttributeGroupId"],
                            AttributeGroupName = dr["AttributeGroupName"] as string
                        }
                    };
                    return productAttribute;
                }
                
                
                return null;
            }
        }

        public List<ProductAttribute> SelectByProductId(int id)
        {
            string query = "SP_GetProductAttribute";
            using (var conn = db.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ProductId", id);
                var result = db.DoQuery(command: cmd);
                if (result.Rows.Count != 0)
                {
                    var productAttributes = ParseProductAttributeList(result);
                    return productAttributes;
                }


                return null;
            }
        }

        public List<ProductAttribute> SelectByFilter(List<DBFilter> filters)
        {
            string query = "SP_GetProductAttribute";
            using (var conn = db.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                foreach (DBFilter filter in filters)
                {
                    cmd.Parameters.AddWithValue(filter.ParamName, filter.ParamValue);
                }
                var result = db.DoQuery(command: cmd);
                if (result.Rows.Count != 0)
                {
                    var productAttributes = ParseProductAttributeList(result);
                    return productAttributes;
                }


                return null;
            }
        }

        public List<ProductAttribute> SelectAll()
        {
            string query = "SP_GetProductAttribute";
            using (var conn = db.CreateConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                var result = db.DoQuery(command: cmd);
                if (result.Rows.Count != 0)
                {
                    var productAttributes = ParseProductAttributeList(result);

                    return productAttributes;
                }


                return null;
            }
        }

        private static List<ProductAttribute> ParseProductAttributeList(DataTable result)
        {
            List<ProductAttribute> productAttributes = new List<ProductAttribute>();
            foreach (DataRow dr in result.Rows)
            {
                ProductAttribute productAttribute = new ProductAttribute();
                productAttribute.AttributeId = (int) dr["AttributeId"];
                productAttribute.RelationId = (int) dr["RelationId"];
                productAttribute.ProductId = (int) dr["ProductId"];
                productAttribute.AttributeDescription = dr["AttributeDescription"] as string;
                productAttribute.AttributeBm = new Attribute()
                {
                    AttributeId = (int) dr["AttributeId"],
                    AttributeGroupId = (int) dr["AttributeGroupId"],
                    AttributeName = dr["AttributeName"] as string,
                    AttributeGroup = new AttributeGroup()
                    {
                        AttributeGroupId = (int) dr["AttributeGroupId"],
                        AttributeGroupName = dr["AttributeGroupName"] as string
                    }
                };
                productAttributes.Add(productAttribute);
            }

            return productAttributes;
        }
    }
}