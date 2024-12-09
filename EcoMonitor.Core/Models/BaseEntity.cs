﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoMonitor.Core.Models
{
    public abstract class BaseEntity
    {
        [Required]
        public int Id { get; set; }
    }
}
