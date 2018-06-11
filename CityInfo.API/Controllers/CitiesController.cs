using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")] //prepends this to beginning of all routes in this class
    public class CitiesController : Controller
    {
        private ICityInfoRepository _cityInfoRepository;

        public CitiesController(ICityInfoRepository cityInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository;
        }

        [HttpGet()]
        public IActionResult GetCities()
        {
            var cityEntities = _cityInfoRepository.GetCities();

            var results = Mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cityEntities);

            return Ok(results);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id, bool includePoi = false)
        {
            var city = _cityInfoRepository.GetCity(id, includePoi);
            if(city == null)
            {
                return NotFound();
            }

            if(includePoi)
            {
                return Ok(Mapper.Map<CityDto>(city));
            }

            return Ok(Mapper.Map<CityWithoutPointsOfInterestDto>(city));
        }
    }
}
