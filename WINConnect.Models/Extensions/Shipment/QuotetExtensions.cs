using System.Collections.Generic;
using System.Linq;
using WINConnect.Models;

namespace WINConnect.Models.Extensions
{
    public static class QuoteExtensions
    {
        // PLD - PlaceOfDelivery
        // POL - PortOfLoading
        // POD - PortOfDischarge
        // PLR - PlaceOfReceipt
        public static Quote GetLatestQuote(this ICollection<Quote> quotes)
        {
            Quote quote = quotes.OrderByDescending(x => x.CreatedOn).FirstOrDefault();

            if (quote == null)
            {
                return new Quote();
            }

            return quote;
        }
    }
}