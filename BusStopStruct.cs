using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    struct Stop
    {
        public string StopName { get; set; }
        public string StopTime { get; set; }
        public string StopTimeReverse { get; set; }

        public Stop(string stopName, string stopTime, string stopTimeReverse)
        {
            StopName = stopName;
            StopTime = stopTime;
            StopTimeReverse = stopTimeReverse;
        }
        public override string ToString()
        {
            return StopName;
        }

        public int ConvertTime(string timeHoursMinutes)
        {
            int timeMinutes;
            string[] hourMinutes = timeHoursMinutes.Split(':');
            timeMinutes = int.Parse(hourMinutes[0]) * 60 + int.Parse(hourMinutes[1]);
            return timeMinutes;
        }
    }
}
