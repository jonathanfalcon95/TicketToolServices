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
    public static class Function1
    {   
        //URL t
        private const string urlTickets = "https://tmconsulting.freshdesk.com/api/v2/tickets";
        //URL Tickets
        private const string urlTicketstats = "https://tmconsulting.freshdesk.com/api/v2/tickets";

        //Inicio Function
        [FunctionName("Function1")]
        public static void Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)

        {

            //InstanciaHttpClient de Tickets
            HttpClient clientTickets = new HttpClient();

            //AsignacionDeURL Tickets
            clientTickets.BaseAddress = new Uri(urlTickets);

            //Credenciales del API de FreshDesk
            string yourusername = "UNGq0cadwojfirXm6U7o";
            string yourpwd = "X";

            //Authorization al API


            clientTickets.DefaultRequestHeaders.Authorization =
              new System.Net.Http.Headers.AuthenticationHeaderValue(
                  "Basic", Convert.ToBase64String(
                      System.Text.ASCIIEncoding.ASCII.GetBytes(
                         $"{yourusername}:{yourpwd}")));



            clientTickets.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage responseTickets = clientTickets.GetAsync(urlTickets).Result;
            if (responseTickets.IsSuccessStatusCode)
            {
                var ticketID = responseTickets.Content.ReadAsAsync<IEnumerable<dynamic>>().Result;
                {
                    foreach (var item in ticketID)
                    {
                        var tickets = new Tickets()
                        {

                            TicketID = item.id

                        };
                        //InstanciaHttpClient de Ticketstats
                        HttpClient clientTicketstats = new HttpClient();

                        //AsignacionDeURL Ticketstats
                        clientTicketstats.BaseAddress = new Uri(urlTicketstats);

                        clientTicketstats.DefaultRequestHeaders.Authorization =
                                 new AuthenticationHeaderValue(
                                     "Basic", Convert.ToBase64String(
                                         System.Text.ASCIIEncoding.ASCII.GetBytes(
                                            $"{yourusername}:{yourpwd}")));

                        clientTicketstats.DefaultRequestHeaders.Accept.Add(
                               new MediaTypeWithQualityHeaderValue("application/json"));

                        HttpResponseMessage responseTicketstats = clientTicketstats.GetAsync(urlTicketstats + "/" + tickets.TicketID + "?include=stats").Result;

                        var dataObjects = responseTicketstats.Content.ReadAsAsync<IEnumerable<dynamic>>().Result;

                        foreach (var itemT in dataObjects)
                        {
                            var Ticketstat = new Tickets()
                            {

                                TicketID = itemT.id,
                                subject = itemT.subject,
                                description = itemT.description_text
                                /* status = item.status,
                                 priority = item.priority,
                                 source = item.source,
                                 type = item.type,
                                 companyID = item.company_id,
                                 customerID = item.requester_id,
                                 agentID = item.responder_id,
                                 groupID = item.group_id,
                                 creationDate = item.created_at,
                                 expirationDate = item.due_by,
                                 lastUpdateDate = item.updated_at,
                                 resolvedDate = item.custom_fields.cliente*/
                            };


                        }



                    }
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)responseTickets.StatusCode, responseTickets.ReasonPhrase);
            }
        }
    }
}

