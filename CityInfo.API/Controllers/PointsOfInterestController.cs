using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    public class PointsOfInterestController : Controller
    {
        private ICityInfoRepository _cityInfoRepository;

        private ILogger<PointsOfInterestController> _logger;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger, ICityInfoRepository cityInfoRepository)
        {
            _logger = logger;
            _cityInfoRepository = cityInfoRepository;
        }

        [HttpGet("{cityId}/pointsofinterest")]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            if(!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var poiResults =_cityInfoRepository.GetPointsOfInterestForCity(cityId);

            return Ok(Mapper.Map<IEnumerable<PointOfInterestDto>>(poiResults));
        }

        [HttpGet("{cityId}/pointsofinterest/{id}", Name = "GetPointOfInterest")]
        public IActionResult GetPointOfInterest(int cityId, int id)
        {
            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var poiResults = _cityInfoRepository.GetPointOfInterest(cityId, id);
            if(poiResults == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<PointOfInterestDto>(poiResults));
        }

        [HttpPost("{cityId}/pointsofinterest")]
        public IActionResult CreatePointOfInterest(int cityId, [FromBody] PointOfInterestForCreationDto pointOfInterest)
        {
            if (pointOfInterest == null)
            {
                return BadRequest();
            }
            if (pointOfInterest.Description == pointOfInterest.Name)
            {
                ModelState.AddModelError("Description", "Name and Description cannot be the same.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }


            var finalPoi = Mapper.Map<Entities.PointOfInterest>(pointOfInterest);

            _cityInfoRepository.AddPointOfInterestForCity(cityId, finalPoi);

            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "A problem occurred handling your request.");
            }

            var createdPoi = Mapper.Map<PointOfInterestDto>(finalPoi);

            return CreatedAtRoute("GetPointOfInterest", new { cityId, id = createdPoi.Id }, createdPoi);
        }

        [HttpPut("{cityId}/pointsofinterest/{id}")]
        public IActionResult UpdatePointOfInterest(int cityId, int id, [FromBody] PointOfInterestUpdateDto pointOfInterest)
        {
            if (pointOfInterest == null)
            {
                return BadRequest();
            }
            if (pointOfInterest.Description == pointOfInterest.Name)
            {
                ModelState.AddModelError("Description", "Name and Description cannot be the same.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var poiFromDb = _cityInfoRepository.GetPointOfInterest(cityId, id);
            if (poiFromDb == null)
            {
                return NotFound();
            }

            Mapper.Map(pointOfInterest, poiFromDb); //stores new values
            if(!_cityInfoRepository.Save())
            {
                return StatusCode(500, "A problem occurred handling your request.");
            }

            return NoContent();
        }

        //uses JSON Patch API
        [HttpPatch("{cityId}/pointsofinterest/{id}")]
        public IActionResult PartiallyUpdatePointOfInterest(int cityId, int id, [FromBody] JsonPatchDocument<PointOfInterestUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var poiFromDb = _cityInfoRepository.GetPointOfInterest(cityId, id);
            if (poiFromDb == null)
            {
                return NotFound();
            }

            var poiToPatch = Mapper.Map<PointOfInterestUpdateDto>(poiFromDb);
            
            patchDoc.ApplyTo(poiToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (poiToPatch.Description == poiToPatch.Name)
            {
                ModelState.AddModelError("Description", "Name and Description cannot be the same.");
            }
            TryValidateModel(poiToPatch);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Mapper.Map(poiToPatch, poiFromDb);

            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "A problem occurred handling your request.");
            }

            return NoContent();
        }

        [HttpDelete("{cityId}/pointsofinterest/{id}")]
        public IActionResult DeletePointOfInterest(int cityId, int id)
        {
            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var poiFromDb = _cityInfoRepository.GetPointOfInterest(cityId, id);
            if (poiFromDb == null)
            {
                return NotFound();
            }

            _cityInfoRepository.DeletePointOfInterest(poiFromDb);

            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "A problem occurred handling your request.");
            }

            return NoContent();
        }
    }
}