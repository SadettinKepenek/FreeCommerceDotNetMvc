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
    public class StoreRepository : IStoreDal
    {
        MsSQLDatabase database = new MsSQLDatabase();

        public DBResult Insert(Store entity)
        {
            string query = "StoreInsertUpdateDelete";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "INSERT");
                command.Parameters.AddWithValue("@MetaTitle", entity.MetaTitle);
                command.Parameters.AddWithValue("@MetaTagKeywords", entity.MetaTagKeywords);
                command.Parameters.AddWithValue("@MetaTagDescription", entity.MetaTagDescription);
                command.Parameters.AddWithValue("@StoreName", entity.StoreName);
                command.Parameters.AddWithValue("@Phone", entity.Phone);
                command.Parameters.AddWithValue("@StoreOwner", entity.StoreOwner);
                command.Parameters.AddWithValue("@Address", entity.Address);
                command.Parameters.AddWithValue("@EMail", entity.EMail);
                command.Parameters.AddWithValue("@CellPhone", entity.CellPhone);
                command.Parameters.AddWithValue("@Fax", entity.Fax);
                command.Parameters.AddWithValue("@ImageUrl", entity.ImageUrl);
                command.Parameters.AddWithValue("@OpeningTimes", entity.OpeningTimes);
                command.Parameters.AddWithValue("@Comment", entity.Comment);
                command.Parameters.AddWithValue("@AllowReviews", entity.AllowReviews);
                command.Parameters.AddWithValue("@MaxLoginAttempts", entity.MaxLoginAttempts);
                command.Parameters.AddWithValue("@LoginDisplayPrices", entity.LoginDisplayPrices);
                command.Parameters.AddWithValue("@DisplayPricesWithTax", entity.DisplayPricesWithTax);
                command.Parameters.AddWithValue("@DisplayStock", entity.DisplayStock);
                command.Parameters.AddWithValue("@ShowOutOfStockWarning", entity.ShowOutOfStockWarning);
                DataTable datatable = database.DoQuery(command: command);
                if (datatable.Rows.Count != 0)
                {
                    return database.ReadResultFromDataTable(datatable);

                }
            }

            return null;
        }

        public DBResult Update(Store entity)
        {
            string query = "StoreInsertUpdateDelete";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "UPDATE");
                command.Parameters.AddWithValue("@MetaTitle", entity.MetaTitle);
                command.Parameters.AddWithValue("@StoreId", entity.StoreId);
                command.Parameters.AddWithValue("@MetaTagKeywords", entity.MetaTagKeywords);
                command.Parameters.AddWithValue("@MetaTagDescription", entity.MetaTagDescription);
                command.Parameters.AddWithValue("@StoreName", entity.StoreName);
                command.Parameters.AddWithValue("@Phone", entity.Phone);
                command.Parameters.AddWithValue("@StoreOwner", entity.StoreOwner);
                command.Parameters.AddWithValue("@Address", entity.Address);
                command.Parameters.AddWithValue("@EMail", entity.EMail);
                command.Parameters.AddWithValue("@CellPhone", entity.CellPhone);
                command.Parameters.AddWithValue("@Fax", entity.Fax);
                command.Parameters.AddWithValue("@ImageUrl", entity.ImageUrl);
                command.Parameters.AddWithValue("@OpeningTimes", entity.OpeningTimes);
                command.Parameters.AddWithValue("@Comment", entity.Comment);
                command.Parameters.AddWithValue("@AllowReviews", entity.AllowReviews);
                command.Parameters.AddWithValue("@MaxLoginAttempts", entity.MaxLoginAttempts);
                command.Parameters.AddWithValue("@LoginDisplayPrices", entity.LoginDisplayPrices);
                command.Parameters.AddWithValue("@DisplayPricesWithTax", entity.DisplayPricesWithTax);
                command.Parameters.AddWithValue("@DisplayStock", entity.DisplayStock);
                command.Parameters.AddWithValue("@ShowOutOfStockWarning", entity.ShowOutOfStockWarning);
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
            string query = "StoreInsertUpdateDelete";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Action", "DELETE");
                command.Parameters.AddWithValue("@StoreId", id);
                DataTable datatable = database.DoQuery(command: command);
                if (datatable.Rows.Count != 0)
                {
                    return database.ReadResultFromDataTable(datatable);

                }
            }
            return null;
        }

        public List<Store> SelectAll()
        {
            string query = "SP_GetStore";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                DataTable datatable = database.DoQuery(command: command);
                return GetStores(datatable);
            }
        }

        public Store SelectById(int id)
        {
            string query = "SP_GetStore";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                DataTable datatable = database.DoQuery(command: command);
                var rows = datatable.Rows;
                if (rows.Count != 0)
                {
                    Store store = new Store();
                    store.StoreId = (int)rows[0]["StoreId"];
                    store.MetaTitle = rows[0]["MetaTitle"] as string;
                    store.MetaTagKeywords = rows[0]["MetaTagKeywords"] as string;
                    store.MetaTagDescription = rows[0]["MetaTagDescription"] as string;
                    store.ImageUrl = rows[0]["ImageUrl"] as string;
                    store.Fax = rows[0]["Fax"] as string;
                    store.StoreName = rows[0]["StoreName"] as string;
                    store.StoreOwner = rows[0]["StoreOwner"] as string;
                    store.OpeningTimes = rows[0]["OpeningTimes"] as string;
                    store.Phone = rows[0]["Phone"] as string;
                    store.CellPhone = rows[0]["CellPhone"] as string;
                    store.EMail = rows[0]["EMail"] as string;
                    store.Comment = rows[0]["Comment"] as string;
                    store.DisplayStock = (bool)rows[0]["DisplayStock"];
                    store.DisplayPricesWithTax = (bool)rows[0]["DisplayPricesWithTax"];
                    store.AllowReviews = (bool)rows[0]["AllowReviews"];
                    store.LoginDisplayPrices = (bool)rows[0]["LoginDisplayPrices"];
                    store.DisplayPricesWithTax = (bool)rows[0]["DisplayPricesWithTax"];
                    store.MaxLoginAttempts = (bool)rows[0]["MaxLoginAttempts"];
                    store.ShowOutOfStockWarning = (bool)rows[0]["ShowOutOfStockWarning"];
                    return store;
                }
                return null;
            }
        }

            public List<Store> SelectByFilter(List<DBFilter> filters)
            {
            string query = "SP_GetStore";
            using (var connection = database.CreateConnection())
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.StoredProcedure;
                foreach (var item in filters)
                {
                    command.Parameters.AddWithValue(item.ParamName,item.ParamValue);
                }
                DataTable datatable = database.DoQuery(command: command);
                return GetStores(datatable);
            }
        }

            private List<Store> GetStores(DataTable table)
            {
                var rows = table.Rows;
                if (rows.Count != 0)
                {
                    List<Store> stores = new List<Store>();
                    foreach (DataRow row in rows)
                    {
                        Store store = new Store();
                        store.StoreId = (int)row["StoreId"];
                        store.MetaTitle = row["MetaTitle"] as string;
                        store.MetaTagKeywords = row["MetaTagKeywords"] as string;
                        store.MetaTagDescription = row["MetaTagDescription"] as string;
                        store.ImageUrl = row["ImageUrl"] as string;
                        store.Fax = row["Fax"] as string;
                        store.StoreName = row["StoreName"] as string;
                        store.StoreOwner = row["StoreOwner"] as string;
                        store.OpeningTimes = row["OpeningTimes"] as string;
                        store.Phone = row["Phone"] as string;
                        store.CellPhone = row["CellPhone"] as string;
                        store.EMail = row["EMail"] as string;
                        store.Comment = row["Comment"] as string;
                        store.DisplayStock = (bool)row["DisplayStock"];
                        store.DisplayPricesWithTax = (bool)row["DisplayPricesWithTax"];
                        store.AllowReviews = (bool)row["AllowReviews"];
                        store.LoginDisplayPrices = (bool)row["LoginDisplayPrices"];
                        store.DisplayPricesWithTax = (bool)row["DisplayPricesWithTax"];
                        store.MaxLoginAttempts = (bool)row["MaxLoginAttempts"];
                        store.ShowOutOfStockWarning = (bool)row["ShowOutOfStockWarning"];
                        stores.Add(store);
                    }
                    return stores;
                }
                return null;
            }



        }
    }
