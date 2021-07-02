using SO.Weapon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Weapon
{
    [Serializable]
   public class WeaponDynamicData
    {
        #region Fields
        public int currentDamage = 0;
        public int currentAmmunition = 0;
        public int currentMaxAmmunition = 0;
        #endregion

        #region Properties
        public string NameWeapon { get; private set; }

        public string Description { get; private set; }

        public Sprite CurrentIcon { get; private set; }

        public WeaponType Type { get; private set; }
        #endregion

        #region Constructors
        public WeaponDynamicData ()
        {

        }

        public WeaponDynamicData (WeaponData data)
        {
           if (!data)
            {
                throw new WeaponDataException("data weapon template is null");
            }

            currentAmmunition = data.StartAmmunition;
            currentDamage = data.Damage;
            currentMaxAmmunition = data.MaxAmmunition;
            Description = data.DescriptionWeapon;
            NameWeapon = data.NameWeapon;
            Type = data.Type;
        }
        #endregion
    }
}
