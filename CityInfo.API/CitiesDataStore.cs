using CityInfo.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public static CitiesDataStore Current { get; } = new CitiesDataStore();
        public List<CityDto> Cities { get; set; }
        public CitiesDataStore()
        {
            Cities = new List<CityDto>() {
                new CityDto()
                {
                    Id = 1,
                    Name = "Cleveland",
                    Description = "Party wit da best in the land",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "R&R Hall Of Fame",
                            Description = "Weowweow",
                        },
                        new PointOfInterestDto()
                        {
                             Id = 2,
                            Name = "The Flats",
                            Description = "Drink Drank Drunk",
                        }
                    }
                },
                new CityDto()
                {
                    Id = 2,
                    Name = "Shittsburgh",
                    Description = "Party wit nobody cool",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "Mountain",
                            Description = "Pretty",
                        },
                        new PointOfInterestDto()
                        {
                             Id = 2,
                            Name = "Steelers Fans",
                            Description = "Not pretty",
                        }
                    }
                },
                new CityDto()
                {
                    Id = 3,
                    Name = "Detroit",
                    Description = "Maaaaan that be real gross",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "Cars",
                            Description = "Omg wow cars",
                        },
                        new PointOfInterestDto()
                        {
                             Id = 2,
                            Name = "The Hood",
                            Description = "Gun violence",
                        }
                    }
                }
            };
        }
    }
}
