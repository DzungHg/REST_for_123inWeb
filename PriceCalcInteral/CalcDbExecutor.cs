using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace PriceCalcInternal
{
    /// <summary>
    /// Lớp hỗ trợ lấy dữ liệu từ MSSQL
    /// </summary>
    public class CalcDbExecutor
    {
        /// <summary>
        /// Lấy IDbCommand đã khởi tạo sẵn IDbConnection
        /// </summary>
        /// <returns></returns>
        public static IDbCommand GetDbCommand()
        {
            var connectionString = ConfigurationManager.ConnectionStrings[0].ConnectionString;
            var connection = new SqlConnection(connectionString);
            var command = connection.CreateCommand();

            return command;
        }

        /// <summary>
        /// Lấy dữ liệu từ câu truy vấn SQL
        /// </summary>
        /// <param name="query">Câu truy vấn SQL</param>
        /// <param name="parameters">Tham số để xác định dữ liệu cho câu truy vấn</param>
        /// <returns></returns>
        public static DataTable ExecuteQuery(string query, IDictionary<string, object> parameters = null)
        {
            var command = GetDbCommand();

            command.CommandText = query;

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(new SqlParameter(parameter.Key, parameter.Value));
                }
            }

            var dataAdapter = new SqlDataAdapter((SqlCommand)command);
            var tblResult = new DataTable();
            dataAdapter.Fill(tblResult);

            command.Connection.Dispose();
            command.Dispose();

            return tblResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string query, IDictionary<string, object> parameters = null)
        {
            var command = GetDbCommand();

            command.CommandText = query;

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(new SqlParameter(parameter.Key, parameter.Value));
                }
            }

            if (command.Connection.State != ConnectionState.Open)
            {
                command.Connection.Open();
            }

            var result = command.ExecuteScalar();

            command.Connection.Close();

            return result;
        }
    }
}
