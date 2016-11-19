using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xana.Items.Weapons
{
    class TrainingSword : Weapon
    {
        public TrainingSword() : base()
        {
            // Item variables
            maxStack = 1;
            ID = 1001;
            name = "Training Sword";
            description = "A wooden sword used for training and sparring.";

            // Weapon variables;
            baseDamage = 1;
            strScale = LOW_SCALE;
            dexScale = NO_SCALE;
        }
    }
}
