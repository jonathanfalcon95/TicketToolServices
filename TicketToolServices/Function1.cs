using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using TicketToolServices.Controllers;
using TicketToolServices.Models;
using TicketToolServices.Repository;


namespace TicketToolServices
{
    public static class FunctionTimmer
    {
        [FunctionName("Function")]
        public static void FunctionC([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log, ExecutionContext context)
        {
            try
            {
                SFControlData DateExecute = new SFControlData();
                DateExecute.lastExecutedDate = SFControlDataRepository.SelectAsync(context);

                TicketsController Tickets = new TicketsController();
                Tickets.TicketsUpdate(context, "/tickets?updated_since=" + DateExecute.lastExecutedDate, "&include=stats,description&per_page=100&page=1", log, false);

                ConversationsController Conversation = new ConversationsController();
                Conversation.ConversationsUpdate(context, "/tickets",log,false);
                log.LogInformation($"C# Timer trigger functionC executed at: {DateTime.Now}");
            }
            catch(Exception e)
            {
                log.LogError(e.Message);
            }
        }
       
    }

}
