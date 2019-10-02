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

        private static object MapToActivities(SqlDataReader reader)
        {
            var Model = new
            {
                 ticketID = (int)reader["ticketID"],
                 customerID = reader["subject"].ToString(),
                 subject = reader["subject"].ToString(),
                 description = reader["description"].ToString(),
                 status = reader["status"].ToString(),
                 priority = reader["priority"].ToString(),
                 source = reader["source"].ToString(),
                 type = reader["type"].ToString(),
                 companyID = reader["companyID"].ToString(),
                 email = reader["email"].ToString(),
                 phoneNumberRequester = reader["phoneNumberRequester"].ToString(),
                 IDFacebookProfile = reader["IDFacebookProfile"].ToString(),
                 agentID = reader["agentID"].ToString(),
                 groupID = reader["groupID"].ToString(),
                 creationDate = (DateTime)reader["creationDate"],
                 expirationDate = (DateTime)reader["expirationDate"],
                 lastUpdateDate = (DateTime)reader["lastUpdateDate"],
                 resolvedDate = (DateTime)reader["resolvedDate"],
                 closedDate = (DateTime)reader[" closedDate"],
                 fistResponseRequestDate = (DateTime)reader["fistResponseRequestDate"],
                 agentInteractions = (int)reader["agentInteractions"],
                 customerIntearction = (int)reader["customerIntearction"],
                 customerCompany = (int)reader["customerIntearction"],
                 resolutionStatus = reader["customerCompany"].ToString(),
                 firstResponseStatus = reader["firstResponseStatus"].ToString(),
                 projectNumber = reader["projectNumber"].ToString(),
                 quotationID = reader["quotationID"].ToString(),
                 sharePointID = reader["harePointID"].ToString(),
                 customerEstimatedHours = (float)reader["customerEstimatedHours"],
                 tmHoursWeek1 = (float)reader["tmHoursWeek1"],
                 tmHoursWeek2 = (float)reader["tmHoursWeek2"],
                 tmHoursWeek3 = (float)reader["tmHoursWeek3"],
                 tmHoursWeek4 = (float)reader["tmHoursWeek4"],
                 progressWeek1 = (float)reader["progressWeek1"],
                 progressWeek2 = (float)reader["progressWeek2"],
                 progressWeek3 = (float)reader["progressWeek3"],
                 progressWeek4 = (float)reader["progressWeek4"],
                 billingMonth = (DateTime)reader["billingMonth"],
                 totalBillingHours = (float)reader["totalBillingHours"],
                 totalProgress = (float)reader["totalProgress"],
                estimatedStartDate = (DateTime)reader["estimatedStartDate"],
                estimatedEndDate = (DateTime)reader["estimatedEndDate"],
                realStartDate = (DateTime)reader["realStartDate"],
                realEndDate = (DateTime)reader["realEndDate"],
                estimatedHourAgent = (DateTime)reader["estimatedHourAgent"],


            };
            return Model;
        }

        public static async Task Post(Tickets ticketsreq, Tickets ticketsdes,  ExecutionContext context)
        {

            var str = Conexion.GetConnectionString(context);

            using (SqlConnection sql = new SqlConnection(str))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Insert_ticketss", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@ticketID", ticketsreq.TicketID));
                    cmd.Parameters.Add(new SqlParameter("@customerID", ticketsreq.customerID));
                    cmd.Parameters.Add(new SqlParameter("@description", ((ticketsdes.description != null) )));
                    cmd.Parameters.Add(new SqlParameter("@subject", ticketsreq.subject));
                    cmd.Parameters.Add(new SqlParameter("@status", ticketsreq.status));
                    cmd.Parameters.Add(new SqlParameter("@priority", ticketsreq.priority));
                    cmd.Parameters.Add(new SqlParameter("@source", ticketsreq.source));
                    cmd.Parameters.Add(new SqlParameter("@type", ticketsreq.type));
                    cmd.Parameters.Add(new SqlParameter("@companyID", ticketsreq.companyID));
                    cmd.Parameters.Add(new SqlParameter("@email", ticketsreq.email));
                    cmd.Parameters.Add(new SqlParameter("@phoneNumberRequester", ticketsreq.phoneNumberRequester));


                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        public static async Task<List<object>> SelectAsync(ExecutionContext context)
        {

            // log.LogInformation(str.ToString());
            var response = new List<object>();
            var str = Conexion.GetConnectionString(context);

            using (SqlConnection conn = new SqlConnection(str))
            {
                await conn.OpenAsync();
                var text = "Select * From Ticket";


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