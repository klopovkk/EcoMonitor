using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoMonitor.BLL.DTO
{
    public class CalculationDTO
    {
        public int Id { get; set; }
        public float GeneralEmission { get; set; }
        public string Date { get; set; }
        public int FactoryId { get; set; }
        public int PollutionId { get; set; }
        public bool? isAir { get; set; }
        public float? Tax { get; set; }
    }
}
