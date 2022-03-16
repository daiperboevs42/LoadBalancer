using API.Entity;
using API.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrimesController : ControllerBase
    {
        private readonly IPrime _prime;

        public PrimesController(IPrime prime)
        {
            _prime = prime;
        }

        // GET api/<PrimesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var isPrimeResponse = _prime.isPrime(id);
            return Ok(new
            {
                response = isPrimeResponse
            });
        }

        // POST api/<PrimesController>
        [HttpPost]
        public IActionResult Post([FromBody] Response newObject)
        {
            try
            {
                var countResponse = _prime.countPrime(newObject.from, newObject.to);

                return Ok(new
                {
                    response = countResponse
                });

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
