using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoMonitor.BLL.DTO
{
    public class PollutionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float FlowRate { get; set; }
        public float EmissionStandart { get; set; }
        public bool isAirPollution { get; set; }
        public float? TaxRateAw { get; set; }
        public float? TaxRateP { get; set; }
        public string DangerRate { get; set; }
    }
}
