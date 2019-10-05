using FreeCommerceDotNet.Common.Concrete;
using FreeCommerceDotNet.DAL.Abstract;
using FreeCommerceDotNet.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCommerceDotNet.DAL.Concrete
{
    public class SegmentRepository : ISegmentDal
    {
        MsSQLDatabase database = new MsSQLDatabase();
        public DBResult Delete(int id)
        {
            string query = "SegmentInsertUpdateDelete";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "DELETE");
                command.Parameters.AddWithValue("@SegmentId", id);
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

        public DBResult Insert(Segment entity)
        {
            string query = "SegmentInsertUpdateDelete";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "INSERT");
                command.Parameters.AddWithValue("@Priorty", entity.Priorty);
                command.Parameters.AddWithValue("@SegmentName", entity.SegmentName);
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

        public List<Segment> SelectByFilter(List<DBFilter> filters)
        {
            throw new NotImplementedException("Method henüz implement edilmedi");
        }

        public List<Segment> SelectAll()
        {
            string query = "SP_GetSegment";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                DataTable datatable = database.DoQuery(command: command);
                if (datatable.Rows.Count != 0)
                {
                    List<Segment> list = new List<Segment>();
                    foreach (DataRow dr in datatable.Rows)
                    {
                        Segment segment = new Segment();                   
                        int segmetId = (int)dr["SegmentId"];
                        string segmentName = dr["SegmentName"].ToString();
                        string segmentPrioty = dr["Priorty"].ToString();
                        segment.SegmentId = segmetId;
                        segment.Priorty = segmentPrioty;
                        segment.SegmentName = segmentName;
                        list.Add(segment);
                    }
                    return list;
                }

            }
            return null;
        }

        public Segment SelectById(int id)
        {
            string query = "SP_GetSegment";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SegmentId", id);
                DataTable datatable = database.DoQuery(command: command);
                if (datatable.Rows.Count != 0)
                {
                    Segment segment = new Segment();
                    segment.SegmentId = id;
                    segment.SegmentName = datatable.Rows[0]["SegmentName"].ToString();
                    segment.Priorty = datatable.Rows[0]["Priorty"].ToString();
                    return segment;
                }
            }
            return null;
        }

        public DBResult Update(Segment entity)
        {
            string query = "SegmentInsertUpdateDelete";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "UPDATE");
                command.Parameters.AddWithValue("@Priorty", entity.Priorty);
                command.Parameters.AddWithValue("@SegmentId", entity.SegmentId);
                command.Parameters.AddWithValue("@SegmentName", entity.SegmentName);
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
    }
}
