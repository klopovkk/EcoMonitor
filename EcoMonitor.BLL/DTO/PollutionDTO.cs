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
        public string DangerRate { get; set; }
    }
}
