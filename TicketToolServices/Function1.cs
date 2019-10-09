using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using TicketToolServices.Controllers;

namespace TicketToolServices
{
    public static class FunctionTimmer
    {
        [FunctionName("Function1")]
        public static void Function1([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
        [FunctionName("FunctionC")]
        public static void FunctionC([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log, ExecutionContext context)
        {
            log.LogInformation($"C# Timer trigger function2 executed at: {DateTime.Now}");
            ConversationsController Conversation = new ConversationsController();
            Conversation.ConversationsUpdate(context, "/tickets");
        }
    }
}
