using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jwk6gcFinalProject
{
    class Player
    {
        public String name { get; set; }
        public float balance { get; set;}

        public int cards { get; set; } = 0;
        public int wager { get; set; }

        public Player(String name, float balance, int wager)
        {
            this.name = name;
            this.balance = balance;
            this.wager = wager;
        }
    }
}
