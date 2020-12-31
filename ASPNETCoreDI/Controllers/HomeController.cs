using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNETCoreDI.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace ASPNETCoreDI.Controllers
{
    //This is the "client class", with respect to this DI example
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IConfiguration _configuration;

        private readonly IMyDependency1 _myDependency;

        //The dependencies get injected into this client class' constructor

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IMyDependency1 myDependency)
        {
            _logger = logger;

            _configuration = configuration;

            _myDependency = myDependency;
        }
        public IActionResult Index()
        {
            string fakeAppSetting = _configuration["FakeAppSetting"];

            _logger.LogInformation("We have logging, thanks to the injected ILogger");

            _logger.LogInformation("FakeAppSettingValue: " + fakeAppSetting);

            _logger.LogInformation("MyDependendy.TestDependencyInjection() return value: " + _myDependency.TestDependencyInjection());

            return View();
        }
    }
}
