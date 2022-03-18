﻿using API.Entity;
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
        private readonly IPrime _primeService;

        public PrimesController(IPrime primeService)
        {
            _primeService = primeService;
        }

        // GET api/<PrimesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            
            var isPrimeResponse = _primeService.isPrime(id);
            return Ok(new
            {
                response = isPrimeResponse
            });
        }


        // POST api/<PrimesController>
        [HttpPost]
        public IActionResult Post([FromBody] Response entity)
        {
            try
            {
                var responseCount = _primeService.countPrime(entity.start, entity.end);

                return Ok(new
                {
                    response = responseCount

                });

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
