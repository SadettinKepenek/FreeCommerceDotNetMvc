using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FreeCommerceDotNet.Common.Concrete
{
    public static class LinqHelper
    {
        public static IEnumerable<IGrouping<T, DataRow>> GroupDataTableByField<T>(DataTable queryResult, string fieldName)
        {
            /// T Gelen DataTable daki grouplandırılmak istenen columnun veri tipi
            /// field name ise column'ın adı
            
            var groupedData = queryResult.AsEnumerable()
                .GroupBy(row => row.Field<T>(fieldName))
                .Select(grp => grp);
            return groupedData;
        }
    }
}