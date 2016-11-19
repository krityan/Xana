using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xana.Items;

namespace Xana
{
    class Player : Unit
    {
        public static String name;
        private int inventorySize;
        private Inventory inventory;

        public Player(int health, int strength, int dexteirty, int armour) : base(health, strength, dexteirty, armour)
        {
            inventorySize = 10;
            inventory = new Inventory();
        }

        public static void setName(String newName)
        {
            name = newName;
        }

        public String getName()
        {
            return name;
        }

        public Inventory getInv()
        {
            return inventory;
        }
    }
}
