using FreeCommerceDotNet.Common.Abstract;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace FreeCommerceDotNet.Common.Concrete
{
    public class MsSQLDatabase : IDatabase
    {
        private readonly string _connectionString = "server=94.73.144.8;Database=u8206796_dbF1B;User Id=u8206796_userF1B;Password=SPlt16S0;";
        private SqlConnection connection;
        private SqlTransaction Transaction;
        public MsSQLDatabase()
        {
            this.connection = new SqlConnection(_connectionString);
        }
        public bool OpenConnection()
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool CloseConnection()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return false;
        }

        public SqlConnection CreateConnection()
        {
            SqlConnection connection = new SqlConnection(this._connectionString);
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            this.connection = connection;

            return connection;
        }

        public void BeginTransaction()
        {
            SqlTransaction transaction;
            if (this.connection!=null)
            {
                OpenConnection();
                var sqlConnection = this.connection;

                transaction = sqlConnection.BeginTransaction();
                this.Transaction = transaction;
            }
            else
            {
                CreateConnection();
                OpenConnection();
                var sqlConnection = this.connection;

                transaction = sqlConnection.BeginTransaction();
                this.Transaction = transaction;
            }
           
        }

        public void RollbackTranscation()
        {
            this.Transaction.Rollback();
            this.Transaction = null;
        }

        public void CommitTranscation()
        {
            this.Transaction.Commit();
            this.Transaction = null;
        }

        public bool IsInTranscation()
        {
            return Transaction!=null;
        }



        public DataTable DoQuery(string query = null, SqlCommand command = null)
        {
            if (command != null && String.IsNullOrEmpty(query))
            {
                return ExecuteSqlCommand(command);
            }
            else if (!String.IsNullOrEmpty(query) && command==null)
            {
                SqlCommand sqlCommand = new SqlCommand(query);
                return ExecuteSqlCommand(sqlCommand);

            }
            else if (command!=null && !String.IsNullOrEmpty(query))
            {
                throw new AmbiguousMatchException("Query ve Command aynı anda gönderilemez.");
            }
            else
            {
                throw new ArgumentNullException("query ve command parameterlerinin her ikisi de null");
            }
        }

        private DataTable ExecuteSqlCommand(SqlCommand command)
        {
            using (var conn=this.connection)
            {
                BeginTransaction();
                try
                {
                    command.Connection = conn;
                    command.Transaction=this.Transaction;
                    var dataReader = command.ExecuteReader();
                    var dataTable = new DataTable();
                    dataTable.Load(dataReader);
                    CommitTranscation();
                    return dataTable;
                }
                catch (Exception e)
                {
                    RollbackTranscation();
                    throw new Exception("Do Query Has Been Failed. Error Info -->\n" + e.StackTrace);
                }
            }
        }

        public void Dispose()
        {

        }
    }
}