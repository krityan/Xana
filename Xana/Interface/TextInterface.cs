using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xana
{
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
}
