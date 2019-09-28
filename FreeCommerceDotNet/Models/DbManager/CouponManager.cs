using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using FreeCommerceDotNet.Models.DbModels;
using FreeCommerceDotNet.Models.Interfaces;
using FreeCommerceDotNet.Models.Util;

namespace FreeCommerceDotNet.Models.DbManager
{
    public class CouponManager : IDBOperations<Coupon>, IDisposable
    {
        public void Dispose()
        {
        }

        public int Add(Coupon entry)
        {
            using (SqlCommand command = new SqlCommand("sp_coupon_insert"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                return Utilities.ExecuteCommand<Coupon>(sqlCommand);
            }
        }

        public bool CheckIsExist(int id)
        {
            return false;
        }

        public bool Delete(int id)
        {
            string sqlQuery = "DELETE FROM Coupons WHERE CouponId=@id";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                sqlCommand.Parameters.AddWithValue("@id", id);
                return Utilities.ExecuteCommand<Coupon>(sqlCommand, SqlCommandTypes.Remove);
            }
        }

        public Coupon Get(int id)
        {
            string sqlQuery = "select * from Coupons where CouponId = @id";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                sqlCommand.Parameters.AddWithValue("@id", id);
                var Coupon = new List<Coupon>();
                Utilities.ExecuteCommand<Coupon>(sqlCommand, SqlCommandTypes.Select, ref Coupon);
                return Coupon.FirstOrDefault();
            }
        }

        public List<Coupon> GetAll()
        {
            string sqlQuery = "select * from Coupons";
            using (SqlCommand command = new SqlCommand(sqlQuery))
            {
                var sqlCommand = command;
                var Coupons = new List<Coupon>();
                Utilities.ExecuteCommand<Coupon>(sqlCommand, SqlCommandTypes.Select, ref Coupons);
                return Coupons;
            }
        }

        public List<Coupon> GetByIntegerKey(int id, string tbl, string key)
        {
            return Utilities.GetByIntegerKey<Coupon>(id, tbl, key);
        }

        public int Update(Coupon entry)
        {
            using (SqlCommand command = new SqlCommand("sp_coupon_update"))
            {
                var sqlCommand = command;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = Utilities.CreateUpdateSqlParameters(sqlCommand, entry, entry.GetType().GetProperties());
                Utilities.ExecuteCommand<Coupon>(sqlCommand, SqlCommandTypes.Update);
                return 0;
            }
        }
    }
}