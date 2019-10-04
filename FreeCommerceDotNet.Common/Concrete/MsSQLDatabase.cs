using FreeCommerceDotNet.Common.Abstract;
using System;
using System.Data;
using System.Data.SqlClient;

namespace FreeCommerceDotNet.Common.Concrete
{
    public class MsSQLDatabase : IDatabase
    {
        private readonly string _connectionString = "server=94.73.144.8;Database=u8206796_dbF1B;User Id=u8206796_userF1B;Password=SPlt16S0;";
        public SqlConnection connection;
        public SqlTransaction Transaction;
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
            if (OpenConnection())
            {
                var sqlConnection = this.connection;
                if (sqlConnection != null)
                {
                    transaction = sqlConnection.BeginTransaction();
                    this.Transaction = transaction;
                }
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
            if (command != null)
            {
                return ExecuteSqlCommand(command);
            }
            else if (!String.IsNullOrEmpty(query))
            {
                SqlCommand sqlCommand = new SqlCommand(query);
                return ExecuteSqlCommand(sqlCommand);

            }
            else
            {
                throw new ArgumentNullException("query ve command parameterlerinin her ikisi de null");
            }
        }

        private DataTable ExecuteSqlCommand(SqlCommand command)
        {
            using (this.connection)
            {
                try
                {
                    var dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    var dataTable = new DataTable();
                    dataTable.Load(dataReader);
                    return dataTable;
                }
                catch (Exception e)
                {
                    throw new Exception("Do Query Has Been Failed. Error Info -->\n" + e.StackTrace);
                }
            }
        }

        public void Dispose()
        {

        }
    }
}