namespace NetworkBandwidthAlgorithm.Models
{
    public class ErrorViewModel
    {
        // ID запроса для диагностики ошибок
        public string? RequestId { get; set; }

        // Показывать ли ID запроса на странице
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}