using System.Collections.Generic;
using System.Linq;
using WINConnect.Models;

namespace WINConnect.Models.Extensions
{
    public static class ShipmentExtensions
    {
        // PLD - PlaceOfDelivery
        // POL - PortOfLoading
        // POD - PortOfDischarge
        // PLR - PlaceOfReceipt
        public static string GetOriginLocation(this ICollection<Shipment_Location> locations)
        {
            Shipment_Location PLR = locations.FirstOrDefault(x => x.LocationType.Code == "PlaceOfReceipt");
            Shipment_Location POL = locations.FirstOrDefault(x => x.LocationType.Code == "PortOfLoading");

            if (POL != null)
            {
                return POL.LocationName;
            }

            if (PLR != null)
            {
                return PLR.LocationName;
            }
            return "N/A";
        }
        public static string GetDestinationLocation(this ICollection<Shipment_Location> locations)
        {
            Shipment_Location PLD = locations.FirstOrDefault(x => x.LocationType.Code == "PlaceOfDelivery");
            Shipment_Location POD = locations.FirstOrDefault(x => x.LocationType.Code == "PortOfDischarge");

            if (POD != null)
            {
                return POD.LocationName;
            }

            if (PLD != null)
            {
                return PLD.LocationName;
            }
            return "N/A";
        }
    }
}