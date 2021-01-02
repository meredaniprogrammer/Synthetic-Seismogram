    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Synthetic_Seismogram.Models
{
    public class ReflectiveCoefficient
    {
        public int Id { get; set; }
        public float Dt { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public List<AcousticImpedance> AcousticImpedances { get; set; }
        public List<Synthetic> Synthetics { get; set; }
    }
}
