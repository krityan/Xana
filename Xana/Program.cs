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
        // game and interface to use 
        static Game game;
        static Interface UI;

        // start point
        static void Main(string[] args)
        {
            // test code
            Inventory inv = new Inventory();
            inv.addItem(new Item("Sword", 1));
            inv.addItem(new Item("Apple", 99));
            inv.addItem(new ItemStack(new Item("Apple", 99), 40));
            inv.addItem(new ItemStack(new Item("Apple", 99), 99));
            Console.WriteLine(inv.allItemsString());
            Console.ReadKey();

            //grabbing previous console color 
            ConsoleColor old = Console.ForegroundColor;

            // initialises the game and chosen interface (currently only a text based interface)
            UI = new TextInterface();
            game = new Game(UI);
            game.introduction();

            //setting the console color to what it was before the program was run
            Console.ForegroundColor = old;
        }
    }

    // template for interfaces to play the game via, currently only one implementation but this allows for seamless implementation of others
    abstract class Interface
    {
        // creates the interface
        public Interface(){}

        // provides system feedback on a new line
        public abstract void systemSays(String output);

        // provides system feedback inline
        public abstract void systemSaysInline(String output);

        // for use with story and events, text is on a new line
        public abstract void narratorSays(String output);

        // for use with story and events, text is inline
        public abstract void narratorSaysInline(String output);

        // takes input from the user, strings are unchanged
        public abstract String[] takeInput();

        // takes commands from the user, string[] returned is the command to execute
        public abstract String[] takeCommand();
    }

    // text based interface using user text to command the player in the game
    class TextInterface : Interface
    {
        // colours for console text to distinquish between system, narrator and player input
        ConsoleColor narratorColor;
        ConsoleColor systemColor;
        ConsoleColor playerColor;

        // initialises the console colours
        public TextInterface()
        {
            narratorColor = ConsoleColor.DarkCyan;
            systemColor = ConsoleColor.Gray;
            playerColor = ConsoleColor.DarkMagenta;
        }

        // prints in the system colour on a new line
        public override void systemSays(String output)
        {
            Console.ForegroundColor = systemColor;
            Console.WriteLine(output);
            Console.ForegroundColor = playerColor;
        }

        // prints in the system colour inline
        public override void systemSaysInline(String output)
        {
            Console.ForegroundColor = systemColor;
            Console.Write(output);
            Console.ForegroundColor = playerColor;
        }

        // prints in the narrator colour on a new line
        public override void narratorSays(String output)
        {
            Console.ForegroundColor = narratorColor;
            Console.WriteLine(output);
            Console.ForegroundColor = playerColor;
        }

        // prints in the narrator colour inline
        public override void narratorSaysInline(String output)
        {
            Console.ForegroundColor = narratorColor;
            Console.Write(output);
            Console.ForegroundColor = playerColor;
        }

        // takes the user's input and stores it in a string, then converts it to an array of words
        public override String[] takeInput()
        {            
            String commandString = Console.ReadLine();
            String[] commands = commandString.Split((string[])null, StringSplitOptions.RemoveEmptyEntries);
            return commands;
        }

        // takes the user's input and stores it in a string, then converts it to lower case and to an array of words
        public override String[] takeCommand()
        {            
            String commandString = Console.ReadLine();
            commandString = commandString.ToLower();
            String[] commands = commandString.Split((string[])null, StringSplitOptions.RemoveEmptyEntries);
            return commands;
        }
    }

    // the game logic
    class Game
    {
        // the character for the user
        Player mainChar;
        // the level the user is currently on
        Level currentLevel;
        // the interface for the game to interact with
        Interface UI;

        // sets up the main character
        public Game(Interface UI)
        {
            mainChar = new Player(10, 3, 3, 1); // player starts with 10 health, 3 str, 3 dex and 1 armour.
            this.UI = UI;
        }

        // the main loop for the game, finishes when game completes
        public void loop()
        {
            // this method will loop once multiple levels are added

            // initial sequence for the level
            UI.narratorSays("Welcome to level " + currentLevel.getLevelNo() + ": " + currentLevel.getName() + "\n");
            UI.narratorSays(currentLevel.getStartStory());
            UI.systemSays("You are in: " + currentLevel.getCurrentRoom().getName());
            Room currentRoom = null;
            bool levelComplete = false;

            // the loop for the level
            while (!levelComplete)
            {
                // if the room has changed, or this is the first room of the level, prints available paths and sets currentRoom to the new room
                if (currentRoom == null || currentRoom != currentLevel.getCurrentRoom())
                {                    
                    UI.systemSays("Available paths are:");
                    UI.systemSays(currentLevel.getCurrentRoom().getDirections());
                    currentRoom = currentLevel.getCurrentRoom();

                    if (currentRoom.isFinal())
                    {
                        levelComplete = true;
                        break; // reached end of level, break out of level loop
                    }
                }
                
                parseCommand();
            } // end of level loop

            // ending sequence for the level
            UI.narratorSays("Reached the end of " + currentLevel.getLevelNo() + ": " + currentLevel.getName() + "\n");
            UI.narratorSays(currentLevel.getEndStory());
        }

        // takes user input and executes functions based on it.
        public void parseCommand()
        {
            // bool to track if the command successfully executes
            bool commandSuccess = false;

            // loops until a valid command is supplied
            while (!commandSuccess)
            {
                // get user's commands
                String[] input = UI.takeCommand();

                // checks if the command is valid for current level (later levels will have new commands added)
                if (input.Length > 0 && currentLevel.getCommands().ContainsKey(input[0]))
                {
                    // if the command successfully executes, end loop
                    if (currentLevel.executeCommand(input))
                    {
                        commandSuccess = true;
                    }

                    // provide feedback based on the result of the command
                    switch (input[0])
                    {
                        case "move": 
                            if (commandSuccess) // 2nd word valid direction
                            {
                                UI.systemSays("\nMoved to " + currentLevel.getCurrentRoom().getName() + ".");
                            }
                            else if (input.Length > 1) // 2nd word not valid direction
                            {
                                UI.systemSays("There is no room to the " + input[1] + ".");
                            }
                            else // only 1 word provided
                            {
                                UI.systemSays("Please provide a direction to move.");
                            }
                            break;
                    }
                }
                // if the command isn't valid
                else
                {
                    if (input.Length < 1)
                    {
                        UI.systemSays("Please provide a command.");
                    }
                    else
                    {
                        UI.systemSays(input[0] + " is not a valid command.");
                    }
                }
            }
        }

        // the introduction sequence for the story
        public void introduction()
        {
            UI.narratorSays("Welcome to Xana.\nThis is a story of a 17 year old boy setting out on an\nadventure to find out what destiny awaits him.\nHis name is...");

            // allows player to set their own name for the main character
            bool named = false;
            while (!named)
            {
                UI.systemSaysInline("\nEnter Name: ");
                String[] input = UI.takeInput();
                if (input.Length > 0 && input.Length < 2)
                {
                    Player.name = input[0];
                    named = true;
                }
                else
                {
                    UI.systemSays("Name must be a single word.");
                }
            }
            UI.narratorSays("\n" + mainChar.getName() + ", his home was attacked recently by bandits, who killed his monther.\nHis father had gone off to the city with his older brother, both were\nsoldiers in the army, " + mainChar.getName() + " had been training to also join but was too young.\nNow it's time for him to make his way to the city of Xanaric to find out\nwhat future had in store for him.\n");
            UI.systemSays("Press any key to continue...\n");
            currentLevel = LevelSetup.levelOne(mainChar); // sets up first level while user is reading story.
            Console.ReadKey(); // waits for user input (once they finish reading)
            loop();
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

    abstract class Command
    {
        public abstract bool execute(Level level, String[] input);
    }

    class MoveCommand : Command
    {
        public override bool execute(Level level, String[] input)
        {
            if (input.Length < 2)
            {
                return false;
            }
            else
            {
                switch(input[1])
                {
                    case "north":
                        if (level.getCurrentRoom().getNorth() != null)
                        {
                            level.setCurrentRoom(level.getCurrentRoom().getNorth());
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    case "east":
                        if (level.getCurrentRoom().getEast() != null)
                        {
                            level.setCurrentRoom(level.getCurrentRoom().getEast());
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    case "south":
                        if (level.getCurrentRoom().getSouth() != null)
                        {
                            level.setCurrentRoom(level.getCurrentRoom().getSouth());
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    case "west":
                        if (level.getCurrentRoom().getWest() != null)
                        {
                            level.setCurrentRoom(level.getCurrentRoom().getWest());
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                }
                return false;
            }
        }
    }

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
