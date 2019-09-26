using System;
using System.Collections.Generic;
using System.Text;

namespace TicketToolServices.Models
{
    class Ticket
    {
        public int ticketID { get; set; }
        public string customerID { get; set; }
        public string subject { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public string priority { get; set; }
        public string source { get; set; }
        public string type { get; set; }
        public string email { get; set; }
        public string phoneNumberRequester { get; set; }
        public string IDFacebookProfile { get; set; }
        public string agentID { get; set; }
        public string groupID { get; set; }
        public DateTime creationDate { get; set; }
        public DateTime expirationDate { get; set; }
        public DateTime resolvedDate { get; set; }
        public DateTime closedDate { get; set; }
        public DateTime lastUpdateDate { get; set; }
        public DateTime fistResponseRequestDate { get; set; }
        public int agentInteractions { get; set; }
        public int customerIntearction { get; set; }
        public string resolutionStatus { get; set; }
        public string firstResponseStatus { get; set; }
        public string tags { get; set; }
        public string surveysResult { get; set; }
        public string companyID { get; set; }
        public string customerCompany { get; set; }
        public string projectNumber { get; set; }
        public string sharePointID { get; set; }
        public string quotationID { get; set; }
        public float customerEstimatedHours { get; set; }
        public float tmHoursWeek1 { get; set; }
        public float tmHoursWeek2 { get; set; }
        public float tmHoursWeek3 { get; set; }
        public float tmHoursWeek4 { get; set; }
        public float progressWeek1 { get; set; }
        public float progressWeek2 { get; set; }
        public float progressWeek3 { get; set; }
        public float progressWeek4 { get; set; }
        public DateTime billingMonth { get; set; }
        public float totalBillingHours { get; set; }
        public float totalProgress { get; set; }
        public DateTime estimatedStartDate { get; set; }
        public DateTime estimatedEndDate { get; set; }
        public DateTime realStartDate { get; set; }
        public DateTime realEndDate { get; set; }
        public float estimatedHourAgent {get; set;}
}
}
