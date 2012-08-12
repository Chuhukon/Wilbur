using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wilbur.DataAccess
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Born { get; set; }
        public DateTime? Died { get; set; }
        public string Nationality { get; set; }
    }

    public class AviationPioneer : Person
    {
        public string FavoriteAircraft { get; set; }
        public DateTime FirstFlight { get; set; }
    }
}
