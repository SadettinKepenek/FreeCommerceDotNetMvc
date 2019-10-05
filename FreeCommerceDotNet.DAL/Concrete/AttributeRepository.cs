using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.DAL.Abstract;
using FreeCommerceDotNet.Entities.Concrete;

namespace FreeCommerceDotNet.DAL.Concrete
{
    public class AttributeRepository:IAttributeDal
    {
        MsSQLDatabase database=new MsSQLDatabase();
        public DBResult Insert(Attribute entity)
        {
            string query = "AttributeInsertUpdateDelete";
            using (var conn = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "INSERT");
                command.Parameters.AddWithValue("@AttributeName", entity.AttributeName);
                command.Parameters.AddWithValue("@AttributeGroup", entity.AttributeGroupId);
                var queryResult = database.DoQuery(command: command);
                return database.ReadResultFromDataTable(queryResult);
            }
        }

        public DBResult Update(Attribute entity)
        {
            string query = "AttributeInsertUpdateDelete";
            using (var conn = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "UPDATE");
                command.Parameters.AddWithValue("@AttributeId", entity.AttributeId);
                command.Parameters.AddWithValue("@AttributeName", entity.AttributeName);
                command.Parameters.AddWithValue("@AttributeGroup", entity.AttributeGroupId);
                var queryResult = database.DoQuery(command: command);
                return database.ReadResultFromDataTable(queryResult);
            }
        }

        public DBResult Delete(int id)
        {
            string query = "AttributeInsertUpdateDelete";
            using (var conn = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "DELETE");
                command.Parameters.AddWithValue("@AttributeId",id);
                var queryResult = database.DoQuery(command: command);
                return database.ReadResultFromDataTable(queryResult);
            }
        }

        public Attribute SelectById(int id)
        {
            string query = "SP_GetAttribute";
            using (var conn = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);
                var queryResult = database.DoQuery(command: command);

                if (queryResult.Rows.Count!=0)
                {
                    Attribute attribute=new Attribute();
                    attribute.AttributeId = (int) queryResult.Rows[0]["AttributeId"];
                    attribute.AttributeGroupId = (int) queryResult.Rows[0]["AttributeGroup"];
                    attribute.AttributeName = queryResult.Rows[0]["AttributeName"] as string;
                    AttributeGroup attributeGroup=new AttributeGroup();
                    attributeGroup.AttributeGroupId= (int)queryResult.Rows[0]["AttributeGroup"];
                    attributeGroup.AttributeGroupName = queryResult.Rows[0]["AttributeGroupName"] as string;
                    attribute.AttributeGroup = attributeGroup;
                    return attribute;
                }
                
            }
            return null;
        }

        public List<Attribute> SelectByFilter(List<DBFilter> filters)
        {
            string query = "SP_GetAttribute";
            using (var conn = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                foreach (DBFilter dbFilter in filters)
                {
                    command.Parameters.AddWithValue(dbFilter.ParamName, dbFilter.ParamValue);
                }
                var queryResult = database.DoQuery(command: command);

                if (queryResult.Rows.Count != 0)
                {
                    List<Attribute> attributes=new List<Attribute>();
                    foreach (DataRow row in queryResult.Rows)
                    {
                        Attribute attribute = new Attribute();
                        attribute.AttributeId = (int)row["AttributeId"];
                        attribute.AttributeGroupId = (int)row["AttributeGroup"];
                        attribute.AttributeName = row["AttributeName"] as string;
                        AttributeGroup attributeGroup = new AttributeGroup();
                        attributeGroup.AttributeGroupId = (int)row["AttributeGroup"];
                        attributeGroup.AttributeGroupName = row["AttributeGroupName"] as string;
                        attribute.AttributeGroup = attributeGroup;
                        attributes.Add(attribute);
                    }

                    return attributes;
                }

            }
            return null;
        }

        public List<Attribute> SelectAll()
        {
            string query = "SP_GetAttribute";
            using (var conn = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                var queryResult = database.DoQuery(command: command);

                if (queryResult.Rows.Count != 0)
                {
                    List<Attribute> attributes = new List<Attribute>();
                    foreach (DataRow row in queryResult.Rows)
                    {
                        Attribute attribute = new Attribute();
                        attribute.AttributeId = (int)row["AttributeId"];
                        attribute.AttributeGroupId = (int)row["AttributeGroup"];
                        attribute.AttributeName = row["AttributeName"] as string;
                        AttributeGroup attributeGroup = new AttributeGroup();
                        attributeGroup.AttributeGroupId = (int)row["AttributeGroup"];
                        attributeGroup.AttributeGroupName = row["AttributeGroupName"] as string;
                        attribute.AttributeGroup = attributeGroup;
                        attributes.Add(attribute);
                    }
                    return attributes;
                }

            }
            return null;
        }
    }
}