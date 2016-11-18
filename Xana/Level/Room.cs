using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xana
{
    class Room
    {
        String name;
        bool finalRoom;
        Room northRoom;
        Room eastRoom;
        Room southRoom;
        Room westRoom;

        public Room(String name)
        {
            this.name = name;
            finalRoom = false;
            northRoom = null;
            southRoom = null;
            eastRoom = null;
            westRoom = null;
        }

        public String getName()
        {
            return name;
        }

        public void setRooms(Room north, Room east, Room south, Room west)
        {
            northRoom = north;
            eastRoom = east;
            southRoom = south;
            westRoom = west;
        }

        public String getDirections()
        {
            String dir = "";
            if (northRoom != null)
            {
                if (dir.Equals(""))
                {
                    dir = dir + "north";
                }
                else
                {
                    dir = dir + "\nnorth";
                }
            }
            if (eastRoom != null)
            {
                if (dir.Equals(""))
                {
                    dir = dir + "east";
                }
                else
                {
                    dir = dir + "\neast";
                }
            }
            if (southRoom != null)
            {
                if (dir.Equals(""))
                {
                    dir = dir + "south";
                }
                else
                {
                    dir = dir + "\nsouth";
                }
            }
            if (westRoom != null)
            {
                if (dir.Equals(""))
                {
                    dir = dir + "west";
                }
                else
                {
                    dir = dir + "\nwest";
                }
            }
            return dir;
        }

        public void setFinal()
        {
            finalRoom = true;
        }

        public bool isFinal()
        {
            return finalRoom;
        }

        public void setNorth(Room room)
        {
            northRoom = room;
        }

        public void setEast(Room room)
        {
            eastRoom = room;
        }

        public void setSouth(Room room)
        {
            southRoom = room;
        }

        public void setWest(Room room)
        {
            westRoom = room;
        }

        public Room getNorth()
        {
            return northRoom;
        }

        public Room getEast()
        {
            return eastRoom;
        }

        public Room getSouth()
        {
            return southRoom;
        }

        public Room getWest()
        {
            return westRoom;
        }
    }
}
