using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjucOchPolis_13
{
    public class Thief : Person
    {
        public Thief(int id, int x, int y) : base(id, x, y) { }

        public override void Move(int worldWidth, int worldHeight, List<Person> people, EventLog eventLog)
        {
            X = MoveLeft(X, worldWidth); // Rör sig åt vänster

            // Kolla för rån
            foreach (var other in people)
            {
                if (other is Citizen citizen && citizen.X == X && citizen.Y == Y)
                {
                    Random rand = new Random();
                    if (citizen.Inventory.Count > 0) // Kontrollera att medborgaren har föremål
                    {
                        int itemIndex = rand.Next(citizen.Inventory.Count);
                        string stolenItem = citizen.Inventory[itemIndex];
                        citizen.Inventory.RemoveAt(itemIndex);
                        Inventory.Add(stolenItem);
                        eventLog.AddEvent($"Tjuv {Id} rånade medborgare {citizen.Id} och stal {stolenItem}.");
                    }
                }
            }
        }

        private int MoveLeft(int x, int worldWidth)
        {
            x -= 1; // Rör sig åt vänster
            return WrapAround(x, worldWidth);
        }

        private int WrapAround(int position, int limit)
        {
            if (position < 0)
            {
                return limit - 1; // Wrap-around logik till höger
            }
            else if (position >= limit)
            {
                return 0; // Wrap-around logik till vänster
            }
            return position; // Ingen förändring
        }
    }
}



