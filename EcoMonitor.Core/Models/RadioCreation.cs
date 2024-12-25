using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoMonitor.Core.Models
{
    public class RadioCreation : BaseEntity
    {
        public int FactoryId { get; set; }
        public float? OnElectricity { get; set; }
        public float? C1ns { get; set; }
        public float? C1v { get; set; }
        public float? C2ns { get; set; }
        public float? C2v { get; set; }
        public float? V1ns { get; set; }
        public float? V1v { get; set; }
        public float? V2ns { get; set; }
        public float? V2v { get; set; }
        public float? Tax { get; set; }
        [NotMapped] public Factories Factory { get; set; }
    }
}
