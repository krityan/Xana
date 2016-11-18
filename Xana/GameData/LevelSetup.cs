using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xana
{ 
    // class for loading the levels used in the game
    class LevelSetup
    {
        public static Level levelOne(Player mainChar)
        {
            Level one = new Level(1, "Red Forest");

            // creating the list of rooms
            List<Room> rooms = new List<Room>(16);
            rooms.Add(new Room("Entrance")); // room 0
            rooms.Add(new Room("Central Forest")); // room 1
            rooms.Add(new Room("Central Forest")); // room 2
            rooms.Add(new Room("West Forest")); // room 3
            rooms.Add(new Room("East Forest")); // room 4
            rooms.Add(new Room("East Forest")); // room 5
            rooms.Add(new Room("East Clearing")); // room 6
            rooms.Add(new Room("East Forest")); // room 7
            rooms.Add(new Room("East Forest")); // room 8
            rooms.Add(new Room("Hidden Base")); // room 9
            rooms.Add(new Room("West Forest")); // room 10
            rooms.Add(new Room("West Clearing")); // room 11
            rooms.Add(new Room("West Forest")); // room 12
            rooms.Add(new Room("West Forest")); // room 13
            rooms.Add(new Room("Central Forest")); // room 14
            rooms.Add(new Room("Exit")); // room 15

            // setting up the room connections
            rooms[0].setRooms(rooms[1], null, null, null);
            rooms[1].setRooms(rooms[2], null, rooms[0], null);
            rooms[2].setRooms(null, rooms[4], rooms[1], rooms[3]);
            rooms[3].setRooms(rooms[10], rooms[2], null, null);
            rooms[4].setRooms(rooms[7], rooms[5], null, rooms[2]);
            rooms[5].setRooms(null, rooms[6], null, rooms[4]);
            rooms[6].setRooms(null, null, null, rooms[5]);
            rooms[7].setRooms(rooms[8], null, rooms[4], null);
            rooms[8].setRooms(null, rooms[9], rooms[7], null);
            rooms[9].setRooms(null, null, null, rooms[8]);
            rooms[10].setRooms(rooms[12], null, rooms[3], rooms[11]);
            rooms[11].setRooms(null, rooms[10], null, null);
            rooms[12].setRooms(rooms[13], null, rooms[10], null);
            rooms[13].setRooms(null, rooms[14], rooms[12], null);
            rooms[14].setRooms(rooms[15], null, null, rooms[13]);
            rooms[15].setRooms(null, null, rooms[14], null);

            // setting final room
            rooms[15].setFinal();

            // setting up the commands for the level
            one.addCommand("move", new MoveCommand());

            // setting up the story for the start of the level
            one.setStartStory("The Red Forest is a calm place, it's where " + mainChar.getName() + " spent his childhood, he had a small camp on the west side of the forest with some gear he was hiding.\nIt may prove useful, as he doesn't plan to return here for a while.\n");

            // setting the starting room
            one.setCurrentRoom(rooms[0]);

            // giving level the reference to the list of rooms
            one.setRooms(rooms);

            return one;
        }
    }
}
