using System;
using System.Data;
using System.Data.SqlClient;

namespace FreeCommerceDotNet.Common.Abstract
{
    public interface IDatabase:IDisposable
    {
        bool OpenConnection();
        bool CloseConnection();
        SqlConnection CreateConnection();
        void BeginTransaction();
        void RollbackTranscation();
        void CommitTranscation();
        bool IsInTranscation();
        DataTable DoQuery(string query = null, SqlCommand command = null);

    }
}