using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

using System.Threading.Tasks;

namespace TicketToolServices.Repository
{
    class DemoRepository
    {

        private static object MapToActivities(SqlDataReader reader)
        {
            var Model = new
            {

                subjet = reader["agentID"].ToString(),
                description = reader["agentDescription"].ToString(),



            };
            return Model;
        }
        public static async Task<List<object>> SelectAsync(ExecutionContext context)
        {
           
            
            // log.LogInformation(str.ToString());
            var response = new List<object>();
            var str = Conexion.GetConnectionString(context);

            using (SqlConnection conn = new SqlConnection(str))
            {
                await conn.OpenAsync();
                var text = "Select * From Agent";


                using (SqlCommand cmd = new SqlCommand(text, conn))
                {
                    // Execute the command and log the # rows affected.
                    //  var rows = await cmd.ExecuteNonQueryAsync();
                    //   log.LogInformation("rows were updated");



                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToActivities(reader));
                        }
                    }
                    //log.LogInformation();

                }
                return response;
            }


         
        }
       

    }
}
