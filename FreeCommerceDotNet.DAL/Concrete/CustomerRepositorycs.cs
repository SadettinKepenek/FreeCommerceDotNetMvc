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
    public class CustomerRepositorycs : ICustomerDal
    {
        MsSQLDatabase database = new MsSQLDatabase();
        public DBResult Insert(Customer entity)
        {
            string query = "CustomerInsertUpdateDelete";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "INSERT");
                command.Parameters.AddWithValue("@Address1", entity.Address1);
                command.Parameters.AddWithValue("@Address2", entity.Address2);
                command.Parameters.AddWithValue("@Email", entity.Email);
                command.Parameters.AddWithValue("@Firstname", entity.Firstname);
                command.Parameters.AddWithValue("@Lastname", entity.Lastname);
                command.Parameters.AddWithValue("@Password", entity.Password);
                command.Parameters.AddWithValue("@Status", entity.Status);
                command.Parameters.AddWithValue("@TaxAddress", entity.TaxAddress);
                command.Parameters.AddWithValue("@SegmentId", entity.SegmentId);
                command.Parameters.AddWithValue("@UserId", entity.UserId);
                command.Parameters.AddWithValue("@Telephone", entity.Telephone);
                DataTable datatable = database.DoQuery(command: command);
                if (datatable.Rows.Count != 0)
                {
                    return database.ReadResultFromDataTable(datatable);

                }
            }

            return null;
        }
        public DBResult Update(Customer entity)
        {
            string query = "CustomerInsertUpdateDelete";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "UPDATE");
                command.Parameters.AddWithValue("@Address1", entity.Address1);
                command.Parameters.AddWithValue("@CustomerId", entity.CustomerId);
                command.Parameters.AddWithValue("@Address2", entity.Address2);
                command.Parameters.AddWithValue("@Email", entity.Email);
                command.Parameters.AddWithValue("@Firstname", entity.Firstname);
                command.Parameters.AddWithValue("@Lastname", entity.Lastname);
                command.Parameters.AddWithValue("@Password", entity.Password);
                command.Parameters.AddWithValue("@Status", entity.Status);
                command.Parameters.AddWithValue("@TaxAddress", entity.TaxAddress);
                command.Parameters.AddWithValue("@SegmentId", entity.SegmentId);
                command.Parameters.AddWithValue("@UserId", entity.UserId);
                command.Parameters.AddWithValue("@Telephone", entity.Telephone);
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
            string query = "CustomerInsertUpdateDelete";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "DELETE");
                command.Parameters.AddWithValue("@CustomerId", id);
                DataTable datatable = database.DoQuery(command: command);
                if (datatable.Rows.Count != 0)
                {
                    return database.ReadResultFromDataTable(datatable);

                }
            }
            return null;
        }
        public Customer SelectById(int id)
        {
            string query = "SP_GetCustomer";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);
                DataTable datatable = database.DoQuery(command: command);
                var rows = datatable.Rows;
                if (rows.Count != 0)
                {
                    Customer customer = new Customer();
                    customer.CustomerId = id;
                    customer.Firstname = rows[0]["CustomerFirstName"] as string;
                    customer.Address1 = rows[0]["CustomerAddress1"] as string;
                    customer.Address2 = rows[0]["CustomerAddress2"] as string;
                    customer.TaxAddress = rows[0]["CustomerTaxAddress"] as string;
                    customer.Lastname = rows[0]["CustomerLastName"] as string;
                    customer.Email = rows[0]["CustomerEmail"] as string;
                    customer.Telephone = rows[0]["CustomerTelephone"] as string;
                    Segment segment = new Segment();
                    segment.SegmentId = (int)rows[0]["CustomerSegmentId"];
                    segment.SegmentName = rows[0]["CustomerSegmentName"] as string;
                    customer.Segment = segment;
                    User user = new User();
                    user.UserId = (int)rows[0]["CustomerUserId"];
                    user.Username = rows[0]["CustomerUserName"] as string;
                    user.Password = rows[0]["CustomerKullaniciPassword"] as string;
                    user.EMail = rows[0]["CustomerUserEmail"] as string;
                    customer.UserId = user.UserId;
                    customer.Password = user.Password;
                    customer.User = user;
                    return customer;
                }
            }
            return null;
        }

        public List<Customer> SelectAll()
        {
            string query = "SP_GetCustomer";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                DataTable datatable = database.DoQuery(command: command);
                return GetCustomers(datatable);
            }
        }

        public List<Customer> SelectByFilter(List<DBFilter> filters)
        {
            string query = "SP_GetCustomer";

            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                foreach (var item in filters)
                {
                    command.Parameters.AddWithValue(item.ParamName, item.ParamValue);
                }

                DataTable datatable = database.DoQuery(command: command);
                return GetCustomers(datatable);
            }
        }
        private List<Customer> GetCustomers(DataTable table)
        {

            var rows = table.Rows;
            if (rows.Count != 0)
            {
                List<Customer> customers = new List<Customer>();
                foreach (DataRow row in rows)
                {
                    Customer customer = new Customer();
                    customer.CustomerId = (int)row["CustomerId"];
                    customer.Firstname = row["CustomerFirstName"] as string;
                    customer.Lastname = row["CustomerLastName"] as string;
                    customer.Address1 = rows[0]["CustomerAddress1"] as string;
                    customer.Address2 = rows[0]["CustomerAddress2"] as string;
                    customer.TaxAddress = rows[0]["CustomerTaxAddress"] as string;

                    customer.Email = row["CustomerEmail"] as string;
                    customer.Telephone = row["CustomerTelephone"] as string;
                    Segment segment = new Segment();
                    segment.SegmentId = (int)row["CustomerSegmentId"];
                    segment.SegmentName = row["CustomerSegmentName"] as string;
                    customer.Segment = segment;
                    User user = new User();
                    user.UserId = (int)row["CustomerUserId"];
                    user.Username = row["CustomerUserName"] as string;
                    user.Password = row["CustomerKullaniciPassword"] as string;
                    user.EMail = row["CustomerUserEmail"] as string;
                    customer.User = user;
                    customer.UserId = user.UserId;
                    customer.Password = user.Password;
                    customers.Add(customer);
                }
                return customers;
            }
            return null;
        }

    }
}
