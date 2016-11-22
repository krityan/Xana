using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xana.Commands;

namespace Xana.Maps
{
    class Level
    {
        int levelNumber;
        String levelName;
        String levelStartStory;
        String levelEndStory;
        List<Room> rooms;
        Room currentRoom;

        public Level(int levelNumber, String levelName)
        {
            this.levelNumber = levelNumber;
            this.levelName = levelName;
            levelStartStory = "Not implemented";
            levelEndStory = "Not implemented.";
        }

        public void setRooms(List<Room> rooms)
        {
            this.rooms = rooms;
        }

        public void setCurrentRoom(Room room)
        {
            currentRoom = room;
        }

        public Room getCurrentRoom()
        {
            return currentRoom;
        }

        public String getName()
        {
            return levelName;
        }

        public int getLevelNo()
        {
            return levelNumber;
        }

        public String getStartStory()
        {
            return levelStartStory;
        }

        public void setStartStory(String story)
        {
            levelStartStory = story;
        }

        public String getEndStory()
        {
            return levelEndStory;
        }

        public void setEndStory(String story)
        {
            levelEndStory = story;
        }
    }
}
