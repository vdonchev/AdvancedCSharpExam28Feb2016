namespace _04.Events
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public static class Events
    {
        private const string validEventPattern 
            = @"^\s*#([a-zA-Z]+):\s*@([a-zA-Z]+)\s*(0[0-9]|[0-9]|1[0-9]|2[0-3]):([0-9]|[0-5][0-9])\s*$";

        private static SortedDictionary<string, SortedDictionary<string, List<MyTime>>> events;
         
        public static void Main()
        {
            events = new SortedDictionary<string, SortedDictionary<string, List<MyTime>>>();
            var numOfevents = int.Parse(Console.ReadLine());
            for (int i = 0; i < numOfevents; i++)
            {
                var @event = Console.ReadLine();
                var eventDetails = Regex.Match(@event, validEventPattern);
                if (eventDetails.Success)
                {
                    var location = eventDetails.Groups[2].Value;
                    var person = eventDetails.Groups[1].Value;
                    var hours = int.Parse(eventDetails.Groups[3].Value);
                    var minutes = int.Parse(eventDetails.Groups[4].Value);

                    if (!events.ContainsKey(location))
                    {
                        events[location] = new SortedDictionary<string, List<MyTime>>();
                    }

                    if (!events[location].ContainsKey(person))
                    {
                        events[location][person] = new List<MyTime>();
                    }

                    var eventDate = new MyTime(hours, minutes);
                    events[location][person].Add(eventDate);
                }
            }

            var cities = Console.ReadLine().Split(',').OrderBy(c => c).ToArray();
            foreach (var city in cities)
            {
                if (events.ContainsKey(city))
                {
                    Console.WriteLine($"{city}:");
                    var index = 1;
                    foreach (var person in events[city])
                    {
                        Console.WriteLine(
                            "{0}. {1} -> {2}",
                            index++,
                            person.Key,
                            string.Join(", ", person.Value.OrderBy(t => t)));
                    }
                }
            }
        }

        internal class MyTime : IComparable<MyTime>
        {
            public MyTime(int hours, int minutes)
            {
                this.Hours = hours;
                this.Minutes = minutes;
            }

            private int Hours { get; }

            private int Minutes { get; }

            public int CompareTo(MyTime other)
            {
                var comparator = this.Hours.CompareTo(other.Hours);
                if (comparator == 0)
                {
                    comparator = this.Minutes.CompareTo(other.Minutes);
                }

                return comparator;
            }

            public override string ToString()
            {
                return $"{this.Hours:00}:{this.Minutes:00}";
            }
        }
    }
}
