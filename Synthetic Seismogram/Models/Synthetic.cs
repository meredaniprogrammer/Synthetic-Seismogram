namespace Synthetic_Seismogram.Models
{
    public class Synthetic
    {
        public int Id { get; set; }
        public string TWT { get; set; }
        public string Sy { get; set; }
        public int ReflectiveCoefficientId { get; set; }
        public ReflectiveCoefficient ReflectiveCoefficient { get; set;}
    }
}
