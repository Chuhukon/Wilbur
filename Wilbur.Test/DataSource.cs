using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wilbur.DataAccess;

namespace Wilbur.Test
{
    /// <summary>
    /// Dump of datasoures used for testing purpose.
    /// </summary>
    public class DataSource
    {
        public static IList<AviationPioneer> Pioneers()
        {
                List<AviationPioneer> result = new List<AviationPioneer>();
                result.Add(new AviationPioneer
                {
                    FirstName = "Orville",
                    LastName = "Wright",
                    Born = new DateTime(1871, 8, 19),
                    Nationality = "US",
                    Died = new DateTime(1948, 1, 30),
                    FavoriteAircraft = "Wright Flyer I",
                    FirstFlight = new DateTime(1903, 12, 17)
                });

                result.Add(new AviationPioneer
                {
                    FirstName = "Wilbur",
                    LastName = "Wright",
                    Born = new DateTime(1867, 8, 19),
                    Nationality = "US",
                    Died = new DateTime(1912, 5, 30),
                    FavoriteAircraft = "Wright Flyer I",
                    FirstFlight = new DateTime(1903, 12, 17)
                });

                result.Add(new AviationPioneer
                {
                    FirstName = "Anthony",
                    LastName = "Fokker",
                    Born = new DateTime(1890, 4, 6),
                    Nationality = "NL",
                    Died = new DateTime(1939, 12, 23),
                    FavoriteAircraft = "Spin",
                    FirstFlight = new DateTime(1911, 8, 31)
                });

                result.Add(new AviationPioneer
                {
                    FirstName = "Neil",
                    LastName = "Armstrong",
                    Born = new DateTime(1930, 8, 5),
                    Nationality = "US",
                    FavoriteAircraft = "X-15",
                    FirstFlight = new DateTime(1956, 1, 1)
                });

                result.Add(new AviationPioneer
                {
                    FirstName = "Jeroen",
                    LastName = "Wijdeven",
                    Born = new DateTime(1982, 5, 20),
                    Nationality = "NL",
                    FavoriteAircraft = "Cessna 172 Skyhawk",
                    FirstFlight = new DateTime(1996, 4, 1)
                });

                result.Add(new AviationPioneer
                {
                    FirstName = "Martin",
                    LastName = "Schröder",
                    Born = new DateTime(1931, 5, 13),
                    Nationality = "NL",
                    FavoriteAircraft = "Douglas DC-8",
                    FirstFlight = new DateTime(1958, 5, 24)
                });

                return result;
        }
    }
}
