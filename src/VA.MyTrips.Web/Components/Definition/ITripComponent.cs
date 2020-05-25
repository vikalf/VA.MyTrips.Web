using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VA.MyTrips.Web.Components.Definition
{
    public interface ITripComponent
    {
        Task<List<ViewModels.TripModel>> GetTrips();

        Task<ViewModels.TripDetailedModel> GetTripDetails(int tripId);

        Task<ViewModels.TripDetailedModel> CreateTrip(ViewModels.TripDetailedModel newTrip);

        Task<bool> UploadPhoto(int tripId, byte[] file);

        Task<bool> ArchivePhoto(int tripId, string photoId);

    }
}
