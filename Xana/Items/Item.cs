using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xana.Items
{
    abstract class Item
    {
        protected int maxStack;
        protected int ID;
        protected String name;
        protected String description;
        protected String useName;

        public Item()
        {
            maxStack = 99;
            ID = 0;
            name = "";
            description = "";
            useName = "Use";
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

        public String getUseName()
        {
            return useName;
        }

        abstract public String use();
    }
}
