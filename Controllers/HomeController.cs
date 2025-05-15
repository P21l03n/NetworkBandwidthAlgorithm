using Microsoft.AspNetCore.Mvc;
using NetworkBandwidthAlgorithm.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;

namespace NetworkBandwidthAlgorithm.Controllers
{
    public class HomeController : Controller
    {
        private readonly List<DevicePreset> _devicePresets = new List<DevicePreset>
        {
            new DevicePreset { Name = "Умная колонка", Protocol = "Wi-Fi", TypicalLoad = 150 },
            new DevicePreset { Name = "Камера 1080p", Protocol = "Wi-Fi", TypicalLoad = 500 },
            new DevicePreset { Name = "Умная лампа", Protocol = "Zigbee", TypicalLoad = 5 },
            new DevicePreset { Name = "Датчик движения", Protocol = "Z-Wave", TypicalLoad = 2 }
        };

        private readonly List<Protocol> _defaultProtocols = new List<Protocol>
        {
            new Protocol { Name = "Wi-Fi", BaseCapacity = 1000 },
            new Protocol { Name = "Zigbee", BaseCapacity = 250 },
            new Protocol { Name = "Z-Wave", BaseCapacity = 100 }
        };

        public IActionResult Index()
        {
            Debug.WriteLine("Executing HomeController.Index()");
            ViewBag.DevicePresets = _devicePresets;
            ViewBag.Protocols = _defaultProtocols;
            var model = new BandwidthModel { Protocols = _defaultProtocols };
            Debug.WriteLine($"DevicePresets count: {ViewBag.DevicePresets?.Count ?? 0}");
            Debug.WriteLine($"Protocols count: {ViewBag.Protocols?.Count ?? 0}");
            return View(model);
        }

        [HttpPost]
        public IActionResult Calculate(BandwidthModel model)
        {
            if (!ModelState.IsValid)
            {
                // Для диагностики: выведем все ошибки в консоль
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Debug.WriteLine($"Ошибка валидации: {error.ErrorMessage}");
                }

                ViewBag.DevicePresets = _devicePresets;
                ViewBag.Protocols = _defaultProtocols;
                return View("Index", model);
            }

            model.Calculate();

            var chartData = new
            {
                labels = model.Protocols.Select(p => p.Name).ToArray(),
                datasets = new[]
                {
                    new
                    {
                        label = "Пропускная способность",
                        data = model.Protocols.Select(p => p.Capacity).ToArray(),
                        backgroundColor = "rgba(54, 162, 235, 0.5)"
                    },
                    new
                    {
                        label = "Текущая нагрузка",
                        data = model.Protocols.Select(p => p.CurrentLoad).ToArray(),
                        backgroundColor = "rgba(255, 99, 132, 0.5)"
                    }
                }
            };

            return View("Result", new CalculationResult
            {
                Model = model,
                ChartData = JsonSerializer.Serialize(chartData)
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}