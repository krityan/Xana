using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xana.Items.Weapons;

namespace Xana
{
    abstract class Unit
    {
        private int baseHealth;
        private int baseStrength;
        private int baseDexterity;
        private int baseArmour;

        Weapon mainWeapon;

        protected Unit(int health, int strength, int dexterity, int armour)
        {
            baseHealth = health;
            baseStrength = strength;
            baseDexterity = dexterity;
            baseArmour = armour;
            mainWeapon = null;
        }

        public void setMainWeapon(Weapon wep)
        {
            mainWeapon = wep;
        }

        public void removeMainWeapon()
        {
            mainWeapon = null;
        }
    }
}
