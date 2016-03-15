using System.Collections.Generic;
using Couverts.Example.Services.Results.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Couverts.Example.Services.Calls.GetAvailableTimes {
    public class GetAvailableTimesResult {
        public List<Time> Times { get; set; }

        public NoTimesAvailableDetails NoTimesAvailable { get; set; }

        public class Time {
            public int Hours { get; set; }
            public int Minutes { get; set; }

            public override string ToString() {
                return Hours.ToString("00") + ":" + Minutes.ToString("00");
            }
        }

        public class NoTimesAvailableDetails {
            [JsonConverter(typeof(StringEnumConverter))]
            public Reason Reason { get; set; }

            public MultilingualTextField Message { get; set; }
        }

        public enum Reason {
            Unknown,
            RestaurantIsClosed,
            RestaurantDoesNotAcceptOnlineReservations,
            RestaurantIsFullyBooked,
            RequestedNumPersonsOutOfBounds
        }
    }
}