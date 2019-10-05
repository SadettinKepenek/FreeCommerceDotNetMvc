using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.DAL.Abstract;
using FreeCommerceDotNet.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

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
                    return attributeGroup;
                }
            }
            return null;
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
                    List<AttributeGroup> attributeGroups=new List<AttributeGroup>();
                    foreach (DataRow row in queryResult.Rows)
                    {
                        AttributeGroup attributeGroup = new AttributeGroup();
                        attributeGroup.AttributeGroupId = (int)row["AttributeGroupId"];
                        attributeGroup.AttributeGroupName = row["AttributeGroupName"] as string;
                        attributeGroups.Add(attributeGroup);
                    }

                    return attributeGroups;
                }

            }
            return null;
        }
    }
}