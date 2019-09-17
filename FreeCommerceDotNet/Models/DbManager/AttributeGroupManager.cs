using FreeCommerceDotNet.Models.Interfaces;
using FreeCommerceDotNet.Models.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using FreeCommerceDotNet.Models.DbModels;

namespace FreeCommerceDotNet.Models.DbManager
{
    public class AttributeGroupManager : IOperations<AttributeGroup> , IDisposable
    {
        public void Dispose()
        {

        }

        public List<AttributeGroup> GetAll()
        {
            string query = "select * from AttributeGroups";
            using (SqlCommand command = new SqlCommand(query))
            {
                var sqlCommand = command;
                var attributegroups = new List<AttributeGroup>();
                Utilities.ExecuteCommand<AttributeGroup>(sqlCommand,SqlCommandTypes.Select,ref attributegroups);
                return attributegroups;
            }
        }

        public AttributeGroup Get(int id)
        {
            string query = "select * from AttributeGroups where AttributeGroupId = @id";
            using (SqlCommand command = new SqlCommand(query))
            {
                var sqlCommand = command;
                var attributegroups = new List<AttributeGroup>();
                Utilities.ExecuteCommand<AttributeGroup>(sqlCommand, SqlCommandTypes.Select, ref attributegroups);
                return attributegroups.First();
            }
        }

        public bool Add(AttributeGroup entry)
        {
            using (SqlCommand command = new SqlCommand("sp_attributegroup_insert"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                Utilities.CreateUpdateSqlParameters(sqlCommand,entry,entry.GetType().GetProperties());
                Utilities.ExecuteCommand<AttributeGroup>(sqlCommand, SqlCommandTypes.Insert);
                return true;
            }
        }

        public int Update(AttributeGroup entry)
        {
            using (SqlCommand command = new SqlCommand("sp_attributegroup_update"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                Utilities.ExecuteCommand<AttributeGroup>(sqlCommand, SqlCommandTypes.Update);
                return 0;
            }
        }

        public bool Delete(int id)
        {
            string sqlQuery = "DELETE FROM AttributeGroup WHERE AttributeGroupID=@id";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@Id", id);
                return Utilities.ExecuteCommand<AttributeGroup>(sqlCommand, SqlCommandTypes.Select);
            }
        }

        public bool CheckIsExist(int id)
        {
            throw new NotImplementedException();
        }
    }
}