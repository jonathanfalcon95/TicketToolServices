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
                    cmd.Parameters.Add(new SqlParameter("@agentID", tickets.agentID));
                    cmd.Parameters.Add(new SqlParameter("@groupID", tickets.groupID));
                    cmd.Parameters.Add(new SqlParameter("@creationDate", tickets.creationDate));
                    cmd.Parameters.Add(new SqlParameter("@expirationDate", tickets.expirationDate));
                    cmd.Parameters.Add(new SqlParameter("@resolvedDate", tickets.resolvedDate));
                    cmd.Parameters.Add(new SqlParameter("@closedDate", tickets.closedDate));
                    cmd.Parameters.Add(new SqlParameter("@fistResponseRequestDate", tickets.fistResponseRequestDate));
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
    }
}