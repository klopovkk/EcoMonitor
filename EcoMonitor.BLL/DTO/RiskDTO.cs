using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoMonitor.BLL.DTO
{
    public class RiskDTO
    {
        public int Id { get; set; }
        public int FactoryId { get; set; }
        public int PollutionId { get; set; }
        public float? Conc { get; set; }
        public float? Rfc { get; set; }
        public float? Hq { get; set; }
        public float? Ladd { get; set; }
        public float? Sf { get; set; }
        public float? Cr { get; set; }
        public float? Date { get; set; }
        public string FactoryName { get; set; }
        public string PollutionName { get; set; }

    }
}
