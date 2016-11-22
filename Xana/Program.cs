using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xana.GameEngine;
using Xana.Interfaces;
using Xana.Items;

namespace Xana
{
    class Program
    {
        // game and interface to use 
        static GameEngine.Game game;
        static Interface UI;

        // start point
        static void Main(string[] args)
        {
            //grabbing previous console color 
            ConsoleColor old = Console.ForegroundColor;

            // initialises the game and chosen interface (currently only a text based interface)
            game = new Game();
            UI = new TextInterface(game);
            UI.start();

            //setting the console color to what it was before the program was run
            Console.ForegroundColor = old;
        }
    } 
}
