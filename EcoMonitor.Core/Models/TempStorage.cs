using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoMonitor.Core.Models
{
    public class TempStorage : BaseEntity
    {
        public float N { get; set; }
        public float V { get; set; }
        public float T { get; set; }
        public float Tax { get; set; }
        public int FactoryId { get; set; }
        [NotMapped] public Factories Factory { get; set; }
    }
}
