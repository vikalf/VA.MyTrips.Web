using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VA.MyTrips.Service;
using VA.MyTrips.Web.Components.Definition;
using VA.MyTrips.Web.ViewModels;

namespace VA.MyTrips.Web.Components.Implementation
{
    public class TripComponent : ITripComponent
    {
        private readonly Service.Trip.TripClient _tripclient;

        public TripComponent(Trip.TripClient tripclient)
        {
            _tripclient = tripclient;
        }

        public async Task<List<ViewModels.TripModel>> GetTrips()
        {

            var reply = await _tripclient.GetTripsAsync(new Service.EmtpyRequest(), deadline: DateTime.UtcNow.AddSeconds(5));

            var result = (from t in reply.Trips
                          select new ViewModels.TripModel
                          {
                              TripId = t.TripId,
                              Name = t.Name,
                              Destination = t.Destination
                          }).ToList();

            return result;

        }

        public async Task<TripDetailedModel> GetTripDetails(string tripId)
        {
            var reply = await _tripclient.GetTripAsync(new TripRequest { TripId = tripId }, deadline: DateTime.UtcNow.AddSeconds(5));

            return new TripDetailedModel
            {
                TripId = reply.TripId,
                Destination = reply.Destination,
                Name = reply.Name,
                EndDate = System.DateTime.Parse(reply.EndDate),
                GeoLocation = reply.GeoLocation,
                Photos = reply.Photos.Select(e => new ViewModels.PhotoModel
                {
                    TripId = e.TripId,
                    Url = e.Url
                }).ToList(),
                StartDate = System.DateTime.Parse(reply.StartDate)
            };

        }

        public async Task<TripDetailedModel> CreateTrip(TripDetailedModel newTrip)
        {
            var reply = await _tripclient.CreateTripAsync(new CreateTripRequest
            {
                Name = newTrip.Name,
                Destination = newTrip.Destination,
                StartDate = newTrip.StartDate.ToString("g"),
                EndDate = newTrip.EndDate.ToString("g"),
                GeoLocation = newTrip.GeoLocation
            }, deadline: DateTime.UtcNow.AddSeconds(5));

            return new TripDetailedModel
            {
                Name = reply.Name,
                Destination = reply.Destination,
                GeoLocation = reply.GeoLocation,
                EndDate = DateTime.Parse(reply.EndDate),
                StartDate = DateTime.Parse(reply.StartDate),
                TripId = reply.TripId,
                Photos = new List<ViewModels.PhotoModel>()
            };

        }

        public async Task<bool> UploadPhoto(string tripId, byte[] file, string fileName)
        {

            var request = new UploadPhotoRequest { TripId = tripId, FileName = fileName };
            request.Filebytes = ByteString.CopyFrom(file);

            var reply = await _tripclient.UploadPhotoAsync(request);

            return reply.IsSuccess;
        }
        public async Task<bool> ArchivePhoto(string tripId, string photoId)
        {
            var reply = await _tripclient.ArchivePhotoAsync(new ArchivePhotoRequest { TripId = tripId, PhotoId = photoId });
            return reply.IsSuccess;
        }
    }
}