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
        public static async Task<dynamic> SelectAsync(ExecutionContext context)
        {
            var response3 = new List<dynamic>();
            var str = Conexion.GetConnectionString(context);
            using (SqlConnection conn = new SqlConnection(str))
            {
                await conn.OpenAsync();
                var text = "SELECT lastExecutedDate FROM dbo.SFControlData";
                using (SqlCommand cmd = new SqlCommand(text, conn))
                {
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response3.Add(MapToActivities(reader));
                        }
                    }

                }
                return response3;
            }

        }
    }
}
