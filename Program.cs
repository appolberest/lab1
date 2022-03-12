using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    internal class Program
    {
        static Stop red = new Stop("Красное", "11:00", "13:00");
        static Stop green = new Stop("Зеленое", "11:20", "12:40");
        static Stop white = new Stop("Белое", "11:30", "12:30");
        static Stop blue = new Stop("Синее", "11:15", "12:45");
        static Stop orange = new Stop("Оранжевое", "11:40", "12:20");
        static Stop violet = new Stop("Фиолетовое", "11:50", "12:10");
        static Stop black = new Stop("Черное", "12:00", "12:00");

        static BusWay[] allWays;

        static void Main(string[] args)
        {
            int operation;

            Stop[] peachLine = new[] { red, green, white, violet, black };
            var peachLineReverse = peachLine.Reverse().ToArray();
            Stop[] pinkLine = new[] { red, blue, white };
            var pinkLineReverse = pinkLine.Reverse().ToArray();
            Stop[] limeLine = new[] { red, orange, violet };
            var limeLineReverse = limeLine.Reverse().ToArray();

            BusWay redBlack = new BusWay(101, peachLine);
            BusWay blackRed = new BusWay(102, peachLineReverse);

            BusWay redWhite = new BusWay(103, pinkLine);
            BusWay whiteRed = new BusWay(104, pinkLineReverse);

            BusWay redViolet = new BusWay(105, limeLine);
            BusWay violetRed = new BusWay(106, limeLineReverse);

            //создадим массив маршрутов
            allWays = new BusWay[] { redBlack, blackRed, redWhite, whiteRed, redViolet, violetRed };

            //покажем пользователю все доступные маршруты
            Console.WriteLine("Все маршруты в области:");
            foreach (BusWay way in allWays)
            {
                Console.WriteLine(way.ToString()); 
            }
            
            //пользователь выберет ветку-операцию
            Console.WriteLine("\n\nВыберите операцию\n1 - Маршрут из пункта А в пункт Б\n2 - Информация о населенном пункте (маршруты, время отправления)\n3 - Самый ранний маршрут из заданного пункта\n");
            
            do
            {
                Int32.TryParse(Console.ReadLine(), out operation);
            } while (operation > 3 || operation < 1);

            if (operation == 1)
            {
                Console.Write("Станция А:  ");
                Stop stationA = ConvertStopToVariable(Console.ReadLine());

                Console.Write("Станция Б:  ");
                Stop stationB = ConvertStopToVariable(Console.ReadLine());

                FindWay(stationA, stationB);
                 
            }
            else if (operation == 2)
            {
                Stop stationRequested = ConvertStopToVariable(Console.ReadLine());
                //Console.WriteLine(stationRequested.ToString());
                ShowInfo(stationRequested);
            }
            else if (operation == 3)
            {
                Stop stationTimeRequested = ConvertStopToVariable(Console.ReadLine());
                //Console.WriteLine(stationTimeRequested.ToString());
                ShowEarliestWay(stationTimeRequested);
            }
            Console.ReadKey();
        }
 
        static Stop ConvertStopToVariable(string inputStop)
        {
            Stop stop = new Stop();
            switch (inputStop)
            {
                case "Красное":
                    stop = red;
                    break;
                case "Зеленое":
                case "Зелёное":
                    stop = green;
                    break;
                case "Синее":
                    stop = blue;
                    break;
                case "Белое":
                    stop = white;
                    break;
                case "Черное":
                case "Чёрное":
                    stop = black;
                    break;
                case "Оранжевое":
                    stop = orange;
                    break;
                case "Фиолетовое":
                    stop = violet;
                    break;
             }
            return stop;
        }

        static void FindWay(Stop stopA, Stop stopB)
        {
            //вот это все не работает
            for (int i = 0; i<allWays.Length; i++)
            {
                bool foundA = false;
                bool foundB = false;
                int forComparisonLine = 0;
                int forComparisonStop = 0;

                for (int k = 0; k < allWays[i].Stops.Length; k++)
                {
                    if (allWays[i].Stops[k].StopName == stopA.StopName)
                    {
                        foundA = true;
                        forComparisonLine = i;
                        forComparisonStop = k;
                    }
                    if (allWays[i].Stops[k].StopName==stopB.StopName)
                        foundB = true;
                    if (foundA && foundB && forComparisonLine == i && k>=forComparisonStop)
                    {
                        int line = allWays[i].WayNumber;
                        Console.WriteLine("Пункт {0} и пункт {1} принадлежат одной ветке {2}. Ваш маршрут - линия {2}", stopA.StopName, stopB.StopName, line);
                    }
                }
            }
        }

        static void ShowInfo(Stop stop)
        {
            Console.WriteLine("\nИнформация об остановке автобуса в населенном пункте " + stop.StopName + ":\n");

            for (int i = 0; i < allWays.Length; i++)
            {
                for(int k=0; k<allWays[i].Stops.Length; k++)
                    if (allWays[i].Stops[k].StopName==stop.StopName)
                    { 
                        Console.Write("Маршрут: " + allWays[i].WayNumber);

                        if (allWays[i].WayNumber % 2 != 0)
                        {
                            if (k == allWays[i].Stops.Length - 1)
                            {
                                Console.Write(" с остановкой в " + allWays[i].Stops[k].StopTime + "\n");
                                continue;
                            }
                            
                            Console.Write(" с отправкой в " + allWays[i].Stops[k].StopTime + "\n");
                        }
                        else if (allWays[i].WayNumber % 2 == 0)
                        {
                            if (k == allWays[i].Stops.Length - 1)
                            {
                                Console.Write(" с остановкой в " + allWays[i].Stops[k].StopTimeReverse + "\n");
                                continue;
                            }
                            Console.Write(" с отправкой в " + allWays[i].Stops[k].StopTimeReverse + "\n");
                        }
                    }
            }
        }

        static void ShowEarliestWay(Stop stop)
        {
            Console.WriteLine("\nCамый ранний маршрут из населенного пункта " + stop.StopName + ":\n");
            int thereMin = stop.ConvertTime(stop.StopTime);
            int reverseMin = stop.ConvertTime(stop.StopTimeReverse);
            

            if (thereMin < reverseMin)
            {
                Console.WriteLine("Время отправления " + stop.StopTime);
                for (int i = 0; i < allWays.Length; i++)
                {
                    for (int k = 0; k < allWays[i].Stops.Length; k++)
                    {
                        if (allWays[i].Stops[k].StopName == stop.StopName && i % 2 == 0)
                        {
                            if (k == allWays[i].Stops.Length - 1) continue;
                            Console.WriteLine("Маршрут: " + allWays[i].WayNumber);
                        }
                    }
                }
            }
            else 
            {
                Console.WriteLine("Время отправления " + stop.StopTimeReverse);
                for (int i = 0; i < allWays.Length; i++)
                {
                    for (int k = 0; k < allWays[i].Stops.Length; k++)
                    {
                        if (allWays[i].Stops[k].StopName == stop.StopName && i % 2 != 0)
                        {
                            Console.WriteLine("Маршрут: " + allWays[i].WayNumber);
                        }
                    }
                }
            }
        }

    }
}
