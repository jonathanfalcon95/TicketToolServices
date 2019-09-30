using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

using System.Threading.Tasks;

namespace TicketToolServices.Repository
{
    class TicketRepository
    {

        private static object MapToActivities(SqlDataReader reader)
        {
            var Model = new
            {

                /* ticketID = (int)reader["ticketId"],
                 customerID = reader["customerID"].ToString(),
                 subject = reader["subject"].ToString(),
                 description = reader["description"].ToString(),
                 status = reader["status"].ToString(),
                 priority = reader["priority"].ToString(),
                 source = reader["priority"].ToString(),
                 type = reader["priority"].ToString(),
                 companyID = reader["priority"].ToString(),
                 customerID = reader["priority"].ToString(),
                 agentID
                 groupID
                 creationDate
                 expirationDate
                 lastUpdateDate
                 resolvedDate
                 closedDate
                 fistResponseRequestDate
                 customerCompany
                 projectNumber
                 quotationID
                 sharePointID
                 customerEstimatedHours
                 tmHoursWeek1
                 tmHoursWeek2
                 tmHoursWeek3
                 tmHoursWeek4
                 progressWeek1
                 progressWeek2
                 progressWeek3
                 progressWeek4
                 billingMonth
                 totalBillingHours
                 totalProgress
                 estimatedStartDate
                 estimatedEndDate
                 realStartDate
                 realEndDate
                 estimatedHourAgent*/


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
                var text = "Select * From ticketID";


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