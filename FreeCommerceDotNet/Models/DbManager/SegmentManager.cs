using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using FreeCommerceDotNet.Models.DbModels;
using FreeCommerceDotNet.Models.Interfaces;
using FreeCommerceDotNet.Models.Util;

namespace FreeCommerceDotNet.Models.DbManager
{
    public class SegmentManager : IDBOperations<Segment>, IDisposable
    {
        public void Dispose()
        {
        }

        public int Add(Segment entry)
        {
            using (SqlCommand command = new SqlCommand("sp_segments_insert"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                return Utilities.ExecuteCommand<Segment>(sqlCommand);
            }
        }

        public bool CheckIsExist(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            string sqlQuery = "DELETE FROM Segments WHERE SegmentId=@id";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                sqlCommand.Parameters.AddWithValue("@id", id);
                return Utilities.ExecuteCommand<Segment>(sqlCommand, SqlCommandTypes.Remove);
            }
        }

        public Segment Get(int id)
        {
            string sqlQuery = "select * from Segments where SegmentId = @id";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                sqlCommand.Parameters.AddWithValue("@id", id);
                var segment = new List<Segment>();
                Utilities.ExecuteCommand<Segment>(sqlCommand, SqlCommandTypes.Select, ref segment);
                return segment.FirstOrDefault();
            }
        }

        public List<Segment> GetAll()
        { 
            string sqlQuery = "select * from Segments";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                var segments = new List<Segment>();
                Utilities.ExecuteCommand<Segment>(sqlCommand, SqlCommandTypes.Select, ref segments);
                return segments;
            }
        }

        public List<Segment> GetByIntegerKey(int id, string tbl, string key)
        {
            return Utilities.GetByIntegerKey<Segment>(id, tbl, key);
        }

        public int Update(Segment entry)
        {
            using (SqlCommand command = new SqlCommand("sp_segments_update"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                Utilities.ExecuteCommand<Segment>(sqlCommand, SqlCommandTypes.Update);
                return 0;
            }
        }
    }
}