namespace NetworkBandwidthAlgorithm.Models
{
    public class BandwidthModel
    {
        public int WifiDevices { get; set; }
        public int ZigbeeDevices { get; set; }
        public int ZWaveDevices { get; set; }

        public double AudioLoad { get; set; } = 150;
        public double VideoLoad { get; set; } = 500;
        public double ControlLoad { get; set; } = 10;
        public double LightLoad { get; set; } = 5;

        public double WifiEfficiency { get; set; } = 0.6;
        public double ZigbeeEfficiency { get; set; } = 0.8;
        public double ZWaveEfficiency { get; set; } = 0.7;

        public double WifiBandwidth { get; private set; }
        public double ZigbeeBandwidth { get; private set; }
        public double ZWaveBandwidth { get; private set; }
        public bool IsOptimal { get; private set; }
        public string Status { get; private set; }

        public void Calculate()
        {
            WifiBandwidth = 1000 * WifiEfficiency;
            ZigbeeBandwidth = 250 * ZigbeeEfficiency;
            ZWaveBandwidth = 100 * ZWaveEfficiency;

            double wifiLoad = WifiDevices * (AudioLoad + VideoLoad + ControlLoad);
            double zigbeeLoad = ZigbeeDevices * LightLoad;
            double zwaveLoad = ZWaveDevices * ControlLoad;

            bool wifiOk = wifiLoad <= WifiBandwidth;
            bool zigbeeOk = zigbeeLoad <= ZigbeeBandwidth;
            bool zwaveOk = zwaveLoad <= ZWaveBandwidth;

            IsOptimal = wifiOk && zigbeeOk && zwaveOk;
            Status = IsOptimal ? "Оптимальная нагрузка" : "Перегрузка сети!";
        }
    }
}
