using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjucOchPolis_13
    {
        public class Police : Person
        {
            public Police(int id, int x, int y) : base(id, x, y) { }

            public override void Move(int worldWidth, int worldHeight, List<Person> people, EventLog eventLog)
            {
                Y = MoveUp(Y, worldHeight); // Rör sig uppåt

                // Kolla för arrestering
                foreach (var other in people)
                {
                    if (other is Thief thief && thief.X == X && thief.Y == Y)
                    {
                        // Flytta stulna föremål från tjuven till polisen
                        foreach (var item in thief.Inventory)
                        {
                            Inventory.Add(item);
                        }
                        thief.Inventory.Clear();
                        eventLog.AddEvent($"Polis {Id} + {Name} arresterade tjuv {thief.Id}.");
                    }
                }
            }

            private int MoveUp(int y, int worldHeight)
            {
                y -= 1; // Rör sig uppåt
                return WrapAround(y, worldHeight);
            }

            private int WrapAround(int position, int limit)
            {
                if (position < 0)
                {
                    return limit - 1;
                }
                else if (position >= limit)
                {
                    return 0;
                }
                return position;
            }
        }
    }


