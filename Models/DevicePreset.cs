namespace NetworkBandwidthAlgorithm.Models
{
    public class DevicePreset
    {
        public string Name { get; set; }
        public string Protocol { get; set; }
        public double TypicalLoad { get; set; } // Кбит/с
    }
}