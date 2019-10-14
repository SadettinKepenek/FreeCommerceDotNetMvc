using FreeCommerceDotNet.Common.Abstract;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using FreeCommerceDotNet.Entities.Concrete;

namespace FreeCommerceDotNet.Common.Concrete
{
    public class MsSQLDatabase : IDatabase
    {
        private readonly string _connectionString = "server=94.73.144.8;Database=u8206796_dbF1B;User Id=u8206796_userF1B;Password=SPlt16S0;MultipleActiveResultSets=true;";
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
                throw new ConnectionException("Bağlantı Başlatılamadı "+e.StackTrace);
                
            }
        }

        public bool CloseConnection()
        {
            try
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                throw new ConnectionException("Bağlantı Kapatılamadı "+e.StackTrace);
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
            SqlTransaction sqlTransaction = this.Transaction;
            sqlTransaction?.Rollback();
            this.Transaction = null;
        }

        public void CommitTranscation()
        {
            var sqlTransaction = this.Transaction;
            if (sqlTransaction != null) sqlTransaction.Commit();
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

        public DBResult ReadResultFromDataTable(DataTable table)
        {
            if (table.Rows.Count != 0)
            {
                DBResult result = new DBResult()
                {
                    Id = (int)table.Rows[0]["ReturnValue"],
                    Message =  table.Rows[0]["Message"] as string 
                };
                return result;
            }
            return null;
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
                    dataReader.Close();
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