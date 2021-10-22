using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CityInfo.API.DTOs;
using CityInfo.API.Entities;

namespace CityInfo.API.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<City, CityWithoutPointsOfInterestDto>();
            CreateMap<City,CityDto>();
            CreateMap<PointOfInterest, PointOfInterestDto>();
            CreateMap<PointOfInterestCreateDto, PointOfInterest>();
            CreateMap<PointOfInterestUpdateDto, PointOfInterest>();
            CreateMap<PointOfInterest, PointOfInterestUpdateDto>();
        }
    }
}
