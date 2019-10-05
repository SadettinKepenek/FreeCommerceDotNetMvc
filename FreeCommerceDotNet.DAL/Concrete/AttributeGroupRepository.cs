using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.DAL.Abstract;
using FreeCommerceDotNet.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Attribute = FreeCommerceDotNet.Entities.Concrete.Attribute;

namespace FreeCommerceDotNet.DAL.Concrete
{
    public class AttributeGroupRepository : IAttributeGroupDal
    {
        MsSQLDatabase database = new MsSQLDatabase();
        public DBResult Insert(AttributeGroup entity)
        {
            string query = "AttributeGroupInsertUpdateDelete";
            using (var conn = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "INSERT");
                command.Parameters.AddWithValue("@AttributeGroupName", entity.AttributeGroupName);
                var queryResult = database.DoQuery(command: command);
                return database.ReadResultFromDataTable(queryResult);

            }
        }

        public DBResult Update(AttributeGroup entity)
        {
            string query = "AttributeGroupInsertUpdateDelete";
            using (var conn = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "UPDATE");
                command.Parameters.AddWithValue("@AttributeGroupId", entity.AttributeGroupId);
                command.Parameters.AddWithValue("@AttributeGroupName", entity.AttributeGroupName);
                var queryResult = database.DoQuery(command: command);
                return database.ReadResultFromDataTable(queryResult);

            }
        }

        public DBResult Delete(int id)
        {
            string query = "AttributeGroupInsertUpdateDelete";
            using (var conn = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "DELETE");
                command.Parameters.AddWithValue("@AttributeGroupId", id);
                var queryResult = database.DoQuery(command: command);
                return database.ReadResultFromDataTable(queryResult);

            }
        }

        public AttributeGroup SelectById(int id)
        {
            string query = "SP_GetAttributeGroup";
            using (var conn = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@AttributeGroupId", id);
                var queryResult = database.DoQuery(command: command);
                if (queryResult.Rows.Count != 0)
                {
                    AttributeGroup attributeGroup = new AttributeGroup();
                    attributeGroup.AttributeGroupId = (int)queryResult.Rows[0]["AttributeGroupId"];
                    attributeGroup.AttributeGroupName = queryResult.Rows[0]["AttributeGroupName"] as string;
                    attributeGroup.Attributes = GetAttributeGroupsAttributes(queryResult,attributeGroup.AttributeGroupId);
                    return attributeGroup;
                }
            }
            return null;
        }

        private List<Attribute> GetAttributeGroupsAttributes(DataTable queryResult, int? id = null)
        {
            if (id == null)
                return null;
                    
            
            List<Attribute> attributes = new List<Attribute>();
            var groupedData = LinqHelper.GroupDataTableByField<int>(queryResult: queryResult, fieldName: "AttributeGroupId");
            var groupOfId = groupedData.FirstOrDefault(x => x.Key == id);
            if (groupOfId != null)
            {
                int attributeGroupId = groupOfId.Key;

                foreach (DataRow row in groupOfId)
                {
                    Attribute attribute = new Attribute();
                    attribute.AttributeGroupId = attributeGroupId;
                    attribute.AttributeName = row["AttributeName"] as string;
                    attribute.AttributeId = (int)row["AttributeId"];
                    attributes.Add(attribute);
                }
            }


            return attributes;
        }

        public List<AttributeGroup> SelectByFilter(List<DBFilter> filters)
        {
            throw new NotImplementedException("Method henüz implement edilmedi");
        }

        public List<AttributeGroup> SelectAll()
        {

            string query = "SP_GetAttributeGroup";
            using (var conn = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                var queryResult = database.DoQuery(command: command);
                if (queryResult.Rows.Count != 0)
                {
                    List<AttributeGroup> attributeGroups = new List<AttributeGroup>();
                    foreach (DataRow row in queryResult.Rows)
                    {
                        AttributeGroup attributeGroup = new AttributeGroup();
                        attributeGroup.AttributeGroupId = (int)row["AttributeGroupId"];
                        attributeGroup.AttributeGroupName = row["AttributeGroupName"] as string;
                        attributeGroup.Attributes = GetAttributeGroupsAttributes(queryResult, attributeGroup.AttributeGroupId);
                        attributeGroups.Add(attributeGroup);

                    }

                    return attributeGroups;
                }

            }
            return null;
        }
    }
}