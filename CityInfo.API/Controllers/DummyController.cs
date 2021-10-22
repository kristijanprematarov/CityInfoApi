using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Data;

namespace CityInfo.API.Controllers
{
    [Route("api/testdatabase")]
    [ApiController]
    public class DummyController : ControllerBase
    {
        private readonly DataContext _context;

        public DummyController(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        public IActionResult TestDatabase()
        {
            return Ok();
        }
    }
}
