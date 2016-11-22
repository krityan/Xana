using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xana.GameEngine;

namespace Xana.Commands
{
    class MoveCommand : Command
    {
        public override String execute(Game game, String[] input)
        {
            if (input.Length < 2)
            {
                return "Please provide a direction to move";
            }
            else
            {
                switch (input[1])
                {
                    case "north":
                        if (game.getCurrentLevel().getCurrentRoom().getNorth() != null)
                        {
                            game.getCurrentLevel().setCurrentRoom(game.getCurrentLevel().getCurrentRoom().getNorth());
                            return "Moved North";
                        }
                        else
                        {
                            return "There is no room to the North";
                        }
                    case "east":
                        if (game.getCurrentLevel().getCurrentRoom().getEast() != null)
                        {
                            game.getCurrentLevel().setCurrentRoom(game.getCurrentLevel().getCurrentRoom().getEast());
                            return "Moved East";
                        }
                        else
                        {
                            return "There is no room to the East";
                        }
                    case "south":
                        if (game.getCurrentLevel().getCurrentRoom().getSouth() != null)
                        {
                            game.getCurrentLevel().setCurrentRoom(game.getCurrentLevel().getCurrentRoom().getSouth());
                            return "Moved South";
                        }
                        else
                        {
                            return "There is no room to the South";
                        }
                    case "west":
                        if (game.getCurrentLevel().getCurrentRoom().getWest() != null)
                        {
                            game.getCurrentLevel().setCurrentRoom(game.getCurrentLevel().getCurrentRoom().getWest());
                            return "Moved West";
                        }
                        else
                        {
                            return "There is no room to the West";
                        }
                    default:
                        return input[1] + "is not a valid direction";
                }
            }
        }
    }
}
