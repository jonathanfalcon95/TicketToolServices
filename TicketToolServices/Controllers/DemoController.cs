using Amazon.IdentityManagement.Model;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
//using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TicketToolServices.Models;
using TicketToolServices.Repository;

// ultimos
using System.Net;
//using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;

namespace TicketToolServices.Controllers
{

    public static class DemoController 
    {
        
        /*
        [FunctionName("Todo_Get2")]
        public static async Task<HttpResponseMessage> Get22([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "agents")] HttpRequest req, ILogger log, ExecutionContext context)
        {
            // return await DemoRepository.SelectAsync(context);

            var response = Conexion.GetDataApi("/agents");
           // var prueba = JsonConvert.SerializeObject(response);

            if (response.IsSuccessStatusCode)
            {

                // var dataObjects = response.Content.ReadAsAsync<IEnumerable<dynamic>>().Result;
                var dataObjects = response.Content.ReadAsAsync<IEnumerable<dynamic>>().Result;
                {
                    foreach (var item in dataObjects)
                    {
                        var agent = new Agent()
                        {

                            agentID = item.agentID,
                            agentDescription = item.agentDescription,
                        };

                        //JsonConvert.SerializeObject(agent);
                    }
                    

                }

            }

            return await Task<HttpResponseMessage>.Factory.StartNew(() =>
            {
                //return Request.CreateResponse(HttpStatusCode.OK, data);
                return response;
            });



            //return response;
            /*
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jsonToReturn, Encoding.UTF8, "application/json")
            };*/


            //return response.Content.ReadAsStringAsync().Result;

            //return JsonConvert.SerializeObject(response);

            //return await DemoRepository.SelectAsync(context);

            //return await response.Content.ReadAsStringAsync();
        //}
        
        


       

         // funciona desde la base de datos
         /*
        [FunctionName("Todo_GetById")]
        public static async Task<IActionResult> GetByIdGroups(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "groups/{groupID}")]
        HttpRequest req,
        ILogger log,
        ExecutionContext context,
        string groupID)
        {
            //var cnnString = GetConnectionString(log, context);
            var str = Conexion.GetConnectionString(context);
            using (var connection = new SqlConnection(str))
            {
                connection.Open();

                //var todos = await connection.QueryAsync<SFAgentGroupDetail>("select groupID, groupName, agentID, agentName, convert(date,date) as date,hours from dbo.SFAgentGroupDetail where groupID = @groupID", new { groupID });
                var todos = await connection.QueryAsync<SFAgentGroupDetail>("select * from dbo.SFAgentGroupDetail where groupID = @groupID", new { groupID });
                // if (todos.Count() == 0) return new NotFoundResult();
                return new OkObjectResult(todos);
            }
        }  
        */

        // otro intento

        [FunctionName("GetAgents")]
        public static async Task<HttpResponseMessage> GetAgents([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "agents")] HttpRequest req, ILogger log, ExecutionContext context)
        {
                using (var httpClient = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://tmconsulting.freshdesk.com/api/v2/agents"))
                    {
                        var base64authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes("UNGq0cadwojfirXm6U7o:X"));
                        request.Headers.TryAddWithoutValidation("Authorization", $"Basic {base64authorization}");

                        //request.Headers.GetValues("application/json");
                        
                        var result = await httpClient.SendAsync(request);

                        return result;
                    }
                }
            
            
        }

        // grupos
        [FunctionName("GetGroup")]
        public static async Task<HttpResponseMessage> GetGroup([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "groups")] HttpRequest req, ILogger log, ExecutionContext context)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://tmconsulting.freshdesk.com/api/v2/groups"))
                {
                    var base64authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes("UNGq0cadwojfirXm6U7o:X"));
                    request.Headers.TryAddWithoutValidation("Authorization", $"Basic {base64authorization}");

                    //request.Headers.GetValues("application/json");

                    var result = await httpClient.SendAsync(request);

                    return result;
                }
            }


        }


    }
}
