﻿using System;
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
        private const string urlConversations = "https://tmconsulting.freshdesk.com/api/v2/tickets";
        //URL Tickets
        private const string urlTickets = "https://tmconsulting.freshdesk.com/api/v2/tickets";

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

            // List data response.
            HttpResponseMessage responseTickets = clientTickets.GetAsync(urlTickets).Result;
            if (responseTickets.IsSuccessStatusCode)
            {
                var dataObjects = responseTickets.Content.ReadAsAsync<IEnumerable<Tickets>>().Result;
                {
                    foreach (var item in dataObjects)
                    {
                        var tickets = new Tickets()
                        {

                            id = item.id

                        };


                        //TicketRepository.Tickets(Tickets);

                        Console.WriteLine(tickets.id);


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
