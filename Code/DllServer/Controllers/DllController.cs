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
        private static string path;

        static DllController()
        {
            path = @"C:\Users\Grego\source\repos\Software_Integrity_Failure_Demo\Code\WPFTools\bin\Debug\net6.0-windows\WPFTools.dll";
        }

        [HttpGet("load")]
        public async Task<IActionResult> LoadDll()
        {
            return File(System.IO.File.ReadAllBytes(path), "application/octet-stream", "Ourlibrary.dll");
        }

        [HttpGet("update")]
        public async Task<IActionResult> UpdateDll(string password, string updatedPath)
        {
            if (password != "12345")
            {
                return Unauthorized();
            }

            path = updatedPath;

            return Ok();
        }
    }
}
