using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjucOchPolis_13
{ 
    public class Citizen : Person
    {
        public Citizen(int id, int x, int y) : base(id, x, y) { }

        public override void Move(int worldWidth, int worldHeight, List<Person> people, EventLog eventLog)
        {
            Y = MoveDown(Y, worldHeight); // Rör sig nedåt
        }

        private int MoveDown(int y, int worldHeight)
        {
            y += 1; // Rör sig nedåt
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

