using SO.Weapon;
using System;
using UnityEngine;
using Weapon;

namespace SO.Character
{
    [Serializable]
  public  class BonusDamageWeaponSCOElement
    {
        #region Fields
        public WeaponType weaponType;

        [Range(0, 100)]
        public int bonusDamage = 0;
        #endregion

        #region Constructors
        public BonusDamageWeaponSCOElement ()
        {

        }
        #endregion

    }
}
