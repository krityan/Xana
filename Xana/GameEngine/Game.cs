using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xana.Maps;
using Xana.GameData;
using Xana.Commands;

namespace Xana.GameEngine
{
    // the game logic
    class Game
    {
        // the character for the user
        Player player;
        // the level the user is currently on
        Level currentLevel;
        // the commands for the game
        Dictionary<String, Command> commands;

        int nextLevel;  

        // sets up the main character
        public Game()
        {
            player = new Player(10, 3, 3, 1); // player starts with 10 health, 3 str, 3 dex and 1 armour.
            nextLevel = 1;
            commands = new Dictionary<string, Command>();
            setInitialCommands();
        }

        public void setInitialCommands()
        {
            commands.Add("move", new MoveCommand());
        }

        public Player getPlayer()
        {
            return player;
        }

        public Dictionary<String, Command> getCommands()
        {
            return commands;
        }

        // the main loop for the game, finishes when game completes
        //public void loop()
        //{
        //    // this method will loop once multiple levels are added

        //    // initial sequence for the level

        //    UI.systemSays("You are in: " + currentLevel.getCurrentRoom().getName());
        //    Room currentRoom = null;
        //    bool levelComplete = false;

        //    // the loop for the level
        //    while (!levelComplete)
        //    {
        //        // if the room has changed, or this is the first room of the level, prints available paths and sets currentRoom to the new room
        //        if (currentRoom == null || currentRoom != currentLevel.getCurrentRoom())
        //        {
        //            UI.systemSays("Available paths are:");
        //            UI.systemSays(currentLevel.getCurrentRoom().getDirections());
        //            currentRoom = currentLevel.getCurrentRoom();

        //            if (currentRoom.isFinal())
        //            {
        //                levelComplete = true;
        //                break; // reached end of level, break out of level loop
        //            }
        //        }

        //        parseCommand();
        //    } // end of level loop

        //    // ending sequence for the level
        //    UI.narratorSays("Reached the end of " + currentLevel.getLevelNo() + ": " + currentLevel.getName() + "\n");
        //    UI.narratorSays(currentLevel.getEndStory());
        //}

        // takes user input and executes functions based on it.
        //public String parseCommand(String[] inptut)
        //{

        //        // checks if the command is valid for current level (later levels will have new commands added)
        //        if (input.Length > 0 && currentLevel.getCommands().ContainsKey(input[0]))
        //        {
        //            // if the command successfully executes, end loop
        //            if (currentLevel.executeCommand(input))
        //            {
        //                commandSuccess = true;
        //            }

        //            // provide feedback based on the result of the command
        //            switch (input[0])
        //            {
        //                case "move":
        //                    if (commandSuccess) // 2nd word valid direction
        //                    {
        //                        UI.systemSays("\nMoved to " + currentLevel.getCurrentRoom().getName() + ".");
        //                    }
        //                    else if (input.Length > 1) // 2nd word not valid direction
        //                    {
        //                        UI.systemSays("There is no room to the " + input[1] + ".");
        //                    }
        //                    else // only 1 word provided
        //                    {
        //                        UI.systemSays("Please provide a direction to move.");
        //                    }
        //                    break;
        //            }
        //        }
        //        // if the command isn't valid
        //        else
        //        {
        //            if (input.Length < 1)
        //            {
        //                UI.systemSays("Please provide a command.");
        //            }
        //            else
        //            {
        //                UI.systemSays(input[0] + " is not a valid command.");
        //            }
        //        }
        //    }
        //}

        // checks that a command has been provided, if so it checks if it's a valid command then executes it
        public String parseCommand(String[] input)
        {
            if (input.Length > 0)
            {
                if (commands.ContainsKey(input[0]))
                {
                    return commands[input[0]].execute(this, input);
                }
                else
                {
                    return input[0] + " is not a valid command.";
                }
            }          
            else
            {
                return "Please provide a command.";
            }
        }
        
        // the introduction sequence for the story
        //public void introduction()
        //{
        //    // allows player to set their own name for the main character
        //    bool named = false;
        //    while (!named)
        //    {
        //        UI.systemSaysInline("\nEnter Name: ");
        //        String[] input = UI.takeInput();
        //        if (input.Length > 0 && input.Length < 2)
        //        {
        //            Player.name = input[0];
        //            named = true;
        //        }
        //        else
        //        {
        //            UI.systemSays("Name must be a single word.");
        //        }
        //    }
        //    UI.narratorSays("\n" + player.getName() + ", his home was attacked recently by bandits, who killed his monther.\nHis father had gone off to the city with his older brother, both were\nsoldiers in the army, " + mainChar.getName() + " had been training to also join but was too young.\nNow it's time for him to make his way to the city of Xanaric to find out\nwhat future had in store for him.\n");
        //    UI.systemSays("Press any key to continue...\n");
        //    currentLevel = LevelSetup.levelOne(player); // sets up first level while user is reading story.
        //    Console.ReadKey(); // waits for user input (once they finish reading)
        //    loop();
        //}

        public Level getCurrentLevel()
        {
            return currentLevel;
        }

        public String setupNextLevel()
        {
            switch (nextLevel)
            {
                case 1:
                    currentLevel = LevelSetup.levelOne(player);
                    nextLevel++;
                    return newLevelString();
                    
                default:
                    return "Hit default in Game.setupNextLevel(), assumed level overflow.";
            }
        }

        public String newLevelString()
        {
            return ("Welcome to level " + currentLevel.getLevelNo() + ": " + currentLevel.getName() + "\n" + currentLevel.getStartStory());
        }

        public String endLevelString()
        {
            return ("End of level " + currentLevel.getLevelNo() + ": " + currentLevel.getName() + '\n' + currentLevel.getEndStory());
        }

    }
}
