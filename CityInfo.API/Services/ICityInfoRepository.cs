using CityInfo.API.Entities;
using System.Collections.Generic;

namespace CityInfo.API.Services
{
    public interface ICityInfoRepository
    {
        IEnumerable<City> GetCities();
        City GetCity(int cityId, bool includePoi);
        PointOfInterest GetPointOfInterest(int cityId, int pointOfInterestId);
        IEnumerable<PointOfInterest> GetPointsOfInterestForCity(int cityId);
        bool CityExists(int cityId);
        void AddPointOfInterestForCity(int cityId, PointOfInterest pointOfInterest);
        bool Save();
        void DeletePointOfInterest(PointOfInterest pointOfInterest);
    }
}
