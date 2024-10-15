using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TjucOchPolis_13
{
    public class World
    {
        private int width;
        private int height;
        private List<Person> people;
        private EventLog eventLog;

        public World(int width, int height)
        {
            this.width = width;
            this.height = height;
            people = new List<Person>();
            eventLog = new EventLog();
            InitializePeople();
        }

        private void InitializePeople()
        {
            // Skapa 20 tjuvar
            for (int i = 0; i < 20; i++)
            {
                people.Add(new Thief(i, new Random().Next(0, width), new Random().Next(0, height)));
            }

            // Skapa 30 medborgare
            for (int i = 0; i < 30; i++)
            {
                people.Add(new Citizen(i + 20, new Random().Next(0, width), new Random().Next(0, height)));
            }

            // Skapa 10 poliser
            for (int i = 0; i < 10; i++)
            {
                people.Add(new Police(i + 50, new Random().Next(0, width), new Random().Next(0, height)));
            }
        }

        public void StartGame()
        {
            ConsoleKeyInfo keyInfo;
            do
            {
                MovePeople();
                DisplayWorld(); // Visa världen efter varje rörelse

                // Pausa spelet i 2 sekunder
                Thread.Sleep(2000);

                // Kontrollera om spelaren trycker på 'Q' för att avsluta
                if (Console.KeyAvailable)
                {
                    keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Q)
                    {
                        EndGame();
                        return;
                    }
                }

            } while (true); // Oändlig loop
        }

        private void MovePeople()
        {
            for (int i = 0; i < people.Count; i++)
            {
                people[i].Move(width, height, people, eventLog);
            }

            // Kontrollera för rån och arresteringar efter att alla har rört sig
            CheckForRobberiesAndArrests();
        }

        private void CheckForRobberiesAndArrests() // 
        {
            for (int i = 0; i < people.Count; i++)
            {
                for (int j = 0; j < people.Count; j++)
                {
                    if (i != j) // Undvik att jämföra med sig själv
                    {
                        // Kolla om en tjuv rånar en medborgare
                        if (people[i] is Thief thief && people[j] is Citizen citizen && thief.X == citizen.X && thief.Y == citizen.Y)
                        {
                            Random rand = new Random();
                            if (citizen.Inventory.Count > 0)
                            {
                                int itemIndex = rand.Next(citizen.Inventory.Count);
                                string stolenItem = citizen.Inventory[itemIndex];
                                citizen.Inventory.RemoveAt(itemIndex);
                                thief.Inventory.Add(stolenItem);
                                Console.WriteLine($"Tjuv {thief.Id} rånade medborgare {citizen.Id} och stal {stolenItem}.");
                                Thread.Sleep(5000);
                            }
                        }
                        // Kolla om en polis arresterar en tjuv
                        else if (people[i] is Police police && people[j] is Thief arrestedThief && police.X == arrestedThief.X && police.Y == arrestedThief.Y)
                        {
                            // Flytta stulna föremål från tjuven till polisen
                            foreach (var item in arrestedThief.Inventory)
                            {
                                police.Inventory.Add(item);
                            }
                            arrestedThief.Inventory.Clear();
                            Console.WriteLine($"Polis {police.Id} arresterade tjuv {arrestedThief.Id}.");
                            Thread.Sleep(5000);
                        }
                    }
                }
            }
        }

        private void DisplayWorld()
        {
            Console.Clear(); // Rensa konsolen
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    bool characterFound = false;

                    foreach (var person in people)
                    {
                        if (person.X == x && person.Y == y)
                        {
                            // Visa olika tecken för olika typer av karaktärer
                            if (person is Thief)
                            {
                                Console.Write("T "); // Tjuv
                            }
                            else if (person is Citizen)
                            {
                                Console.Write("C "); // Medborgare
                            }
                            else if (person is Police)
                            {
                                Console.Write("P "); // Polis
                            }
                            characterFound = true;
                            break;
                        }
                    }

                    if (!characterFound)
                    {
                        Console.Write("  "); // Tom plats
                    }
                }
                Console.WriteLine(); // Ny rad
            }
            Console.WriteLine("Tryck 'Q' för att avsluta spelet."); // Anvisning för att avsluta
        }

        private void EndGame()
        {
            Console.WriteLine("Spelet är avslutat. Händelser:");
            eventLog.PrintLog();
        }
    }
}


