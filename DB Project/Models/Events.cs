using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_Project.Models
{
    public class Events
    {
        public string Event_ID { get; set; }
        public string Event_Title { get; set; }
        public DateTime Event_Date { get; set; }
        public string Event_Description { get; set; }
        public string Event_Category { get; set; }
        public string Class_Code { get; set; }
    }
}
