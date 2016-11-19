using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xana.Items
{
    abstract class Item
    {
        protected int maxStack = 99;
        protected int ID = 0;
        protected String name = "";
        protected String description = "";

        public Item()
        {

        }

        public int getID()
        {
            return ID;
        }

        public int getMaxStack()
        {
            return maxStack;
        }

        public String getDescription()
        {
            return description;
        }        

        public String getName()
        {
            return name;
        }
    }
}
