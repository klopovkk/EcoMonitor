using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoMonitor.Core.Models;

public class Calculations : BaseEntity
{
    [Required] public float GeneralEmission { get; set; }

    [Required] public string Date { get; set; }

    public int FactoryId { get; set; }
    public int PollutionId { get; set; }

    [NotMapped] public Pollutions Pollution { get; set; }

    [NotMapped] public Factories Factory { get; set; }
}