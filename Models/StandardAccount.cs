using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImportCSV.Models
{
    public class StandardAccount
    {
        public int Id { get; set; }
        public string AccountCode { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime? OpenDate { get; set; }
        public string Currency { get; set; }
    }
}
