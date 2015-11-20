using System;
using System.Collections.Generic;
using System.Linq;
using WINConnect.Models;

namespace WINConnect.Models.Extensions
{
    public static class SeaBookingExtensions
    {
        public static string GetINTTRAReferenceNumber(this IEnumerable<SeaBooking_Reference> references)
        {
            var reference = references.FirstOrDefault(x => x.Type.Code == "INTTRAReferenceNumber");
            if (reference == null)
            {
                return null;
            }
            return reference.Number;
        }
        public static string GetBookingNumber(this IEnumerable<SeaBooking_Reference> references)
        {
            var reference = references.FirstOrDefault(x => x.Type.Code == "BookingNumber");
            if (reference == null)
            {
                return null;
            }
            return reference.Number;
        }

        public static string GetPOL(this IEnumerable<SeaBooking_Location> locations)
        {
            var location = locations.FirstOrDefault(x => x.Type.Code == "PortOfLoad");
            if (location == null)
            {
                return null;
            }
            return location.Name;
        }

        public static string GetPOD(this IEnumerable<SeaBooking_Location> locations)
        {
            var location = locations.FirstOrDefault(x => x.Type.Code == "PortOfDischarge");
            if (location == null)
            {
                return null;
            }
            return location.Name;
        }
    }
}