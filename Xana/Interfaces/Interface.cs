using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xana.GameEngine;

namespace Xana.Interfaces
{
    // template for interfaces to play the game via, currently only one implementation but this allows for seamless implementation of others
    abstract class Interface
    {
        protected Game game;

        // creates the interface
        public Interface(GameEngine.Game game)
        {
            this.game = game;
        }

        // sets up and starts the sessions
        public abstract void start();

        // main loop for the interface
        public abstract void loop();

        // closes the session
        public abstract void end();

        // provides system feedback on a new line
        public abstract void systemSays(String output);

        // provides system feedback inline
        public abstract void systemSaysInline(String output);

        // for use with story and events, text is on a new line
        public abstract void narratorSays(String output);

        // for use with story and events, text is inline
        public abstract void narratorSaysInline(String output);

        // takes input from the user, strings are unchanged
        public abstract String[] takeInput();

        // takes commands from the user, string[] returned is the command to execute
        public abstract String[] takeCommand();
    }
}
