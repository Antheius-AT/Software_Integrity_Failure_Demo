using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;

namespace DllServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DllController : ControllerBase
    {
        [HttpGet("load")]
        public async Task<IActionResult> LoadDll()
        {
            var asm = Assembly.Load("OurLibrary.dll");
           
            return Content(JsonConvert.SerializeObject(asm));
        }
    }
}
