using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CityInfo.API.Data;
using CityInfo.API.DTOs;
using CityInfo.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public CitiesController(ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public IActionResult GetCities()
        {
            var cityEntities = _cityInfoRepository.GetCities();

            //var results = new List<CityWithoutPointsOfInterestDto>();
            var results = _mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cityEntities);

            return Ok(results);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetCityById(int id, bool includePointsOfInterest = false)
        {
            #region DEMO
            //var city = CitiesDataStore.CurrentCitiesDataStore.Cities.FirstOrDefault(x => x.Id == id);

            //if (city is null)
            //{
            //    return NotFound();
            //}

            //return Ok(city);
            #endregion

            var city = _cityInfoRepository.GetCity(id, includePointsOfInterest);

            if (city is null)
            {
                return NotFound();
            }

            #region CityWithPointsOfInterest

            if (includePointsOfInterest)
            {
                var cityWithPointsOfInterest = _mapper.Map<CityDto>(city);

                return Ok(cityWithPointsOfInterest);
            }

            #endregion

            #region CityWithoutPointsOfInterest

            var cityWithoutPointsOfInterest = _mapper.Map<CityWithoutPointsOfInterestDto>(city);

            return Ok(cityWithoutPointsOfInterest);

            #endregion
        }
    }
}
