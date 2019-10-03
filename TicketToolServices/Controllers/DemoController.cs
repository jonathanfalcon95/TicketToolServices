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
using TicketToolServices.Models;
using TicketToolServices.Repository;


namespace TicketToolServices.Controllers
{

    public static class DemoController 
    {
        static readonly List<SFAgentGroupDetail> items = new List<SFAgentGroupDetail>();
                
        //private static readonly object groupdetail;
                
        [FunctionName("Todo_Get2")]
        public static async Task<ActionResult<object>> Get22([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "agents")] HttpRequest req, ILogger log, ExecutionContext context)
        {
            // return await DemoRepository.SelectAsync(context);

            var response = Conexion.GetDataApi("/agents");

            if (response.IsSuccessStatusCode)
            {

                var dataObjects = response.Content.ReadAsAsync<IEnumerable<dynamic>>().Result;
                {
                    foreach (var item in dataObjects)
                    {
                        var agent = new Agent()
                        {

                            agentID = item.agentID,

                        };
                    }

                }

            }

            return await DemoRepository.SelectAsync(context);
        }    

        
        [FunctionName("All_Groups")]
        public static async Task<ActionResult<object>> GetGroups([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "groups")] HttpRequest req, ILogger log, ExecutionContext context)
        {
            var response = Conexion.GetDataApi("/groups");

            if (response.IsSuccessStatusCode)
            {
                var dataObjects = response.Content.ReadAsAsync<IEnumerable<dynamic>>().Result;
                {
                    foreach (var item in dataObjects)
                    {
                        var groups = new Groups()
                        {
                            groupID = item.groupID,
                        };
                    }

                }

            }
            return await DemoRepository.GetAllGroups(context);
        }
        

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
               

                var todos = await connection.QueryAsync<SFAgentGroupDetail>("select * from dbo.SFAgentGroupDetail where groupID = @groupID", new { groupID });

                // if (todos.Count() == 0) return new NotFoundResult();
                return new OkObjectResult(todos);
            }
        }

    }

}
