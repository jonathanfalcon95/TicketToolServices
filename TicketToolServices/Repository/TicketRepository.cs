using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using TicketToolServices.Models;

using System.Threading.Tasks;

namespace TicketToolServices.Repository
{
    class TicketRepository
    {
        public static async Task Post(Tickets tickets, ExecutionContext context)
        {
            DateTime parsedDate = DateTime.Parse(tickets.billingMonth);
            var str = Conexion.GetConnectionString(context);
            //int resp = 0;
            using (SqlConnection sql = new SqlConnection(str))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.sp_Insert_Ticket", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ticketID", tickets.TicketID));
                    cmd.Parameters.Add(new SqlParameter("@customerID", tickets.customerID));

                  
                    cmd.Parameters.Add(new SqlParameter("@subject", tickets.subject));
                    cmd.Parameters.Add(new SqlParameter("@description", tickets.description));
                    cmd.Parameters.Add(new SqlParameter("@status", tickets.status));
                    cmd.Parameters.Add(new SqlParameter("@priority", tickets.priority));
                    cmd.Parameters.Add(new SqlParameter("@source", tickets.source));
                    cmd.Parameters.Add(new SqlParameter("@type", tickets.type));
                    //cmd.Parameters.Add(new SqlParameter("@email", tickets.email));
                    //cmd.Parameters.Add(new SqlParameter("@phoneNumberRequester", tickets.phoneNumberRequester));
                    //cmd.Parameters.Add(new SqlParameter("@IDFacebookProfile", tickets.IDFacebookProfile));
                    cmd.Parameters.Add(new SqlParameter("@agentID", tickets.agentID));
                    cmd.Parameters.Add(new SqlParameter("@groupID", tickets.groupID));
                    cmd.Parameters.Add(new SqlParameter("@creationDate", tickets.creationDate));
                    cmd.Parameters.Add(new SqlParameter("@expirationDate", tickets.expirationDate));
                    cmd.Parameters.Add(new SqlParameter("@resolvedDate", tickets.resolvedDate));
                    cmd.Parameters.Add(new SqlParameter("@closedDate", tickets.closedDate));
                    cmd.Parameters.Add(new SqlParameter("@lastUpdateDate", tickets.lastUpdateDate));
                    cmd.Parameters.Add(new SqlParameter("@fistResponseRequestDate", tickets.fistResponseRequestDate));
                    //cmd.Parameters.Add(new SqlParameter("@agentInteractions", tickets.agentInteractions));
                    //cmd.Parameters.Add(new SqlParameter("@customerInteraction", tickets.customerInteraction));
                    //cmd.Parameters.Add(new SqlParameter("@resolutionStatus", "prueba"));
                    //cmd.Parameters.Add(new SqlParameter("@firstResponseStatus", "prueba"));
                    //cmd.Parameters.Add(new SqlParameter("@tags", "prueba"));
                    //cmd.Parameters.Add(new SqlParameter("@surveysResult", "prueba"));
                    cmd.Parameters.Add(new SqlParameter("@companyID", tickets.companyID));
                    cmd.Parameters.Add(new SqlParameter("@customerCompany", tickets.customerCompany));
                    cmd.Parameters.Add(new SqlParameter("@projectNumber", tickets.projectNumber));
                    cmd.Parameters.Add(new SqlParameter("@quotationID", tickets.quotationID));
                    cmd.Parameters.Add(new SqlParameter("@sharePointID", tickets.sharePointID));
                    cmd.Parameters.Add(new SqlParameter("@customerEstimatedHours", Convert.ToDouble(tickets.customerEstimatedHours)));
                    cmd.Parameters.Add(new SqlParameter("@tmHoursWeek1", tickets.tmHoursWeek1));
                    cmd.Parameters.Add(new SqlParameter("@tmHoursWeek2", tickets.tmHoursWeek2));
                    cmd.Parameters.Add(new SqlParameter("@tmHoursWeek3", tickets.tmHoursWeek3));
                    cmd.Parameters.Add(new SqlParameter("@tmHoursWeek4", tickets.tmHoursWeek4));
                    cmd.Parameters.Add(new SqlParameter("@progressWeek1", tickets.progressWeek1));
                    cmd.Parameters.Add(new SqlParameter("@progressWeek2", tickets.progressWeek2));
                    cmd.Parameters.Add(new SqlParameter("@progressWeek3", tickets.progressWeek3));
                    cmd.Parameters.Add(new SqlParameter("@progressWeek4", tickets.progressWeek4));
                    cmd.Parameters.Add(new SqlParameter("@billingMonth", parsedDate));
                    cmd.Parameters.Add(new SqlParameter("@totalBillingHours", tickets.totalBillingHours));
                    cmd.Parameters.Add(new SqlParameter("@totalProgress", tickets.totalProgress));
                    cmd.Parameters.Add(new SqlParameter("@estimatedStartDate", tickets.estimatedStartDate));
                    cmd.Parameters.Add(new SqlParameter("@estimatedEndDate", tickets.estimatedEndDate));
                    cmd.Parameters.Add(new SqlParameter("@realStartDate", tickets.realStartDate));
                    cmd.Parameters.Add(new SqlParameter("@realEndDate", tickets.realEndDate));
                    cmd.Parameters.Add(new SqlParameter("@estimatedHourAgent", tickets.estimatedHourAgent));
                    await sql.OpenAsync();
                    int resp = await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

       /* public static object MapToActivities(SqlDataReader reader)
        {
            var Model = new
            {

                lastExecutedDate = reader["lastExecutedDate"].ToString(),

            };
            return Model;
        }
        public static async Task<List<object>> SelectAsync(ExecutionContext context)
        {
            // log.LogInformation(str.ToString());
            var response2 = new List<object>();
            var str = Conexion.GetConnectionString(context);
            using (SqlConnection conn = new SqlConnection(str))
            {
                await conn.OpenAsync();
                var text = "SELECT lastExecutedDate FROM dbo.SFControlData";
                using (SqlCommand cmd = new SqlCommand(text, conn))
                {
                    // Execute the command and log the # rows affected.
                    //  var rows = await cmd.ExecuteNonQueryAsync();
                    //   log.LogInformation("rows were updated");
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response2.Add(MapToActivities(reader));
                        }
                    }
                    //log.LogInformation();

                }
                return response2;
            }

        }*/

    }
}