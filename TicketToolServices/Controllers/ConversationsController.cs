using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public class ConversationsController
    {
        public async void ConversationsUpdate(ExecutionContext context, string url)
        {
            var response = Conexion.GetDataApi(url);
            if (response.IsSuccessStatusCode)
            {
                var dataObjectsTickets = response.Content.ReadAsAsync<IEnumerable<dynamic>>().Result;
                foreach (var item in dataObjectsTickets)
                {
                    var tickets = new Tickets()
                    {
                        TicketID = item.id,
                    };
                    var responseConversations = Conexion.GetDataApi("/tickets/" + tickets.TicketID + "/conversations");
                    if (responseConversations.IsSuccessStatusCode)
                    {
                        var dataObjectsConversations = responseConversations.Content.ReadAsAsync<IEnumerable<dynamic>>().Result;
                        foreach (var itemC in dataObjectsConversations)
                        {
                            Console.WriteLine(itemC.id);
                            JArray to_emails = (JArray)itemC["to_emails"];
                            JArray cc_emails = (JArray)itemC["to_emails"];
                            JArray bcc_emails = (JArray)itemC["to_emails"];
                            var conversation = new Conversations()
                            {
                                body_text = (string)itemC.body_text,
                                id = itemC.id,
                                incoming = itemC.incoming,
                                @private = itemC.@private,
                                user_id = (string)itemC.user_id,
                                support_email = (string)itemC.support_email,
                                source = itemC.source,
                                category = itemC.category,
                                ticket_id = itemC.ticket_id,
                                to_emails = to_emails.ToObject<List<string>>(),//(itemC.to_emails is null) ? new List<string>() : itemC.to_emails,
                                from_email = (string)itemC.from_email,
                                cc_emails = cc_emails.ToObject<List<string>>(),//(itemC.cc_emails is null) ? new List<string>() : itemC.cc_emails,
                                bcc_emails = bcc_emails.ToObject<List<string>>(), //(itemC.bcc_emails is null) ? new List<string>() : itemC.bcc_emails,
                                email_failure_count = (string)itemC.email_failure_count,
                                created_at = (itemC.created_at is null) ? new DateTime() : itemC.created_at,
                                updated_at = (itemC.updated_at is null) ? new DateTime() : itemC.updated_at,
                            };
                            await ConversationsRepository.Post(conversation, context);
                        }
                    }
                }
            }
            //return await ConversationsRepository.SelectAsync(context);
        }
    }
}
