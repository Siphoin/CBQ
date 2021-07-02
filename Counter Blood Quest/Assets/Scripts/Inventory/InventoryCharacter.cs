using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Weapon;

namespace Inventory
{
    [Serializable]
  public  class InventoryCharacter
    {
        #region Fields


        [JsonRequired]
      private  List<WeaponDynamicData> weaponList;

        #endregion
        #region Constructors
        public InventoryCharacter ()
        {
            weaponList = new List<WeaponDynamicData>();
        }

        public InventoryCharacter (List<WeaponDynamicData> list)
        {
            if (list == null)
            {
                throw new InventoryCharacterException("list is null");
            }

            weaponList = list;
        }

        #endregion

        #region Interactions

        public void Add (WeaponDynamicData weapon)
        {

            CheckWeaponArgumentisNull(weapon);


            if (weaponList.Contains(weapon))
            {
                throw new InventoryCharacterException($"current list weapon of the character contains the weapon {weapon.NameWeapon}");
            }


        }

        public bool TryAdd(WeaponDynamicData weapon)
        {
            CheckWeaponArgumentisNull(weapon);

            if (weaponList.Contains(weapon))
            {
                return false;
            }

            if (weaponList.Count(x => x.Type == weapon.Type) > 0)
            {
                return false;
            }

            Add(weapon);
            return true;
        }

        public bool TryGet (int index, out WeaponDynamicData weapon)
        {
            if (index < 0)
            {
                throw new InventoryCharacterException("index is invalid");
            }


            if (index > weaponList.Count - 1)
            {
                weapon = null;
                return false;
            }

            weapon = weaponList.ElementAt(index);
            return true;
        }

        public bool TryRemove(int index)
        {
            if (index < 0)
            {
                throw new InventoryCharacterException("index is invalid");
            }


            if (index > weaponList.Count - 1)
            {
                return false;
            }

            weaponList.RemoveAt(index);
            return true;
        }

        public bool TryRemove(WeaponDynamicData weapon)
        {
            CheckWeaponArgumentisNull(weapon);

            if (!weaponList.Contains(weapon))
            {
                return true;
            }

            weaponList.Remove(weapon);
            return true;
        }

        #endregion

        #region Other

        private void CheckWeaponArgumentisNull (WeaponDynamicData weapon)
        {
            if (weapon == null)
            {
                throw new InventoryCharacterException("weapon argument is null");
            }
        }

        #endregion
    }
}
