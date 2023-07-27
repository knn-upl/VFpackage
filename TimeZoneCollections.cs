using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VF
{
    public class TimeZoneCollections
    {
        public static List<(int Id, string DisplayName)> GetTimeZones()
        {
            var timeZones = new List<(int, string)>();

            foreach (var timeZone in TimeZoneInfo.GetSystemTimeZones())
            {
                timeZones.Add((Convert.ToInt32(timeZone.Id), timeZone.DisplayName));
            }

            return timeZones;
        }
    }
}
