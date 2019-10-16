using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TicketToolServices.Models;
using TicketToolServices.Repository;
using TicketToolServices.Controllers;

namespace TicketToolServices
{
    public static class HttpUpdateTickets
    {
        [FunctionName("HttpUpdateTickets")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "{tickets?}/{TicketID?}/{conversations?}")] HttpRequest req,
            ILogger log,string tickets,string TicketID,string conversations, ExecutionContext context)
        {
            int TicketIDint = 0;
            bool LogingBool = false;
            string Loging = req.Query["log"];
            if (Loging is null)
            {
            }
            else
            {
                if ((String.Compare(Loging.ToLower(), "off") == 0 || String.Compare(Loging.ToLower(), "debug") == 0))
                {
                    LogingBool = ((String.Compare(Loging.ToLower(), "off") == 0) ? false : true);
                }
                else
                {
                    return (ActionResult)new BadRequestObjectResult($"Ha sucedido un error al ejecutar - log {Loging} solo permite valores 'off' y 'debug'");
                }
            }
            try
            {
                if (!String.IsNullOrEmpty(tickets))
                {
                    if (string.Compare(tickets.ToLower(), "tickets") == 0)
                    {
                        if (!String.IsNullOrEmpty(TicketID))
                        {
                            if (Int32.TryParse(TicketID, out TicketIDint))
                            {
                                if (!String.IsNullOrEmpty(conversations))
                                {
                                    if (string.Compare(conversations.ToLower(), "conversations") == 0)
                                    {
                                        
                                        ConversationsController Conversation = new ConversationsController();
                                        Conversation.ConversationUpdate(context, "/tickets/" + TicketIDint + "/conversations", log, LogingBool);
                                        
                                        log.LogInformation($"C# HTTP trigger function processed - {tickets} - {TicketIDint} - {conversations} - {Loging} ");
                                        return (ActionResult)new OkObjectResult($"Se ha ejecutado {tickets} - {TicketIDint} - {conversations} - {Loging} ");
                                    }
                                    else
                                    {
                                        log.LogInformation("C# HTTP trigger function processed a request with errors.");
                                        return (ActionResult)new BadRequestObjectResult($"No se puede ejecutar el proceso sobre  Conversations {conversations} ");
                                    }
                                }
                                else
                                {
                                    
                                    TicketsController Tickets = new TicketsController();
                                    Tickets.TicketUpdate(context, "/tickets/" + TicketIDint + "&?include=stats", "", log, LogingBool);
                                    
                                    log.LogInformation($"C# HTTP trigger function processed -  {tickets} - {TicketIDint} - {Loging} ");
                                    return (ActionResult)new OkObjectResult($"Se ha ejecutado {tickets} - {TicketIDint} - {Loging} ");
                                }
                            }
                            else
                            {
                                log.LogInformation("C# HTTP trigger function processed a request with errors.");
                                return (ActionResult)new BadRequestObjectResult($"No se puede ejecutar el proceso sobre  TicketID {TicketID}");
                            }
                        }
                        else
                        {
                            
                            SFControlData DateExecute = new SFControlData();
                            DateExecute.lastExecutedDate = SFControlDataRepository.SelectAsync(context);
                            
                            TicketsController Tickets = new TicketsController();
                            Tickets.TicketsUpdate(context, "/tickets?updated_since=" + DateExecute.lastExecutedDate, "&include=stats,description&per_page=100&page=1", log, LogingBool);
                            
                            log.LogInformation($"C# HTTP trigger function processed -  {tickets} - {Loging} ");
                            return (ActionResult)new OkObjectResult($"Se ha ejecutado {tickets} - {Loging} ");

                        }
                    }
                    else
                    {
                        log.LogInformation("C# HTTP trigger function processed a request with errors.");
                        return (ActionResult)new BadRequestObjectResult($"No se puede ejecutar el proceso sobre  Tickets {tickets}");
                    }
                }
                else
                {
                    SFControlData DateExecute = new SFControlData();
                    DateExecute.lastExecutedDate = SFControlDataRepository.SelectAsync(context);

                    TicketsController Tickets = new TicketsController();
                    Tickets.TicketsUpdate(context, "/tickets?updated_since=" + DateExecute.lastExecutedDate, "&include=stats,description&per_page=100&page=1", log, LogingBool);

                    ConversationsController Conversation = new ConversationsController();
                    Conversation.ConversationsUpdate(context, "/tickets", log, LogingBool);

                    log.LogInformation($"C# HTTP trigger function processed a request without param - log {Loging}");
                    return (ActionResult)new OkObjectResult($"Se ha ejecutado  Tickets - ticketID - Conversations - Log {Loging}");
                }
            }
            catch (Exception e)
            {
                return (ActionResult)new BadRequestObjectResult($"Ha sucedido un error al ejecutar - Tickets {tickets} - ticketID {TicketID} - Conversations {conversations} - log {Loging}  " + e.Message);
            }
        }
    }
}
