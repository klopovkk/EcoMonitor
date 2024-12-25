using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EcoMonitor.BLL.DTO;
using EcoMonitor.Core.Models;

namespace EcoMonitor.BLL.Services
{
    public class ConfigurationMapper : Profile
    {
        public ConfigurationMapper()
        {
            CreateMap<Factories, FactoryDTO>().ReverseMap();
            CreateMap<Pollutions, PollutionDTO>().ReverseMap();
            CreateMap<Calculations, CalculationDTO>().ReverseMap();
            CreateMap<Risk, RiskDTO>().ReverseMap();
        }
    }
}
