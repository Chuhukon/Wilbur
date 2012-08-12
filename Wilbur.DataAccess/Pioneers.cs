using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wilbur.DataAccess
{
    public enum Source
    {
        Database,
        WebService,
        XmlFile
    }

    //[MockClass] nadeel om datetime.now te addresseren met mocken moet deze attribuut gebruikt worden, daardoor al niet beschikbaar in 1.1.
    // http://www.telerik.com/community/forums/justmock/general-discussions/mock-datetime-now.aspx
    public class Pioneers
    {
        private IList<AviationPioneer> _dataContext { get; set; }

        public Pioneers(Source source)
        {
            //Based on given parameter context is loaded from an external source
        }

        public List<AviationPioneer> GetDutchPioneers()
        {
            var results = from p in _dataContext
                          where p.Nationality.Equals("NL")
                          select p;

            return results.ToList();
        }

        public List<AviationPioneer> OlderThan65()
        {
            return _dataContext
                .Where(p => !p.Died.HasValue && CalculateAge(p.Born) > 65)
                .ToList();
        }

        public static int CalculateAge(DateTime birth)
        {
            DateTime today = DateTime.Now;
            int age = today.Year - birth.Year;

            if ((today.Month < birth.Month) ||
                (today.Month.Equals(birth.Month)
                    && today.Day < birth.Day))
                return age - 1;
            
            return age;
        }
    }
}
