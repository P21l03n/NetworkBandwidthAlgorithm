namespace NetworkBandwidthAlgorithm.Models
{
    public class ScenarioComparisonResult
    {
        public BandwidthModel Scenario1 { get; set; }
        public BandwidthModel Scenario2 { get; set; }
        public string ChartData1 { get; set; }
        public string ChartData2 { get; set; }
    }
}