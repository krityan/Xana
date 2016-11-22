using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xana.GameEngine;

namespace Xana.Interfaces
{
    // text based interface using user text to command the player in the game
    class TextInterface : Interface
    {
        // colours for console text to distinquish between system, narrator and player input
        ConsoleColor narratorColor;
        ConsoleColor systemColor;
        ConsoleColor playerColor;

        // initialises the console colours
        public TextInterface(Game game) : base(game)
        {
            narratorColor = ConsoleColor.DarkCyan;
            systemColor = ConsoleColor.Gray;
            playerColor = ConsoleColor.DarkMagenta;
        }

        // sets up and starts the sessions
        public override void start()
        {
            narratorSays("Welcome to Xana.\nThis is a story of a 17 year old boy setting out on an\nadventure to find out what destiny awaits him.\nHis name is...");

            // allows player to set their own name for the main character
            bool named = false;
            while (!named)
            {
                systemSaysInline("\nEnter Name: ");
                String[] input = takeInput();
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
            narratorSays("\n" + game.getPlayer().getName() + ", his home was attacked recently by bandits, who killed his monther.\nHis father had gone off to the city with his older brother, both were\nsoldiers in the army, " + game.getPlayer().getName() + " had been training to also join but was too young.\nNow it's time for him to make his way to the city of Xanaric to find out\nwhat future had in store for him.\n");
            systemSays("Press any key to continue...\n");
            Console.ReadKey(); // waits for user input (once they finish reading)
            loop();
        }

        // main loop for the interface
        public override void loop()
        {

        }

        // closes the session
        public override void end()
        {

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
}
