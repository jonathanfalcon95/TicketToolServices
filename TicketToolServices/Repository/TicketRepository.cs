using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketToolServices.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TicketToolServices.Repository
{
    class TicketRepository
    {
      
        internal static void Tickets(Tickets tickets)
        {
            
        }







        /*
        private readonly string _connectionString;
        public TicketRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SQLConnectionString");
        }
        public async Task<List<Tickets>> GetAll()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertUpdateDelete_Ticket", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Query", 4);
                    var response = new List<Tickets>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToTicket(reader));
                        }
                    }

                    return response;
                }
            }
        }

        private Tickets MapToTicket(SqlDataReader reader)
        {
            return new Tickets()
            {
                ticketID = (int)reader["ticketId"],
                customerID = reader["customerID"].ToString(),
                subject = reader["subject"].ToString(),
                description = reader["description"].ToString(),
                status = reader["status"].ToString(),
                priority = reader["priority"].ToString(),
                source = reader["priority"].ToString(),
                type = reader["priority"].ToString(),
                email = reader["priority"].ToString(),
                phoneNumberRequester = reader["priority"].ToString(),
                IDFacebookProfile = reader["priority"].ToString(),
                agentID = reader["priority"].ToString(),
                groupID = reader["priority"].ToString(),
                /*creationDate = reader["creationDate"].ToString().datetime,
                expirationDate = reader["expirationDate"].ToString("yyyy-MM-dd'T'HH:mm:ss.SSS"),
                resolvedDate = reader["resolvedDate"].ToString('2019-08-05T15:02:29.393'),
                closedDate = reader["closedDate"].ToString('2019-08-05T15:02:29.393'),
                lastUpdateDate = reader["lastUpdateDate"].ToString('2019-08-05T15:02:29.393'),
                fistResponseRequestDate = reader["fistResponseRequestDate"].ToString('2019-08-05T15:02:29.393'),*/
        /*    agentInteractions = (int)reader["agentInteractions"],
            customerIntearction = (int)reader["customerIntearction"],
            resolutionStatus = reader["resolutionStatus"].ToString(),
            firstResponseStatus = reader["firstResponseStatus"].ToString(),
            tags = reader["tags"].ToString(),
            surveysResult = reader["surveysResult"].ToString(),
            companyID = reader["companyID"].ToString(),
            customerCompany = reader["customerCompany"].ToString(),
            projectNumber = reader["projectNumber"].ToString(),
            sharePointID = reader["sharePointID"].ToString(),
            quotationID = reader["quotationID"].ToString(),
            customerEstimatedHours = (float)reader["customerEstimatedHours"],
            tmHoursWeek1 = (float)reader["tmHoursWeek1"],
            tmHoursWeek2 = (float)reader["tmHoursWeek2"],
            tmHoursWeek3 = (float)reader["tmHoursWeek3"],
            tmHoursWeek4 = (float)reader["tmHoursWeek4"],
            progressWeek1 = (float)reader["rogressWeek1"],
            progressWeek2 = (float)reader["rogressWeek2"],
            progressWeek3 = (float)reader["tmHoursWeek3"],
            progressWeek4 = (float)reader["progressWeek4"],
           /* billingMonth = reader["billingMonth"].ToString("dd/mm/yy HH:mm:ss"),
            totalBillingHours = (float)reader["totalBillingHours"],
            totalProgress = (float)reader["totalProgress"],
            estimatedStartDate = reader["estimatedStartDate"].ToString("dd/mm/yy HH:mm:ss"),
            estimatedEndDate = reader["estimatedEndDate"].ToString("dd/mm/yy HH:mm:ss"),
            realStartDate = reader["realStartDate"].ToString("dd/mm/yy HH:mm:ss"),
            realEndDate = reader["realEndDate"].ToString("dd/mm/yy HH:mm:ss"),*/
        /*   estimatedHourAgent = (float)reader["estimatedHourAgent"],

       };
   }

   public async Task Update(long Id, Tickets ticket)
   {

       using (SqlConnection sql = new SqlConnection(_connectionString))
       {
           using (SqlCommand cmd = new SqlCommand("sp_InsertUpdateDelete_Ticket", sql))
           {
               cmd.CommandType = System.Data.CommandType.StoredProcedure;
               cmd.Parameters.Add(new SqlParameter("@ticketId", Id));
             //cmd.Parameters.Add(new SqlParameter("@ticketName", ticket.ticketName));
               cmd.Parameters.AddWithValue("@Query", 2);
               await sql.OpenAsync();
               await cmd.ExecuteNonQueryAsync();
               return;
           }
       }
   }

}*/
    }
}