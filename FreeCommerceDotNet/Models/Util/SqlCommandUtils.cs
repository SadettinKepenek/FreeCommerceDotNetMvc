using System.Collections.Generic;
using System.Data.SqlClient;

namespace FreeCommerceDotNet.Models.Util
{
    public class SqlCommandUtilModel
    {
        public string parameterName { get; set; }
        public string propertyName { get; set; }
    }
    public class SqlCommandUtils
    {
        public SqlCommand InitParameters(List<SqlCommandUtilModel> model,SqlCommand command)
        {
            foreach (SqlCommandUtilModel utilModel in model)
            {
                command.Parameters.AddWithValue(utilModel.parameterName, utilModel.propertyName);
                return command;
            }
            return null;

        }
    }
}