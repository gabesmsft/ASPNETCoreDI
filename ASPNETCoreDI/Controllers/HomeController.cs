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

        private readonly IMyDependency1 _myDependency1;

        private readonly IMyDependency2 _myDependency2;

        //The dependencies get injected into this client class' constructor

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IMyDependency1 myDependency1, IMyDependency2 myDependency2)
        {
            _logger = logger;

            _configuration = configuration;

            _myDependency1 = myDependency1;

            _myDependency2 = myDependency2;
        }
        public IActionResult Index()
        {
            string fakeAppSetting = _configuration["FakeAppSetting"];

            _logger.LogInformation("We have logging, thanks to the injected ILogger");

            _logger.LogInformation("FakeAppSettingValue: " + fakeAppSetting);

            //if the following prints "MyDependency1 successfully injected", we know MyDependency1 was sucessfully injected because this message was passed into the constructor during registration
            _logger.LogInformation("MyDependendy.TestDependencyInjection() return value: " + _myDependency1.TestDependencyInjection());

            _myDependency2.DoSomeLogging();

            return View();
        }
    }
}
