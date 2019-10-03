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

namespace TicketToolServices.Controllers
{
    public static class FunctionTickets
    {   
     
        //Inicio Function
        [FunctionName("Function1")]
        public static void Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)

        {
            var response = Conexion.GetDataApi("/tickets?include=requester?tickets?include=description/");
            if (response.IsSuccessStatusCode)
            {
                var ticketID = response.Content.ReadAsAsync<IEnumerable<dynamic>>().Result;
                {
                    foreach (var item in ticketID)
                    {
                        var tickets = new Tickets()
                        {

                            TicketID = item.id,
                            subject = item.subject,
                            status = item.status,
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
                            //totalBillingHours = Convert.ToString(item.custom_fields.horas_tampm_semana_1 + item.custom_fields.horas_tampm_semana_2 + item.custom_fields.horas_tampm_semana_3 + item.custom_fields.horas_tampm_semana_4),
                            //totalProgress = Convert.ToString(item.custom_fields.porcentaje_de_avance + item.custom_fields.avance_semana_2 + item.custom_fields.avance_semana_3 + item.custom_fields.avance_semana_4),
                            estimatedStartDate = item.custom_fields.cf_fecha_de_estimada_inicio,
                            estimatedEndDate = item.custom_fields.cf_fecha_de_estimada_entrega,
                            realStartDate = item.custom_fields.cf_fecha_de_real_inicio,
                            realEndDate = item.custom_fields.cf_fecha_de_real_entrega,
                            description = item.description_text,
                            estimatedHourAgent = Convert.ToString(item.custom_fields.cf_horas_estimadas_por_agente)
                        };
                        tickets.totalBillingHours = Convert.ToString(tickets.tmHoursWeek1 + tickets.tmHoursWeek2 + tickets.tmHoursWeek3 + tickets.tmHoursWeek4);
                        tickets.totalProgress = Convert.ToString(tickets.progressWeek1 + tickets.progressWeek2 + tickets.progressWeek3 + tickets.progressWeek4);

                        Console.WriteLine(tickets.billingMonth);
                    }
                }
            }
            //Data that is only in the description url
            var response2 = Conexion.GetDataApi("/tickets?include=description");
            if (response2.IsSuccessStatusCode)
            {
                var ticketID = response2.Content.ReadAsAsync<IEnumerable<dynamic>>().Result;
                {
                    foreach (var item in ticketID)
                    {
                        var tickets = new Tickets()
                        {
                            description = item.description_text
                        };

                        //Console.WriteLine(tickets.description);

                    }
                }
            }
            //Data that is only in the description url
            var response3 = Conexion.GetDataApi("/tickets?include=stats");
            if (response3.IsSuccessStatusCode)
            {
                var ticketID = response3.Content.ReadAsAsync<IEnumerable<dynamic>>().Result;
                {
                    foreach (var item in ticketID)
                    {
                        var tickets = new Tickets()
                        {
                            resolvedDate = item.stats.resolved_at,
                            closedDate = item.stats.closed_at,
                            fistResponseRequestDate = item.stats.first_responded_at
                        };

                        //Console.WriteLine(tickets.resolvedDate);

                    }
                }
            }
        }
    }
}

