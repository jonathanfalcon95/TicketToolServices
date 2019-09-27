using System;
using System.Collections.Generic;
using System.Text;

namespace TicketToolServices.Models
{
    class Groups
    {
        public Groups()
        {
            SFAgentGroup = new List<SFAgentGroup>();
            SFAgentGroupDetail = new List<SFAgentGroupDetail>();
        }
        public string groupID { get; set; }
        public string groupDescription { get; set; }
        public List<SFAgentGroup> SFAgentGroup { get; }
        public List<SFAgentGroupDetail> SFAgentGroupDetail { get; }


        public class TaskCreateModelGroups
        {
            public string groupID { get; set; }
        }

        public class TaskCreateUpdateGroups
        {
            public string groupDescription { get; set; }
        }
    }
}
