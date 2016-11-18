using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xana
{
    class Item
    {
        int ID;
        String itemName;
        String itemDescription;
        int maxStack;
        int currentStack;

        public Item(String itemName, int maxStack)
        {
            this.itemName = itemName;
            this.maxStack = maxStack;
            currentStack = 1;
            setID();
        }

        private void setID()
        {
            ID = 0;
        }

        public int getID()
        {
            return ID;
        }

        public int getMaxStack()
        {
            return maxStack;
        }

        public void setDescription(String description)
        {
            itemDescription = description;
        }

        public String getName()
        {
            return itemName;
        }

        public bool addItem()
        {
            if (currentStack < maxStack)
            {
                currentStack++;
                return true;
            }
            else
            {
                return false;
            }
        }

        public int addItem(int num)
        {
            if (currentStack + num <= maxStack)
            {
                currentStack += num;
                return 0;
            }
            else
            {
                num = maxStack - currentStack;
                currentStack = maxStack;
                return num;
            }
        }

        public bool removeItem()
        {
            if (currentStack > 0)
            {
                currentStack--;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool removeItem(int num)
        {
            if (currentStack >= num)
            {
                currentStack -= num;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
