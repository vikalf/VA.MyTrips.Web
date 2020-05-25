using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VA.MyTrips.Web.Components.Definition;
using VA.MyTrips.Web.ViewModels;

namespace VA.MyTrips.Web.Components.Implementation
{
    public class TripComponent : ITripComponent
    {

        public Task<List<TripModel>> GetTrips()
        {
            throw new NotImplementedException();
        }

        public Task<TripDetailedModel> GetTripDetails(int tripId)
        {
            throw new NotImplementedException();
        }

        public Task<TripDetailedModel> CreateTrip(TripDetailedModel newTrip)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UploadPhoto(int tripId, byte[] file)
        {
            throw new NotImplementedException();
        }
        public Task<bool> ArchivePhoto(int tripId, string photoId)
        {
            throw new NotImplementedException();
        }
    }
}
