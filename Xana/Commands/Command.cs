using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xana
{
    abstract class Command
    {
        public abstract bool execute(Level level, String[] input);
    }
}
