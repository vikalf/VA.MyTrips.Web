using System;
using System.Collections.Generic;

namespace VA.MyTrips.Web.ViewModels
{
    public class TripDetailedModel : TripModel
    {
        public string GeoLocation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<PhotoModel> Photos { get; set; }

        public string ToJsonString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

    }
}
