using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xana
{
    class Weapon : Item
    {
        public const double NO_SCALE = 0.0;
        public const double LOW_SCALE = 0.2;
        public const double MED_SCALE = 0.5;
        public const double HIGH_SCALE = 0.8;

        int baseDamage;
        double strScale;
        double dexScale;

        public Weapon(String itemName, int baseDamage, double strScale, double dexScale) : base(itemName, 1)
        {
            this.baseDamage = baseDamage;
            this.strScale = strScale;
            this.dexScale = dexScale;
        }

        public int calculateDamage(int str, int dex)
        {
            int damage = baseDamage;
            if (str >= 0)
            {
                damage += (int)Math.Round(str * strScale);
            }
            if (dex >= 0)
            {
                damage += (int)Math.Round(dex * dexScale);
            }
            return damage;
        }
    }
}
