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
using TicketToolServices.Repository;
using TicketToolServices.Models;

namespace TicketToolServices.Controllers
{
    public static class Ticketscontroller
    {

        [FunctionName("functionTicket")]
        public static async Task<ActionResult<object>> functionTicket([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "tickets")] HttpRequest req, ILogger log, ExecutionContext context)
        
{
            var response = Conexion.GetDataApi("/tickets?include=requester");

            var response2 = Conexion.GetDataApi("/tickets?include=description");

            if (response.IsSuccessStatusCode)
            {
                var tickets_requester = response.Content.ReadAsAsync<IEnumerable<dynamic>>().Result;
                {
                    foreach (var item in tickets_requester)
                    {
                        var ticketsreq = new Tickets()
                        {

                            TicketID = item.priority

                        };

                        Console.WriteLine(ticketsreq.TicketID);
                        var tickets_description = response2.Content.ReadAsAsync<IEnumerable<dynamic>>().Result;
                       
                        {
                            foreach (var itemT in tickets_description)
                            {   

                                var ticketsdes = new Tickets()
                                {
                                   
                                    description = itemT.description_text

                                };

                                Console.WriteLine(ticketsdes.description);

                                await TicketRepository.Post(ticketsreq, ticketsdes, context);

                            }
                        }
                    
                
            }


            return await TicketRepository.SelectAsync(context);

        }

    }
}

