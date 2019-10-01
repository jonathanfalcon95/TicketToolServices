using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using TicketToolServices.Models;
using TicketToolServices.Repository;

namespace TicketToolServices.Controllers
{
    
    public static class ConversationsController 
    {
        

        [FunctionName("ConversationsFunction")]
        public static async Task<ActionResult<object>> ConversationsFunction([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "conversations")] HttpRequest req, ILogger log, ExecutionContext context)
        {
            var response = Conexion.GetDataApi("/tickets");

            if (response.IsSuccessStatusCode)
            {
                
                var dataObjectsTickets = response.Content.ReadAsAsync<IEnumerable<dynamic>>().Result;

                    foreach (var item in dataObjectsTickets)
                    {

                        var tickets = new Tickets()
                        {
                            
                            id = item.id,

                        };

                        var responseConversations = Conexion.GetDataApi("/tickets/" + tickets.id + "/conversations");

                        if (responseConversations.IsSuccessStatusCode)
                        {
                            var dataObjectsConversations = responseConversations.Content.ReadAsAsync<IEnumerable<Conversations>>().Result;

                            foreach (var itemC in dataObjectsConversations)
                            {
                                var conversation = new Conversations()
                                {
                                    body_text = itemC.body_text,
                                    id = itemC.id,
                                    incoming = itemC.incoming,
                                    @private = itemC.@private,
                                    user_id = itemC.user_id,
                                    support_email = itemC.support_email,
                                    source = itemC.source,
                                    category = itemC.category,
                                    ticket_id = itemC.ticket_id,
                                    to_emails = itemC.to_emails,
                                    from_email = itemC.from_email,
                                    cc_emails = itemC.cc_emails,
                                    bcc_emails = itemC.bcc_emails,
                                    email_failure_count = itemC.email_failure_count,
                                    created_at = itemC.created_at,
                                    updated_at = itemC.updated_at
                                };
                               //Console.WriteLine(conversation.to_emails.Count > 0 ? conversation.to_emails[0] : "null");
                                await ConversationsRepository.Post(conversation, context);
                            }
                        }
                    }
                    
            }

            return await ConversationsRepository.SelectAsync(context);

        }

    }



}
