using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoMonitor.Core.Models;

public class Pollutions : BaseEntity
{
    [Required] public string Name { get; set; }
    public float FlowRate { get; set; }
    public float EmissionStandart { get; set; }
    public string DangerRate { get; set; }
    public bool isAirPollution { get; set; }
    public float? TaxRateAw { get; set; }
    public float? TaxRateP { get; set; }
    [NotMapped] public ICollection<Calculations> Calculations { get; set; }
    [NotMapped] public ICollection<Risk> Risks { get; set; }

}