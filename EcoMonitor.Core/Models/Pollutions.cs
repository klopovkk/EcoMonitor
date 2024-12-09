using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoMonitor.Core.Models;

public class Pollutions : BaseEntity
{
    [Required] public string Name { get; set; }
    public float FlowRate { get; set; }
    public float EmissionStandart { get; set; }
    public string DangerRate { get; set; }
    [NotMapped] public ICollection<Calculations> Calculations { get; set; }
}