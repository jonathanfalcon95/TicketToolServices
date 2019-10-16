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
        public async void ConversationsUpdate(ExecutionContext context, string url,ILogger log,bool debuging)
        {
            try
            {
                var response = Conexion.GetDataApi(url);
                if (response.IsSuccessStatusCode)
                {
                    if (debuging)
                    {
                        log.LogInformation("Executed API " + url);
                    }
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
                            if (debuging)
                            {
                                log.LogInformation("Executed API  /tickets/" + tickets.TicketID + "/conversations");
                            }
                            var dataObjectsConversations = responseConversations.Content.ReadAsAsync<IEnumerable<dynamic>>().Result;
                            foreach (var itemC in dataObjectsConversations)
                            {
                                //Console.WriteLine(itemC.id);
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
                                if (debuging)
                                {
                                    log.LogInformation("Executing SP_Conversation " + conversation.id);
                                }
                                await ConversationsRepository.Post(conversation, context,log,false);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                log.LogInformation(e.Message);
            }
            //return await ConversationsRepository.SelectAsync(context);
        }
        public async void ConversationUpdate(ExecutionContext context, string url, ILogger log, bool debuging)
        {
            try
            {
                var response = Conexion.GetDataApi(url);
                if (response.IsSuccessStatusCode)
                {
                    if (debuging)
                    {
                        log.LogInformation("Executed API " + url);
                    }
                    var dataObjectsTickets = response.Content.ReadAsAsync<IEnumerable<dynamic>>().Result;
                    foreach (var item in dataObjectsTickets)
                    {
                        {
                            //Console.WriteLine(itemC.id);
                            JArray to_emails = (JArray)item["to_emails"];
                            JArray cc_emails = (JArray)item["to_emails"];
                            JArray bcc_emails = (JArray)item["to_emails"];
                            var conversation = new Conversations()
                            {
                                body_text = (string)item.body_text,
                                id = item.id,
                                incoming = item.incoming,
                                @private = item.@private,
                                user_id = (string)item.user_id,
                                support_email = (string)item.support_email,
                                source = item.source,
                                category = item.category,
                                ticket_id = item.ticket_id,
                                to_emails = to_emails.ToObject<List<string>>(),//(itemC.to_emails is null) ? new List<string>() : itemC.to_emails,
                                from_email = (string)item.from_email,
                                cc_emails = cc_emails.ToObject<List<string>>(),//(itemC.cc_emails is null) ? new List<string>() : itemC.cc_emails,
                                bcc_emails = bcc_emails.ToObject<List<string>>(), //(itemC.bcc_emails is null) ? new List<string>() : itemC.bcc_emails,
                                email_failure_count = (string)item.email_failure_count,
                                created_at = (item.created_at is null) ? new DateTime() : item.created_at,
                                updated_at = (item.updated_at is null) ? new DateTime() : item.updated_at,
                            };
                            if (debuging)
                            {
                                log.LogInformation("Executing SP_Conversation " + conversation.id);
                            }
                            await ConversationsRepository.Post(conversation, context, log, false);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                log.LogInformation(e.Message);
            }
            //return await ConversationsRepository.SelectAsync(context);
        }
    }
}
