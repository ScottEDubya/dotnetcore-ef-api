using CityInfo.API.Entities;
using CityInfo.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API
{
    //This entire class is just used to seed data in the db using the ORM
    public static class CityInfoExtensions
    {
        public static void EnsureSeedDataForContext(this CityInfoContext context)
        {
            if(context.Cities.Any())
            {
                return;
            }

            //seed data
            var cities = new List<City>() {
                new City()
                {
                    Name = "Cleveland",
                    Description = "Party wit da best in the land",
                    PointsOfInterest = new List<PointOfInterest>()
                    {
                        new PointOfInterest()
                        {
                            Name = "R&R Hall Of Fame",
                            Description = "Weowweow",
                        },
                        new PointOfInterest()
                        {
                            Name = "The Flats",
                            Description = "Drink Drank Drunk",
                        }
                    }
                },
                new City()
                {
                    Name = "Shittsburgh",
                    Description = "Party wit nobody cool",
                    PointsOfInterest = new List<PointOfInterest>()
                    {
                        new PointOfInterest()
                        {
                            Name = "Mountain",
                            Description = "Pretty",
                        },
                        new PointOfInterest()
                        {
                            Name = "Steelers Fans",
                            Description = "Not pretty",
                        }
                    }
                },
                new City()
                {
                    Name = "Detroit",
                    Description = "Maaaaan that be real gross",
                    PointsOfInterest = new List<PointOfInterest>()
                    {
                        new PointOfInterest()
                        {
                            Name = "Cars",
                            Description = "Omg wow cars",
                        },
                        new PointOfInterest()
                        {
                            Name = "The Hood",
                            Description = "Gun violence",
                        }
                    }
                }
            };

            context.Cities.AddRange(cities);
            context.SaveChanges();
        }
    }
}
