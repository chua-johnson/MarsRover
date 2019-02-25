using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarsRoverService.CustomBinder;
using MarsRoverService.Models;
using MarsRoverService.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarsRoverService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NasaController : ControllerBase
    {
        private readonly INasaService _service;

        public NasaController(INasaService service)
        {
            _service = service;
        }

        //[HttpGet("{date}")]
        //public NasaInfo Get([DateTimeModelBinder(DateFormat = "MM/dd/yy")] string date)
        //{
        //    NasaInfo nasaInfo = _service.GetNASAInfo(date);
        //    return nasaInfo;
        //}

        [HttpGet("{date}")]
        public ActionResult<NasaInfo> Get(string date)
        {
            if(!string.IsNullOrWhiteSpace(date))
            {
                bool isValidDate = _service.IsValidDate(date);

                if(!isValidDate)
                {
                    return BadRequest("Invalid Date");
                }
            }
            NasaInfo nasaInfo = _service.GetNASAInfo(date);
            return Ok(nasaInfo);
        }
    }
}