using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoMonitor.Core.Models
{
    public class Risk : BaseEntity
    {
        public int FactoryId { get; set; }
        public int PollutionId { get; set; }

        public float? OnElectricity { get; set; }
        public float? Conc { get; set; }
        public float? Rfc { get; set; }
        public float? Hq { get; set; }
        public float? Ladd { get; set; }
        public float? Sf { get; set; }
        public float? Cr { get; set; }
        public float? Date { get; set; }
        [NotMapped] public Factories Factory { get; set; }
        [NotMapped] public Pollutions Pollution { get; set; }
    }
}
