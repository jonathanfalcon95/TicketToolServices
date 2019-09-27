using System;
using System.Collections.Generic;
using System.Text;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;

namespace TicketToolServices.Models
{
    class SFAgentGroupDetail
    {
        public string groupID { get; set; }
        public string groupName { get; set; }
        public string agentID { get; set; }
        public string agentName { get; set; }
        public Date date { get; set; }
        public int hours { get; set; }
    }
}
