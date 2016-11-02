using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xana
{
    class Program
    {
        static Game game;

        static void Main(string[] args)
        {
            //grabbing previous console color 
            ConsoleColor old = Console.ForegroundColor;

            game = new Game();
            game.introduction();

            //setting the console color to what it was before the program was run
            Console.ForegroundColor = old;
        }
    }

    class Game
    {
        ConsoleColor narratorColor = ConsoleColor.DarkCyan;
        ConsoleColor systemColor = ConsoleColor.Gray;
        ConsoleColor playerColor = ConsoleColor.DarkMagenta;
        Player mainChar;
        Level currentLevel;

        public Game()
        {
            mainChar = new Player(10, 3, 3, 1); // player starts with 10 health, 3 str, 3 dex and 1 armour.
        }

        public void loop()
        {
            narratorSays("Welcome to level " + currentLevel.getLevelNo() + ": " + currentLevel.getName());
            bool levelComplete = true; // false when level is finished
            while (!levelComplete)
            {

            }
        }

        public void introduction()
        {
            narratorSays("Welcome to Xana.\nThis is a story of a 17 year old boy setting out on an\nadventure to find out what destiny awaitshim.\nHis name is...");
            bool named = false;
            while (!named)
            {
                systemSaysInline("Enter Name: ");
                String[] input = takeUserInput();
                if (input.Length > 0 && input.Length < 2)
                {
                    Player.name = input[0];
                    named = true;
                }
                else
                {
                    systemSays("Name must be a single word.");
                }
            }
            narratorSays(mainChar.getName() + ", his home was attacked recently by bandits, killing his monther.\nHis Father had gone off to the city with his older brother, both were\nsoldiers in the army, " + mainChar.getName() + " had been training to also join but was too young.\nNow it's time for him to make his way to the city of Xanaric to find out\nwhat future had in store for him.");
            systemSays("Press any key to continue...");
            currentLevel = LevelSetup.levelOne();
            Console.ReadKey();
            loop();
        }

        public void systemSays(String output)
        {
            Console.ForegroundColor = systemColor;
            Console.WriteLine(output);
            Console.ForegroundColor = playerColor;
        }

        public void systemSaysInline(String output)
        {
            Console.ForegroundColor = systemColor;
            Console.Write(output);
            Console.ForegroundColor = playerColor;
        }

        public void narratorSays(String output)
        {
            Console.ForegroundColor = narratorColor;
            Console.WriteLine(output);
            Console.ForegroundColor = playerColor;
        }

        public void narratorSaysInline(String output)
        {
            Console.ForegroundColor = narratorColor;
            Console.Write(output);
            Console.ForegroundColor = playerColor;
        }

        public String[] takeUserInput()
        {
            // takes the user's input and stores it in a string, then converts it to an array of words
            String commandString = Console.ReadLine();
            String[] commands = commandString.Split((string[])null, StringSplitOptions.RemoveEmptyEntries);
            return commands;
        }
    }

    abstract class Unit
    {
        private int baseHealth;
        private int baseStrength;
        private int baseDexterity;
        private int baseArmour;

        Weapon mainWeapon;

        protected Unit(int health, int strength, int dexterity, int armour)
        {
            baseHealth = health;
            baseStrength = strength;
            baseDexterity = dexterity;
            baseArmour = armour;
            mainWeapon = null;
        }

        public void setMainWeapon(Weapon wep)
        {
            mainWeapon = wep;
        }

        public void removeMainWeapon()
        {
            mainWeapon = null;
        }
    }

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

    class Item
    {
        String itemName;
        String itemDescription;
        int maxStack;
        int currentStack;

        public Item(String itemName, int maxStack)
        {
            this.itemName = itemName;
            this.maxStack = maxStack;
            currentStack = 1;
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

    class Weapon : Item
    {
        public const double NO_SCALE = 0.0;
        public const double LOW_SCALE = 0.2;
        public const double MED_SCALE = 0.5;
        public const double HIGH_SCALE = 0.8;

        int baseDamage;
        double strScale;
        double dexScale;

        public Weapon(String itemName, int baseDamage, double strScale, double dexScale) : base(itemName, 1)
        {
            this.baseDamage = baseDamage;
            this.strScale = strScale;
            this.dexScale = dexScale;
        }

        public int calculateDamage(int str, int dex)
        {
            int damage = baseDamage;
            if (str >= 0)
            {
                damage += (int)Math.Round(str * strScale);
            }
            if (dex >= 0)
            {
                damage += (int)Math.Round(dex * dexScale);
            }
            return damage;
        }
    }

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
        }

        public void setRooms(List<Room> rooms)
        {
            this.rooms = rooms;
        }

        public void setCurrentRoom(Room room)
        {
            currentRoom = room;
        }

        public bool moveNorth()
        {
            if (currentRoom.getNorth() != null)
            {
                currentRoom = currentRoom.getNorth();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool moveEast()
        {
            if (currentRoom.getEast() != null)
            {
                currentRoom = currentRoom.getEast();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool moveSouth()
        {
            if (currentRoom.getSouth() != null)
            {
                currentRoom = currentRoom.getSouth();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool moveWest()
        {
            if (currentRoom.getWest() != null)
            {
                currentRoom = currentRoom.getWest();
                return true;
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

    class Room
    {
        String name;
        Room northRoom;
        Room eastRoom;
        Room southRoom;
        Room westRoom;

        public Room(String name)
        {
            this.name = name;
            northRoom = null;
            southRoom = null;
            eastRoom = null;
            westRoom = null;
        }

        public void setRooms(Room north, Room east, Room south, Room west)
        {
            northRoom = north;
            eastRoom = east;
            southRoom = south;
            westRoom = west;
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

    abstract class Command
    {
        public abstract bool execute();
    }

    class MoveCommand : Command
    {
        public override bool execute()
        {
            return true;
        }
    }

    // class for loading the levels used in the game
    class LevelSetup
    {
        public static Level levelOne()
        {
            Level one = new Level(1, "Red Forest");

            // setting the story for the level.
            one.setStartStory("The Red Forest is a calm place, " );

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
            rooms[14].setRooms(null, rooms[15], null, rooms[13]);
            rooms[15].setRooms(null, null, rooms[14], null);

            return one;
        }
    }
}
