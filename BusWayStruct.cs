using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    struct BusWay
    {
        public int WayNumber { get; set; }
        public Stop[] Stops { get; set; }
        public BusWay(int waynumber, Stop[] stops)
        {
            WayNumber = waynumber;
            Stops = stops;
        }

        public override string ToString()
        {
            string stops = "";
            for (int i = 0; i < Stops.Length; i++)
            {
                stops += Stops[i].StopName + " ";
            }
            return "Маршрут номер " + WayNumber + " : " + stops;
        }
    }
}
