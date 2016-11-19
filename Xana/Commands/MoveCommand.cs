using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xana.Maps;

namespace Xana.Commands
{
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
                switch (input[1])
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
}
