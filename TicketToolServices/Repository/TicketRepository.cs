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
                subject = reader["subject"].ToString(),
                description = reader["description"].ToString(),
                status = (int)reader["status"],
                priority = reader["priority"].ToString(),
                source = reader["source"].ToString(),
                type = reader["type"].ToString(),
                companyID = reader["companyID"].ToString(),
                // email = reader["email"].ToString(),
                customerID = reader["customerID"].ToString(),
                // phoneNumberRequester = reader["phoneNumberRequester"].ToString(),
                // IDFacebookProfile = reader["IDFacebookProfile"].ToString(),
                agentID = (long)reader["agentID"],
                groupID = (long)reader["groupID"],
                creationDate = reader["creationDate"].ToString(),
                expirationDate = reader["expirationDate"].ToString(),
                lastUpdateDate = reader["lastUpdateDate"].ToString(),
                resolvedDate = reader["resolvedDate"].ToString(),
                closedDate = reader["closedDate"].ToString(),
                fistResponseRequestDate = reader["fistResponseRequestDate"].ToString(),
                customerCompany = reader["customerCompany"].ToString(),
                // agentInteractions = (int)reader["agentInteractions"],
                //  customerIntearction = (int)reader["customerIntearction"],
                //  resolutionStatus = reader["customerCompany"].ToString(),
                //  firstResponseStatus = reader["firstResponseStatus"].ToString(),
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
                billingMonth = reader["billingMonth"].ToString(),
                totalBillingHours = reader["totalBillingHours"].ToString(),
                totalProgress = reader["totalProgress"].ToString(),
                estimatedStartDate = reader["estimatedStartDate"].ToString(),
                estimatedEndDate = reader["estimatedEndDate"].ToString(),
                realStartDate = reader["realStartDate"].ToString(),
                realEndDate = reader["realEndDate"].ToString(),
                estimatedHourAgent = (float)reader["estimatedHourAgent"],


            };
            return Model;
        }



        public static async Task Post(Tickets tickets, ExecutionContext context)
        {

            var str = Conexion.GetConnectionString(context);

            using (SqlConnection sql = new SqlConnection(str))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Insert_Ticket", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@ticketID", tickets.TicketID));
                    cmd.Parameters.Add(new SqlParameter("@subject", tickets.subject));
                    cmd.Parameters.Add(new SqlParameter("@description", tickets.description));
                    cmd.Parameters.Add(new SqlParameter("@status", tickets.status));
                    cmd.Parameters.Add(new SqlParameter("@priority", tickets.priority));
                    cmd.Parameters.Add(new SqlParameter("@source", tickets.source));
                    cmd.Parameters.Add(new SqlParameter("@type", tickets.type));
                    cmd.Parameters.Add(new SqlParameter("@companyID", tickets.companyID));
                    cmd.Parameters.Add(new SqlParameter("@customerID", tickets.customerID));
                    cmd.Parameters.Add(new SqlParameter("@agentID", tickets.agentID));
                    cmd.Parameters.Add(new SqlParameter("@groupID", tickets.groupID));
                    cmd.Parameters.Add(new SqlParameter("@creationDate", tickets.creationDate));
                    cmd.Parameters.Add(new SqlParameter("@expirationDate", tickets.expirationDate));
                    cmd.Parameters.Add(new SqlParameter("@lastUpdateDate", tickets.lastUpdateDate));
                    cmd.Parameters.Add(new SqlParameter("@resolvedDate", tickets.lastUpdateDate));
                    cmd.Parameters.Add(new SqlParameter("@closedDate", tickets.closedDate));
                    cmd.Parameters.Add(new SqlParameter("@fistResponseRequestDate", tickets.fistResponseRequestDate));
                    cmd.Parameters.Add(new SqlParameter("@customerCompany", tickets.customerCompany));
                    cmd.Parameters.Add(new SqlParameter("@projectNumber", tickets.projectNumber));
                    cmd.Parameters.Add(new SqlParameter("@quotationID", tickets.quotationID));
                    cmd.Parameters.Add(new SqlParameter("@sharePointID", tickets.sharePointID));
                    cmd.Parameters.Add(new SqlParameter("@customerEstimatedHours", tickets.customerEstimatedHours));
                    cmd.Parameters.Add(new SqlParameter("@tmHoursWeek1", tickets.tmHoursWeek1));
                    cmd.Parameters.Add(new SqlParameter("@tmHoursWeek2", tickets.tmHoursWeek2));
                    cmd.Parameters.Add(new SqlParameter("@tmHoursWeek3", tickets.tmHoursWeek3));
                    cmd.Parameters.Add(new SqlParameter("@tmHoursWeek4", tickets.tmHoursWeek4));
                    cmd.Parameters.Add(new SqlParameter("@progressWeek1", tickets.progressWeek1));
                    cmd.Parameters.Add(new SqlParameter("@progressWeek2", tickets.progressWeek2));
                    cmd.Parameters.Add(new SqlParameter("@progressWeek3", tickets.progressWeek3));
                    cmd.Parameters.Add(new SqlParameter("@progressWeek4", tickets.progressWeek4));
                    cmd.Parameters.Add(new SqlParameter("@billingMonth", tickets.billingMonth));
                    cmd.Parameters.Add(new SqlParameter("@totalBillingHours", tickets.totalBillingHours));
                    cmd.Parameters.Add(new SqlParameter("@totalProgress", tickets.totalProgress));
                    cmd.Parameters.Add(new SqlParameter("@estimatedStartDate", tickets.estimatedStartDate));
                    cmd.Parameters.Add(new SqlParameter("@estimatedEndDate", tickets.estimatedEndDate));
                    cmd.Parameters.Add(new SqlParameter("@realStartDate", tickets.realStartDate));
                    cmd.Parameters.Add(new SqlParameter("@realStartDate", tickets.realStartDate));
                    cmd.Parameters.Add(new SqlParameter("@realEndDate", tickets.realEndDate));
                    cmd.Parameters.Add(new SqlParameter("@estimatedHourAgent", tickets.estimatedHourAgent));

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