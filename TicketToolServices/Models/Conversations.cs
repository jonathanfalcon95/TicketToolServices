using System;
using System.Collections.Generic;
using System.Text;

namespace TicketToolServices.Models
{
    public class Conversations
    {
        public string body_text { get; set; }
        public long? id { get; set; }
        public bool incoming { get; set; }
        public bool @private { get; set; }
        public string user_id { get; set; }
        public string support_email { get; set; }
        public int? source { get; set; }
        public int? category { get; set; }
        public int? ticket_id { get; set; }
        public List<string> to_emails { get; set; }
        public string from_email { get; set; }
        public List<string> cc_emails { get; set; }
        public List<string> bcc_emails { get; set; }
        public string email_failure_count { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
