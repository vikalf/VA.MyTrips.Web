using System.Collections.Generic;
using System.Threading.Tasks;

namespace VA.MyTrips.Web.Components.Definition
{
    public interface ITripComponent
    {
        Task<List<ViewModels.TripModel>> GetTrips();

        Task<ViewModels.TripDetailedModel> GetTripDetails(string tripId);

        Task<ViewModels.TripDetailedModel> CreateTrip(ViewModels.TripDetailedModel newTrip);

        Task<bool> UploadPhoto(string tripId, byte[] file, string fileName);

        Task<bool> ArchivePhoto(string tripId, string photoId);

    }
}
