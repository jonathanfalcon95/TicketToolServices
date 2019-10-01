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

namespace TicketToolServices.Controllers
{
    public class Todo
    {
       
        public int Id { get; set; }
        public string Title { get; set; }
       
    }
    public static class DemoController 
    {


        [FunctionName("Todo_Get2")]
        public static async Task<ActionResult<object>> Get22([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "agents")] HttpRequest req, ILogger log, ExecutionContext context)
        {
            var response = Conexion.GetDataApi("/agents");

            if (response.IsSuccessStatusCode)
            {
                log.LogInformation("paso algo=================================================");
                var dataObjects = response.Content.ReadAsAsync<IEnumerable<dynamic>>().Result;
                {
                    foreach (var item in dataObjects)
                    {
                        string algo = item.id;
                        log.LogInformation(algo);
                        //var todo = new Todo()
                        //{

                        //    Id = item.id,
                        //    Title = item.contact.email

                        //};
                       // log.LogInformation("paso algo" + todo.ToString());
                    }
                    
                }



                    }

               
            return await DemoRepository.SelectAsync(context);

        }

    }



}
