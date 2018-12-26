using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using IdentityServer4.Quickstart.UI;
using IdentityServer4.Services;
using Newtonsoft.Json;
using ClientApp._GoogleHome;

namespace ClientApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;

        public HomeController(IIdentityServerInteractionService interaction)
        {
            _interaction = interaction;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        // }

        public async Task<IActionResult> Error(string errorId)
        {
            var vm = new ErrorViewModel();

            // retrieve error details from identityserver
            var message = await _interaction.GetErrorContextAsync(errorId);
            vm.Error = message;


            return View("Error", vm);
        }

        
        // [HttpPost("test")]
        // //[Authorize]
        // public IActionResult Test([FromBody] dynamic data)
        // {
        //     var dtjs = JsonConvert.SerializeObject(data);
        //     Console.WriteLine("------------------------------------- ");
        //     Console.WriteLine("google data: ");
        //     Console.WriteLine(dtjs);
        //     Console.WriteLine("------------------------------------- ");
        //     var response = new SyncResponse{
        //         requestId = data["requestId"],
        //         payload = new Payload{
        //             agentUserId = "123.123",
        //             devices = new List<Device>{GetDevice1()}
        //         }
        //     };
        //     Console.WriteLine("sync response " + JsonConvert.SerializeObject(response));
        //     return new JsonResult(response);
        // }

    }
}
