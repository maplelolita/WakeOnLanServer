using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WolServer.Models;

namespace WolServer.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Index(WolModel model)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(new
                {
                    result  = false,
                    message = "Invalid Value",
                });
            }

            try
            {
                await WolCore.Wake(model.Host, model.Port, model.MacAddr);
                return new JsonResult(new
                {
                    result = true,
                    host = model.Host,
                    port = model.Port,
                    addr = model.MacAddr
                });
            }
            catch (Exception e)
            {
                return new JsonResult(new
                {
                    result  = false,
                    host    = model.Host,
                    port    = model.Port,
                    addr    = model.MacAddr,
                    message = e.Message,
                    ex      = e.ToString()
                });
            }
        }
    }
}
