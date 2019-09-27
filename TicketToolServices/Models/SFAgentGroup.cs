using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace TicketToolServices.Models
{
    class SFAgentGroup
    {
        public string groupID { get; set; }
        public Groups Groups { get; set; }
        public string groupName { get; set; }

        public string agentID { get; set; }
        public Agent Agent { get; set; }
        public string agentName { get; set; }

        public Date date { get; set; }
        public int totalHours { get; set; }
        public int totalDays { get; set; }

    }
}
