using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using TicketToolServices.Controllers;

namespace TicketToolServices
{
    public static class Function1
    {
  
        [FunctionName("Function")]
        public static void FunctionC([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log, ExecutionContext context)
        {
            log.LogInformation($"C# Timer trigger function2 executed at: {DateTime.Now}");
            FunctionTickets Tickets = new FunctionTickets();
            Tickets.TicketsFunction(context, "/tickets");
        }
    }
}
