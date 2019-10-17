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
    public class InvoiceRepository : IInvoiceDal
    {
        MsSQLDatabase database = new MsSQLDatabase();

        public DBResult Insert(Invoice entity)
        {
            string query = "InvoiceInsertUpdateDelete";
            using (var conn = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "INSERT");
                command.Parameters.AddWithValue("@InvoiceTotalPrice", entity.InvoiceTotalPrice);
                command.Parameters.AddWithValue("@OrderId", entity.OrderId);
                command.Parameters.AddWithValue("@TranscationNo", entity.TranscationNo);
                command.Parameters.AddWithValue("@InvoiceStatus", entity.InvoiceStatus);
                var queryResult = database.DoQuery(command: command);
                return database.ReadResultFromDataTable(queryResult);
            }
        }
        public DBResult Update(Invoice entity)
        {
            string query = "InvoiceInsertUpdateDelete";
            using (var conn = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "UPDATE");
                command.Parameters.AddWithValue("@InvoiceTotalPrice", entity.InvoiceTotalPrice);
                command.Parameters.AddWithValue("@OrderId", entity.OrderId);
                command.Parameters.AddWithValue("@TranscationNo", entity.TranscationNo);
                command.Parameters.AddWithValue("@InvoiceStatus", entity.InvoiceStatus);
                command.Parameters.AddWithValue("@InvoiceId", entity.InvoiceId);
                var queryResult = database.DoQuery(command: command);
                return database.ReadResultFromDataTable(queryResult);
            }
        }
        public DBResult Delete(int id)
        {
            string query = "InvoiceInsertUpdateDelete";
            using (var conn = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "DELETE");
                command.Parameters.AddWithValue("@InvoiceId", id);
                var queryResult = database.DoQuery(command: command);
                return database.ReadResultFromDataTable(queryResult);
            }
        }


        public List<Invoice> SelectAll()
        {
            string query = "SP_GetInvoices";
            using (var conn = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                var queryResult = database.DoQuery(command: command);
                return GetInvoices(queryResult);
            }
        }

        public int GetInvoiceCount()
        {
            string query = "SELECT COUNT(1) AS InvoiceCount FROM Invoices";
            using (var conn = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, conn);
                var queryResult = database.DoQuery(command: command);
                if (queryResult.Rows.Count!=0)
                {
                    var dr = queryResult.Rows[0];
                    int invoiceCount = (int) dr["InvoiceCount"];
                    return invoiceCount;
                }
            }
            return 0;
        }

        public List<Invoice> SelectByFilter(List<DBFilter> filters)
        {
            string query = "SP_GetInvoices";

            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                foreach (var item in filters)
                {
                    command.Parameters.AddWithValue(item.ParamName, item.ParamValue);
                }
                DataTable datatable = database.DoQuery(command: command);
                return GetInvoices(datatable);
            }
        }
        public Invoice SelectById(int id)
        {
            string query = "SP_GetInvoices";
            using (var conn = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                var queryResult = database.DoQuery(command: command);
                var rows = queryResult.Rows;
                Invoice invoice = new Invoice();
                invoice.InvoiceId = (int)rows[0]["InvoiceId"];
                invoice.InvoiceStatus = (bool)rows[0]["InvoiceStatus"];
                invoice.InvoiceTotalDiscount = Convert.ToDouble(rows[0]["InvoiceTotalDiscount"]);
                invoice.InvoiceTotalPrice = Convert.ToDouble(rows[0]["InvoiceTotalPrice"]);
                invoice.TranscationNo = rows[0]["TranscationNo"] as string;
                OrderMaster orderMaster = new OrderMaster();
                orderMaster.OrderId = (int)rows[0]["OrderId"];
                return invoice;
            }
           
        }

        private List<Invoice> GetInvoices(DataTable table)
        {
            var rows = table.Rows;
            if (rows.Count != 0)
            {
                List<Invoice> invoices = new List<Invoice>();
                foreach (DataRow row in rows)
                {
                    Invoice invoice = new Invoice();
                    invoice.InvoiceId = (int)row["InvoiceId"];
                    invoice.InvoiceStatus = (bool)row["InvoiceStatus"];
                    invoice.InvoiceTotalDiscount = Convert.ToDouble(row["InvoiceTotalDiscount"]);
                    invoice.InvoiceTotalPrice = Convert.ToDouble(row["InvoiceTotalPrice"]);
                    invoice.TranscationNo = row["TranscationNo"] as string;
                    OrderMaster orderMaster = new OrderMaster();
                    orderMaster.OrderId = (int)row["OrderId"];
                    invoice.OrderMaster = orderMaster;
                    invoices.Add(invoice);
                }
                return invoices;
            }
            return null;
        }

    }
}
