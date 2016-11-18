using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xana
{
    class Inventory
    {
        const int STARTING_SIZE = 10;

        List<ItemStack> inventory;
        int maxSize;
        int currentSize;

        public Inventory()
        {
            inventory = new List<ItemStack>();
            maxSize = STARTING_SIZE;
            currentSize = 0;
        }

        public bool addItem(Item item)
        {
            return addItem(new ItemStack(item));
        }

        public bool addItem(ItemStack items)
        {
            ItemStack existingStack = getItemStack(items.getItem());
            if (existingStack == null)
            {
                if (currentSize < maxSize)
                {
                    inventory.Add(new ItemStack(items.getItem(), items.getAmount()));
                    currentSize++;
                    items.removeFromStack(items.getAmount());
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (existingStack.addToStack(items.getAmount()))
                {
                    items.removeFromStack(items.getAmount());
                    return true;
                }
                else
                {
                    if (currentSize < maxSize)
                    {
                        int remainingAmount = existingStack.getItem().getMaxStack() - existingStack.getAmount();
                        existingStack.addToStack(remainingAmount);
                        inventory.Add(new ItemStack(items.getItem(), items.getAmount() - remainingAmount));
                        currentSize++;
                        items.removeFromStack(items.getAmount());
                        return true;
                    }
                    else
                    {
                        int remainingAmount = existingStack.getItem().getMaxStack() - existingStack.getAmount();
                        existingStack.addToStack(remainingAmount);
                        items.removeFromStack(remainingAmount);
                        return true;
                    }
                }
            }
        }

        // returns the ItemStack from inventory based on the item provided, it uses the Item's unique ID to identify the item, returns null if the item is not in the inventory
        public ItemStack getItemStack(Item item)
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].getItem().getID() == item.getID())
                {
                    return inventory[i];
                }
            }
            return null;
        }

        public String allItemsString()
        {
            String result = "Inventory : " + currentSize + "/" + maxSize + "\n";
            for (int i = 0; i < inventory.Count; i++)
            {
                result += inventory[i].getItem().getName() + ": " + inventory[i].getAmount() + "/" + inventory[i].getItem().getMaxStack() + "\n";
            }
            return result;
        }
    }
}
