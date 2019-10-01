using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using TicketToolServices.Models;
using TicketToolServices.Repository;

namespace TicketToolServices.Controllers
{
    public static class FunctionTickets
    {   
     
        //Inicio Function
        [FunctionName("Function1")]
        public static void Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)

        {

            var response = Conexion.GetDataApi("/tickets");


            if (response.IsSuccessStatusCode)
            {
                var ticketID = response.Content.ReadAsAsync<IEnumerable<dynamic>>().Result;
                {
                    foreach (var item in ticketID)
                    {
                        var tickets = new Tickets()
                        {

                            TicketID = item.id

                        };

                        Console.WriteLine(tickets.TicketID);
                        var response2 = Conexion.GetDataApi("/tickets" + "/" + tickets.TicketID + "?include=stats");

                        var Data = response2.Content.ReadAsAsync<IEnumerable<dynamic>>().Result;
                        {
                            foreach (var itemT in Data)
                            {
                                var Datatickets = new Tickets()
                                {

                                    description = itemT.description_text

                                };
                                Console.WriteLine(Datatickets.description);

                            }
                        }
                    }
                }
            }
        }
    }
}

