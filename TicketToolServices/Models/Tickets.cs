using System;
using System.Collections.Generic;
using System.Text;

namespace TicketToolServices.Models
{
    class Tickets
    {
        public int TicketID { get; set; }
        public string subject { get; set; }
        public string description { get; set; }//descript
        public int status { get; set; }
        public string priority { get; set; }
        public string source { get; set; }
        public string type { get; set; }
        public string companyID { get; set; }
        public string customerID { get; set; }
        public string agentID { get; set; }
        public string groupID { get; set; }
        public string creationDate { get; set; }
        public string expirationDate { get; set; }
        public string lastUpdateDate { get; set; }
        public string resolvedDate { get; set; } //stats
        public string closedDate { get; set; }
        public string fistResponseRequestDate { get; set; }
        public string customerCompany { get; set; }
        public string projectNumber { get; set; }
        public string quotationID { get; set; }
        public string sharePointID { get; set; }
        public string customerEstimatedHours { get; set; }
        public string tmHoursWeek1 { get; set; }
        public string tmHoursWeek2 { get; set; }
        public string tmHoursWeek3 { get; set; }
        public string tmHoursWeek4 { get; set; }
        public string progressWeek1 { get; set; }
        public string progressWeek2 { get; set; }
        public string progressWeek3 { get; set; }
        public string progressWeek4 { get; set; }
        public string billingMonth { get; set; }
        public string totalBillingHours { get; set; }
        public string totalProgress { get; set; }
        public string estimatedStartDate { get; set; }
        public string estimatedEndDate { get; set; }
        public string realStartDate { get; set; }
        public string realEndDate { get; set; }
        public string estimatedHourAgent { get; set; }
    }

}

