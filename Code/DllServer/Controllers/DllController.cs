using Microsoft.AspNetCore.Cors;
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
            return File(System.IO.File.ReadAllBytes(@"F:\FH_Technikum_Wien\2_Se\Websecurity\Demo\Software_Integrity_Failure_Demo\Code\OurLibrary\bin\Debug\net6.0\OurLibrary.dll"), "application/octet-stream", "Ourlibrary.dll");
        }
    }
}
