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
        Dictionary<String, Command> commands;

        public Level(int levelNumber, String levelName)
        {
            this.levelNumber = levelNumber;
            this.levelName = levelName;
            levelStartStory = "Not implemented";
            levelEndStory = "Not implemented.";
            commands = new Dictionary<string, Command>();
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

        public bool addCommand(String commandName, Command command)
        {
            if (commands.ContainsKey(commandName))
            {
                return false;
            }
            else
            {
                commands.Add(commandName, command);
                return true;
            }
        }

        public Dictionary<String, Command> getCommands()
        {
            return commands;
        }

        public bool executeCommand(String[] input)
        {
            if (commands.ContainsKey(input[0]))
            {
                Command command = commands[input[0]];
                return command.execute(this, input);
            }
            else
            {
                return false;
            }
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
