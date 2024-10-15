using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjucOchPolis_13
{
        public class EventLog
        {
            private List<string> events;

            public EventLog()
            {
                events = new List<string>();
            }

            public void AddEvent(string gameEvent)
            {
                events.Add(gameEvent);
            }

            public void PrintLog()
            {
                foreach (var gameEvent in events)
                {
                    Console.WriteLine(gameEvent);
                }
            }
        }
    }

