using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vlapp.Models
{
    public class TimeListItem
    {
        public string ?from_time { get; set; }
        
        public string ?to_time { get; set; } 

        public int day { get; set; }

        public int schedule_id { get; set; }

    }
}
