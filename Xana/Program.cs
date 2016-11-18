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
}
