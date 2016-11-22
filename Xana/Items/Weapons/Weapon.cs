using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xana.Items.Weapons
{
    abstract class Weapon : Item
    {
        protected const double NO_SCALE = 0.0;
        protected const double LOW_SCALE = 0.5;
        protected const double MED_SCALE = 1.0;
        protected const double HIGH_SCALE = 1.5;

        protected int baseDamage = 0;
        protected double strScale = NO_SCALE;
        protected double dexScale = NO_SCALE;

        public Weapon() : base()
        {
            useName = "Equip";
        }

        public int calculateDamage(int str, int dex)
        {
            double damage = baseDamage;
            if (str >= 0)
            {
                damage += (str * strScale);
            }
            if (dex >= 0)
            {
                damage += (dex * dexScale);
            }
            return (int)Math.Round(damage);
        }
    }
}
