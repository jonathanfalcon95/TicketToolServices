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
        public string email { get; set; }
        public string customerID { get; set; }
        public string phoneNumberRequester { get; set; }
        public string IDFacebookProfile { get; set; }
        public string agentInteractions { get; set; }
        public string customerInteraction { get; set; }
        public string resolutionStatus { get; set; }
        public string firstResponseStatus { get; set; }
        public string tags { get; set; }
        public string surveysResult { get; set; }
        public string agentID { get; set; }
        public string groupID { get; set; }
        public DateTime? creationDate { get; set; }
        public DateTime? expirationDate { get; set; }
        public DateTime? lastUpdateDate { get; set; }
        public DateTime? resolvedDate { get; set; } //stats
        public DateTime? closedDate { get; set; }
        public DateTime? fistResponseRequestDate { get; set; }
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
        public DateTime? estimatedStartDate { get; set; }
        public DateTime? estimatedEndDate { get; set; }
        public DateTime? realStartDate { get; set; }
        public DateTime? realEndDate { get; set; }
        public int? estimatedHourAgent { get; set; }
    }
}


