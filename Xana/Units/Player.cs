using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xana
{
    class Player : Unit
    {
        public static String name;
        private int inventorySize;
        private List<Item> inventory;

        public Player(int health, int strength, int dexteirty, int armour) : base(health, strength, dexteirty, armour)
        {
            inventorySize = 10;
            inventory = new List<Item>(10);
        }

        public static void setName(String newName)
        {
            name = newName;
        }

        public String getName()
        {
            return name;
        }

        public List<Item> getInv()
        {
            return inventory;
        }

        public bool searchInv(Item item)
        {
            if (inventory.Contains(item))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool removeFromInv(Item item)
        {
            if (inventory.Remove(item))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool removeFromInv(Item item, int num)
        {
            if (inventory.Contains(item))
            {

            }
            return false; // dummy code
        }

        // if the inventory contains the item, it attempts to add one to its stack, if it cannot then it attempts to add a new stack
        // if the inventory doesn't contain the item, it attempts to add the item to the inventory as a new stack
        public bool addToInv(Item item)
        {
            if (inventory.Contains(item))
            {
                Item slot = inventory.Find(x => x.getName() == item.getName());
                if (slot.addItem())
                {
                    return true;
                }
                else
                {
                    if (inventory.Count < inventorySize)
                    {
                        inventory.Add(item);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                if (inventory.Count < inventorySize)
                {
                    inventory.Add(item);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
