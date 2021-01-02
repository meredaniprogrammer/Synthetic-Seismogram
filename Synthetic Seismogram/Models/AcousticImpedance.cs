using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Synthetic_Seismogram.Models
{
    public class AcousticImpedance
    {
        public int Id { get; set; }
        public float TWT { get; set; }
        public float AI { get; set; }
        public int ReflectiveCoefficientId { get; set; }
        public ReflectiveCoefficient ReflectiveCoefficient { get; set; }
    }
}
