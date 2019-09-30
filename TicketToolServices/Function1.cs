<<<<<<< HEAD
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TicketToolServices.Models;
using static TicketToolServices.Models.Agent;

using System.Linq;
=======
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
>>>>>>> 4849b6bd690cc9ed194eca44e7a96b950e8b90af

namespace TicketToolServices
{
    public static class Function1
    {

        [FunctionName("Function1")]
        public static void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
