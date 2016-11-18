using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xana
{
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
}
