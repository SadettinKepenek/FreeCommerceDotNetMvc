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
    public class PaymentGatewayRepository : IPaymentGatewayDal
    {
        MsSQLDatabase database = new MsSQLDatabase();

        public DBResult Insert(Payment entity)
        {
            string query = "PaymentGatewayInsertUpdateDelete";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "INSERT");
                command.Parameters.AddWithValue("@PaymentName", entity.PaymentName);
                command.Parameters.AddWithValue("@PaymentDescription", entity.PaymentDescription);
                DataTable datatable = database.DoQuery(command: command);
                if (datatable.Rows.Count != 0)
                {
                    return database.ReadResultFromDataTable(datatable);

                }
            }

            return null;
        }
        public DBResult Update(Payment entity)
        {
            string query = "PaymentGatewayInsertUpdateDelete";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "UPDATE");
                command.Parameters.AddWithValue("@PaymentName", entity.PaymentName);
                command.Parameters.AddWithValue("@PaymentId", entity.PaymentId);
                command.Parameters.AddWithValue("@PaymentDescription", entity.PaymentDescription);
                DataTable datatable = database.DoQuery(command: command);
                if (datatable.Rows.Count != 0)
                {
                    return database.ReadResultFromDataTable(datatable);

                }
            }

            return null;
        }
        public DBResult Delete(int id)
        {
            string query = "PaymentGatewayInsertUpdateDelete";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "DELETE");
                command.Parameters.AddWithValue("@PaymentId", id);
                DataTable datatable = database.DoQuery(command: command);
                if (datatable.Rows.Count != 0)
                {
                    return database.ReadResultFromDataTable(datatable);

                }
            }
            return null;
        }


        public List<Payment> SelectAll()
        {
            string query = "SP_GetPayment";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                DataTable datatable = database.DoQuery(command: command);
                return GetPayments(datatable);
            }
        }
        public Payment SelectById(int id)
        {
            string query = "SP_GetPayment";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PaymentId",id);
                DataTable datatable = database.DoQuery(command: command);
                var rows = datatable.Rows;
                Payment payment = new Payment();
                payment.PaymentName = rows[0]["PaymentName"] as string;
                payment.PaymentId = (int)rows[0]["PaymentId"];
                payment.PaymentDescription = rows[0]["PaymentDescription"] as string;
                return payment;
            }
        }

        public List<Payment> SelectByFilter(List<DBFilter> filters)
        {
            string query = "SP_GetPayment";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                foreach (var item in filters)
                {
                    command.Parameters.AddWithValue(item.ParamName, item.ParamValue);
                }
                DataTable datatable = database.DoQuery(command: command);
                return GetPayments(datatable);
            }
        }

        private List<Payment> GetPayments(DataTable table)
        {
            var rows = table.Rows;
            if (rows.Count != 0)
            {
                List<Payment> payments = new List<Payment>();
                foreach (DataRow row in rows)
                {
                    Payment payment = new Payment();
                    payment.PaymentName = row["PaymentName"] as string;
                    payment.PaymentId = (int)row["PaymentId"];
                    payment.PaymentDescription = row["PaymentDescription"] as string;
                    payments.Add(payment);
                }
                return payments;
            }
            return null;
        }


    }
}
