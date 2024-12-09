using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoMonitor.Core.Models
{
    public class Factories : BaseEntity
    {
        [Required] public string Name { get; set; }
        public string Address { get; set; }
        public string Head { get; set; }
        [NotMapped] public ICollection<Calculations> Calculations { get; set; }
    }
}
