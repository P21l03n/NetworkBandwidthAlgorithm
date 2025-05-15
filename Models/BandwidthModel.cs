using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NetworkBandwidthAlgorithm.Models
{
    public class BandwidthModel
    {
        [Required(ErrorMessage = "Необходимо указать хотя бы одно устройство")]
        public List<NetworkDevice> Devices { get; set; } = new List<NetworkDevice>();

        public List<Protocol> Protocols { get; set; } = new List<Protocol>();

        public double TotalLoad { get; set; }
        public double TotalCapacity { get; set; }
        public bool IsOptimal { get; set; }
        public string Message { get; set; }

        public void Calculate()
        {
            TotalLoad = 0;
            TotalCapacity = 0;

            foreach (var protocol in Protocols)
            {
                protocol.CurrentLoad = 0;
                protocol.Capacity = protocol.BaseCapacity * protocol.Efficiency;
            }

            foreach (var device in Devices)
            {
                var protocol = Protocols.Find(p => p.Name == device.Protocol);
                if (protocol != null)
                {
                    protocol.CurrentLoad += device.Load;
                    TotalLoad += device.Load;
                }
            }

            foreach (var protocol in Protocols)
            {
                TotalCapacity += protocol.Capacity;
            }

            IsOptimal = TotalLoad <= TotalCapacity;
            Message = IsOptimal ? "Оптимальная нагрузка" : "Перегрузка сети!";
        }
    }

    public class NetworkDevice
    {
        [Required(ErrorMessage = "Выберите устройство")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Выберите протокол")]
        public string Protocol { get; set; }

        [Range(0.1, double.MaxValue, ErrorMessage = "Нагрузка должна быть больше 0")]
        public double Load { get; set; }
    }

    public class Protocol
    {
        public string Name { get; set; }
        public double BaseCapacity { get; set; } // Кбит/с
        public double Efficiency { get; set; } = 0.7;
        public double Capacity { get; set; }
        public double CurrentLoad { get; set; }
    }
}