using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using TicketToolServices.Repository;

namespace TicketToolServices.Controllers
{

    public static class DemoController 
    {


        [FunctionName("Todo_Get2")]
        public static async Task<ActionResult<object>> Get22([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "agents")] HttpRequest req, ILogger log, ExecutionContext context)
        {
            return await DemoRepository.SelectAsync(context);

        }


        [FunctionName("All_Groups")]
        public static async Task<ActionResult<object>> GetGroups([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "groups")] HttpRequest req, ILogger log, ExecutionContext context)
        {
            return await DemoRepository.GetAllGroups(context);
        }
           
        /*
        [FunctionName("Get_Groups_Id")]
        public static async Task<ActionResult<object>> GetGroupsId([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "groups/{id}")] HttpRequest req, ILogger log, ExecutionContext context)
        {
            return await DemoRepository.GetGroupId(context);
        }*/

    }

}
