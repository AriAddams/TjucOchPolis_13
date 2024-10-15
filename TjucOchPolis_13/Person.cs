using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjucOchPolis_13
{
    
        public abstract class Person
        {
            private static readonly List<string> availableNames = new List<string>
        {
            "Lennart", "Eva", "Fredrik", "Anna", "Björn", "Carina", "Oskar", "Sofia", "Per", "Elin",
            "Johan", "Sara", "Andreas", "Maria", "Peter", "Linda", "Mats", "Camilla", "Robert", "Karin",
            "Anton", "Beatrice", "Claes", "Denise", "Elias", "Frida", "Gabriel", "Hanna", "Isak", "Jenny",
            "Karl", "Lina", "Markus", "Nina", "Olof", "Paula", "Rikard", "Sanna", "Tobias", "Ulrika"
        };

            private static Random rand = new Random();
            private static List<string> usedNames = new List<string>();

            public int Id { get; }
            public int X { get; set; }
            public int Y { get; set; }
            public string Name { get; set; }
            public List<string> Inventory { get; set; }

            protected Person(int id, int x, int y)
            {
                Id = id;
                X = x;
                Y = y;
                Name = GetRandomName();
                Inventory = new List<string> { "Mobil", "Plånbok", "Klocka", "Nycklar" }; // Initiera med exempelobjekt
            }

            // Slumpmässig namnval från listan
            private string GetRandomName()
            {
                if (availableNames.Count == 0)
                    throw new InvalidOperationException("Inga fler namn tillgängliga.");

                int index = rand.Next(availableNames.Count);
                string name = availableNames[index];
                availableNames.RemoveAt(index); // Ta bort det tilldelade namnet för att undvika duplicat
                usedNames.Add(name); // Lägg till i listan med använda namn
                return name;
            }

            public abstract void Move(int worldWidth, int worldHeight, List<Person> people, EventLog eventLog);

            protected void WrapAround(ref int position, int limit)
            {
                if (position < 0)
                {
                    position = limit - 1;
                }
                else if (position >= limit)
                {
                    position = 0;
                }
            }
        }
    }




