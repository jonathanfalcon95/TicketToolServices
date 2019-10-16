using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using TicketToolServices.Models;
using System.Threading.Tasks;

namespace TicketToolServices.Repository
{
    class SFControlDataRepository
    {

        public static dynamic MapToActivities(SqlDataReader reader)
        {
            var Model = new
            {

                lastExecutedDate = reader["lastExecutedDate"].ToString(),

            };
            return Model;
        }
        public static string SelectAsync(ExecutionContext context)
        {
            string response = "";
            var str = Conexion.GetConnectionString(context);
            using (SqlConnection conn = new SqlConnection(str))
            {
                conn.Open();
                var text = "SELECT lastExecutedDate FROM dbo.SFControlData";
                using (SqlCommand cmd = new SqlCommand(text, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            response =(string)reader[0];
                        }
                    }

                }
                return response;
            }

        }
    }
}
