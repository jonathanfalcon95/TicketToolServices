using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

using System.Threading.Tasks;
using TicketToolServices.Models;

namespace TicketToolServices.Repository
{
    class ConversationsRepository
    {

        private static object MapToActivities(SqlDataReader reader)
        {
            var Model = new
            {

                body_text = reader["body_text"].ToString(),
                id = (long)reader["id"],
                incoming = (bool)reader["incoming"],
                @private = (bool)reader["private"],
                user_id = reader["user_id"].ToString(),
                support_email = reader["support_email"].ToString(),
                source = (int)reader["source"],
                category = (int)reader["category"],
                ticket_id = (int)reader["ticket_id"],
                to_emails = reader["to_emails"].ToString(),
                from_email = reader["from_email"].ToString(),
                cc_emails = reader["cc_emails"].ToString(),
                bcc_emails = reader["bcc_emails"].ToString(),
                email_failure_count = reader["email_failure_count"].ToString(),
                created_at = (DateTime)reader["created_at"],
                updated_at = (DateTime)reader["updated_at"]



            };
            return Model;
        }

        public static string Arrays(List<string> To_Emails)
        {
            string cadena = new string("");
            for (int i = 0; i < To_Emails.Count; i++)
            {
                cadena = cadena + Convert.ToString(To_Emails[i]);
                if (i != To_Emails.Count - 1) { cadena = cadena + ","; }
            }
            return cadena;
        }
        
        public static async Task Post(Conversations conversation, ExecutionContext context)
        {


            var str = Conexion.GetConnectionString(context);

            using (SqlConnection sql = new SqlConnection(str))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.sp_Insert_Conversations", sql))
                {
                   
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@body_text", string.IsNullOrEmpty(conversation.body_text) ? "" : conversation.body_text));
                    cmd.Parameters.Add(new SqlParameter("@id", conversation.id));
                    cmd.Parameters.Add(new SqlParameter("@incoming", conversation.incoming ? 1 : 0));
                    cmd.Parameters.Add(new SqlParameter("@private", conversation.@private ? 1 : 0));
                    cmd.Parameters.Add(new SqlParameter("@user_id", string.IsNullOrEmpty(conversation.user_id) ? "" : conversation.user_id));
                    cmd.Parameters.Add(new SqlParameter("@support_email", string.IsNullOrEmpty(conversation.support_email) ? "" : conversation.support_email));
                    cmd.Parameters.Add(new SqlParameter("@source", conversation.source));
                    cmd.Parameters.Add(new SqlParameter("@category", conversation.category));
                    cmd.Parameters.Add(new SqlParameter("@ticket_id", conversation.ticket_id));
                    cmd.Parameters.Add(new SqlParameter("@to_emails", ((conversation.to_emails != null) && (conversation.to_emails.Count > 0)) ? conversation.to_emails[0] : ""));
                    cmd.Parameters.Add(new SqlParameter("@cc_emails", ((conversation.cc_emails != null) && (conversation.cc_emails.Count > 0)) ? conversation.cc_emails[0] : ""));
                    cmd.Parameters.Add(new SqlParameter("@from_email", string.IsNullOrEmpty(conversation.from_email) ? "" : conversation.from_email));
                    cmd.Parameters.Add(new SqlParameter("@bcc_emails", ((conversation.bcc_emails != null) && (conversation.bcc_emails.Count > 0)) ? conversation.bcc_emails[0] : ""));
                    cmd.Parameters.Add(new SqlParameter("@email_failure_count", string.IsNullOrEmpty(conversation.email_failure_count)  ? "" : conversation.email_failure_count));
                    cmd.Parameters.Add(new SqlParameter("@created_at", conversation.created_at));
                    cmd.Parameters.Add(new SqlParameter("@updated_at", conversation.updated_at));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

    
        public static async Task<List<object>> SelectAsync(ExecutionContext context)
        {

            var response = new List<object>();
            var str = Conexion.GetConnectionString(context);

            using (SqlConnection conn = new SqlConnection(str))
            {
                await conn.OpenAsync();
                var text = "Select * From Conversations";


                using (SqlCommand cmd = new SqlCommand(text, conn))
                {

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToActivities(reader));
                        }
                    }
                }
                return response;
            }
        }
       

    }
}
