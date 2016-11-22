using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xana.GameEngine;

namespace Xana.Commands
{
    abstract class Command
    {
        public abstract String execute(Game game, String[] input);
    }
}
