using Microsoft.AspNetCore.Mvc;
using RabbitMQProject.Models;
using RabbitMQProject.RabbitMQ;
using System.Diagnostics;

namespace RabbitMQProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRabbitProducer rabbitProducer;
        private readonly IRabbitConsumer consumer;
        public HomeController(ILogger<HomeController> logger,IRabbitProducer rabbit,IRabbitConsumer rabbitConsumer)
        {
            rabbitProducer = rabbit;
            _logger = logger;
            consumer = rabbitConsumer;
        }

        public IActionResult Index()
        {
            var prodcut=new Product { Id = 1 ,Name="Onur"};
           rabbitProducer.SendMessage<Product>(prodcut);
            return View();
        }

        public IActionResult Privacy()
        {
            consumer.ReceiveMessage();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
