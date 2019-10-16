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
    public class TicketsController
    {
        public async void TicketsUpdate(ExecutionContext context, string url, string urlParameters, ILogger log,bool debuging)
        {
            //var DateExecute = SFControlDataRepository.SelectAsync(context);
            string ParamLinkString = "";
            {
                try
                {
                    var response = Conexion.GetDataApi(url + urlParameters);
                    if (response.IsSuccessStatusCode)
                    {
                        if (debuging)
                        {
                            log.LogInformation("Executed API "+ url + urlParameters);
                        }
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
                                    if (item.custom_fields.cf_horas_estimadas_por_agente is null)
                                    {
                                        string a = "";
                                    }
                                    var tickets = new Tickets()
                                    {
                                        TicketID = (item.id is null ? 0 : Convert.ToInt32((int)item.id)),
                                        subject = item.subject,
                                        description = item.description_text,
                                        status = (item.status is null ? 0 : Convert.ToInt32((int)item.status)),
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
                                        estimatedHourAgent = item.custom_fields.cf_horas_estimadas_por_agente,
                                        resolvedDate = (item.stats.resolved_at is null ? new DateTime?() : item.stats.resolved_at),// (DateTime)Tic.stats.resolved_at);
                                        closedDate = (item.stats.closed_at is null ? new DateTime?() : item.stats.closed_at),// (DateTime)Tic.stats.closed_at);
                                        fistResponseRequestDate = (item.stats.first_responded_at is null ? new DateTime?() : item.stats.first_responded_at),// (DateTime)Tic.stats.first_responded_at) ;
                                    };
                                    tickets.totalBillingHours = Convert.ToString(Convert.ToDecimal(tickets.tmHoursWeek1) + Convert.ToDecimal(tickets.tmHoursWeek2) + Convert.ToDecimal(tickets.tmHoursWeek3) + Convert.ToDecimal(tickets.tmHoursWeek4));
                                    tickets.totalProgress = Convert.ToString(Convert.ToDecimal(tickets.progressWeek1) + Convert.ToDecimal(tickets.progressWeek2) + Convert.ToDecimal(tickets.progressWeek3) + Convert.ToDecimal(tickets.progressWeek4));
                                    //Console.WriteLine(tickets.TicketID);
                                    if (debuging)
                                    {
                                        log.LogInformation("Executing SP_tickets " + tickets.TicketID);
                                    }
                                    await TicketRepository.Post(tickets, context,log,debuging);
                                }
                            }
                            if (!(link is null))
                            {
                                if (debuging)
                                {
                                    log.LogInformation("Executing " +ParamLinkString);
                                }
                                response = Conexion.GetDataApi("/tickets", ParamLinkString);
                            }
                        } while (!(link is null));
                    }
                }
                catch (Exception e)
                {
                    if (debuging)
                    {
                        log.LogError(e.Message);
                    }
                }
            }
        }
        public async void TicketUpdate(ExecutionContext context, string url, string urlParameters, ILogger log, bool debuging)
        {
            try
            {
                //var DateExecute = SFControlDataRepository.SelectAsync(context);
                var response = Conexion.GetDataApi(url + urlParameters);
                if (response.IsSuccessStatusCode)
                {
                    if (debuging)
                    {
                        log.LogInformation("Executed API " + url + urlParameters);
                    }
                    var ticket = response.Content.ReadAsAsync<dynamic>().Result;
                    /*foreach (var item in ticketID)
                    {*/
                    if (ticket.custom_fields.cf_horas_estimadas_por_agente is null)
                    {
                        string a = "";
                    }
                    var tickets = new Tickets()
                    {
                        TicketID = (ticket.id is null ? 0 : Convert.ToInt32((int)ticket.id)),
                        subject = ticket.subject,
                        description = ticket.description_text,
                        status = (ticket.status is null ? 0 : Convert.ToInt32((int)ticket.status)),
                        priority = ticket.priority,
                        source = ticket.source,
                        type = ticket.type,
                        companyID = ticket.company_id,
                        customerID = ticket.requester_id,
                        agentID = ticket.responder_id,
                        groupID = ticket.group_id,
                        creationDate = ticket.custom_fields.created_at,
                        expirationDate = ticket.due_by,
                        lastUpdateDate = ticket.custom_fields.updated_at,
                        customerCompany = ticket.custom_fields.cliente,
                        projectNumber = ticket.custom_fields.proyecto,
                        quotationID = ticket.custom_fields.id_cotizador,
                        sharePointID = ticket.custom_fields.sharepoint_id,
                        customerEstimatedHours = ticket.custom_fields.horas_estimadas_por_cliente,
                        tmHoursWeek1 = ticket.custom_fields.horas_tampm_semana_1,
                        tmHoursWeek2 = ticket.custom_fields.horas_tampm_semana_2,
                        tmHoursWeek3 = ticket.custom_fields.horas_tampm_semana_3,
                        tmHoursWeek4 = ticket.custom_fields.horas_tampm_semana_4,
                        progressWeek1 = ticket.custom_fields.porcentaje_de_avance,
                        progressWeek2 = ticket.custom_fields.avance_semana_2,
                        progressWeek3 = ticket.custom_fields.avance_semana_3,
                        progressWeek4 = ticket.custom_fields.avance_semana_4,
                        billingMonth = ticket.custom_fields.mes_facturacin,
                        totalBillingHours = ticket.custom_fields.horas_tampm_semana_1,
                        totalProgress = ticket.custom_fields.porcentaje_de_avance,
                        estimatedStartDate = ticket.custom_fields.cf_fecha_de_estimada_inicio,
                        estimatedEndDate = ticket.custom_fields.cf_fecha_de_estimada_entrega,
                        realStartDate = ticket.custom_fields.cf_fecha_de_real_inicio,
                        realEndDate = ticket.custom_fields.cf_fecha_de_real_entrega,
                        estimatedHourAgent = ticket.custom_fields.cf_horas_estimadas_por_agente,
                        resolvedDate = (ticket.stats.resolved_at is null ? new DateTime?() : ticket.stats.resolved_at),// (DateTime)Tic.stats.resolved_at);
                        closedDate = (ticket.stats.closed_at is null ? new DateTime?() : ticket.stats.closed_at),// (DateTime)Tic.stats.closed_at);
                        fistResponseRequestDate = (ticket.stats.first_responded_at is null ? new DateTime?() : ticket.stats.first_responded_at),// (DateTime)Tic.stats.first_responded_at) ;
                    };
                    tickets.totalBillingHours = Convert.ToString(Convert.ToDecimal(ticket.tmHoursWeek1) + Convert.ToDecimal(ticket.tmHoursWeek2) + Convert.ToDecimal(ticket.tmHoursWeek3) + Convert.ToDecimal(ticket.tmHoursWeek4));
                    tickets.totalProgress = Convert.ToString(Convert.ToDecimal(ticket.progressWeek1) + Convert.ToDecimal(ticket.progressWeek2) + Convert.ToDecimal(ticket.progressWeek3) + Convert.ToDecimal(ticket.progressWeek4));
                    //Console.WriteLine(tickets.TicketID);
                    if (debuging)
                    {
                        log.LogInformation("Executing SP_tickets " + tickets.TicketID);
                        //log.LogInformation("Executing SP_tickets " + ticket.TicketID);
                    }
                    await TicketRepository.Post(tickets, context, log, debuging);
                }
            }
            catch (Exception e)
            {
                if (debuging)
                {
                    log.LogError(e.Message);
                }
            
            }
        }
    }
}
    
//}