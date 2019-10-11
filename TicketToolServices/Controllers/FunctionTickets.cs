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
using Newtonsoft.Json.Linq;

namespace TicketToolServices.Controllers
{
    public class FunctionTickets
    {
        //Inicio Function
       // [FunctionName("Function1")]
       // public static async Task<ActionResult<object>> TicketsFunction([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "tickets")] HttpRequest req, ILogger log, ExecutionContext context)

             public async void TicketsFunction(ExecutionContext context, string url)
             {
                var DateExecute = await SFControlDataRepository.SelectAsync(context);

            string ParamLinkString="";


                {
                    foreach (var item2 in DateExecute)

                    {
                        var tickets2 = new SFControlData()
                        {
                            lastExecutedDate = item2.lastExecutedDate
                      
                    };
                        Console.WriteLine(tickets2.lastExecutedDate);


                          var response = Conexion.GetDataApi("/tickets?updated_since=" + tickets2.lastExecutedDate + "&include=stats,description&per_page=100&page=1");
                    if (response.IsSuccessStatusCode)
                    {
                        IEnumerable<string> link = new string[] { };
                        do
                        {
                                 if (response.Headers.TryGetValues("link", out link))
                                 {
                                    foreach (var b in link)
                                    {
                                        ParamLinkString = b.Substring(b.IndexOf("?"), (b.IndexOf(">") - b.IndexOf("?")));
                                    }
                                 }
                                 var ticketID = response.Content.ReadAsAsync<IEnumerable<dynamic>>().Result;
                                 {
                                     foreach (var item in ticketID)
                                     {

                                         var tickets = new Tickets()
                                         {
                                            TicketID = item.id,
                                            subject = item.subject,
                                            description = item.description_text,
                                            status = (item.status is null ? 0 : (int)item.status),
                                            priority = item.priority,
                                            source = item.source,
                                            type = item.type,
                                            companyID = item.company_id,
                                            customerID = item.requester_id,
                                            agentID = item.responder_id,
                                            groupID = item.group_id,
                                            creationDate = item.custom_fields.created_at,
                                            expirationDate = item.due_by,
                                            lastUpdateDate = item.custom_fields.updated_at,
                                            customerCompany = item.custom_fields.cliente,
                                            projectNumber = item.custom_fields.proyecto,
                                            quotationID = item.custom_fields.id_cotizador,
                                            sharePointID = item.custom_fields.sharepoint_id,
                                            customerEstimatedHours = item.custom_fields.horas_estimadas_por_cliente,
                                            tmHoursWeek1 = item.custom_fields.horas_tampm_semana_1,
                                            tmHoursWeek2 = item.custom_fields.horas_tampm_semana_2,
                                            tmHoursWeek3 = item.custom_fields.horas_tampm_semana_3,
                                            tmHoursWeek4 = item.custom_fields.horas_tampm_semana_4,
                                            progressWeek1 = item.custom_fields.porcentaje_de_avance,
                                            progressWeek2 = item.custom_fields.avance_semana_2,
                                            progressWeek3 = item.custom_fields.avance_semana_3,
                                            progressWeek4 = item.custom_fields.avance_semana_4,
                                            billingMonth = item.custom_fields.mes_facturacin,
                                            totalBillingHours = item.custom_fields.horas_tampm_semana_1,
                                            totalProgress = item.custom_fields.porcentaje_de_avance,
                                            estimatedStartDate = item.custom_fields.cf_fecha_de_estimada_inicio,
                                            estimatedEndDate = item.custom_fields.cf_fecha_de_estimada_entrega,
                                            realStartDate = item.custom_fields.cf_fecha_de_real_inicio,
                                            realEndDate = item.custom_fields.cf_fecha_de_real_entrega,
                                            estimatedHourAgent = (item.custom_fields.cf_horas_estimadas_por_agente is null ? 0 : (int)item.custom_fields.cf_horas_estimadas_por_agente),
                                            resolvedDate = (item.stats.resolved_at is null ? new DateTime?() : item.stats.resolved_at),// (DateTime)Tic.stats.resolved_at);
                                            closedDate = (item.stats.closed_at is null ? new DateTime?() : item.stats.closed_at),// (DateTime)Tic.stats.closed_at);
                                            fistResponseRequestDate = (item.stats.first_responded_at is null ? new DateTime?() : item.stats.first_responded_at),// (DateTime)Tic.stats.first_responded_at) ;
                                         };

                                             tickets.totalBillingHours = tickets.tmHoursWeek1 + tickets.tmHoursWeek2 + tickets.tmHoursWeek3 + tickets.tmHoursWeek4;
                                              tickets.totalProgress = tickets.progressWeek1 + tickets.progressWeek2 + tickets.progressWeek3 + tickets.progressWeek4;

                                             Console.WriteLine(tickets.TicketID);

                                         await TicketRepository.Post(tickets, context);

                                     }
                                 }
                                    if (!(link is null))
                                    {
                                        response = Conexion.GetDataApi("/tickets", ParamLinkString);
                                    }
                                } while (!(link is null));
                        }
                    }
                }
                 // return await SFControlDataRepository.SelectAsync(context);
             }
    }
}