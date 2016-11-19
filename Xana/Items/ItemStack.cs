using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xana.Items
{
    class ItemStack
    {
        // the item stored in the stack
        Item item;
        int amount;
        int max;

        // constructor for adding a single item with default max size
        public ItemStack(Item item)
        {
            this.item = item;
            amount = 1;
            max = item.getMaxStack();
        }

        // constructor for adding multiple of the item with default max size
        public ItemStack(Item item, int amount)
        {
            this.item = item;
            this.amount = amount;
            max = item.getMaxStack();
        }

        // attempts to add items to stack, returns true if successful, false if stack would overflow
        public bool addToStack(int addAmount)
        {
            if (addAmount < 0)
            {
                return false; //can't add negative amounts
            }
            if (amount + addAmount <= max)
            {
                amount += addAmount;
                return true;
            }
            else
            {
                return false;
            }
        }

        // attempts to "take" items from the stack, returns true if successful, false is not enough in stack
        public bool removeFromStack(int removeAmount)
        {
            if (removeAmount <= amount)
            {
                amount -= removeAmount;
                return true;
            }
            else
            {
                return false;
            }
        }

        // returns true if the amount stored is 0
        public bool isEmpty()
        {
            if (amount == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // returns true if the amount stored is equal to max amount
        public bool isFull()
        {
            if (amount == max)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // returns a reference to the exact item stored in the stack, useful for non generic items such as weapons, and for using an item
        public Item getItem()
        {
            return item;
        }

        // returns the current number of items in the stack
        public int getAmount()
        {
            return amount;
        }
    }
}
