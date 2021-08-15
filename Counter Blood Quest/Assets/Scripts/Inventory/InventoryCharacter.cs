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
        [JsonRequired]
      private  List<WeaponDynamicData> _weaponList;
      
        public InventoryCharacter ()
        {
            _weaponList = new List<WeaponDynamicData>();
        }

        public InventoryCharacter (List<WeaponDynamicData> list)
        {
            if (list == null)
            {
                throw new InventoryCharacterException("list is null");
            }

            _weaponList = list;
        }


        #region Interactions

        public void Add (WeaponDynamicData weapon)
        {
            CheckWeaponArgumentisNull(weapon);

            if (_weaponList.Contains(weapon))
            {
                throw new InventoryCharacterException($"current list weapon of the character contains the weapon {weapon.NameWeapon}");
            }
             
            _weaponList.Add(weapon);

        }

        public bool TryAdd(WeaponDynamicData weapon)
        {
            CheckWeaponArgumentisNull(weapon);

            if (_weaponList.Contains(weapon))
            {
                return false;
            }

            if (_weaponList.Any(x => x.Type == weapon.Type))
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


            if (index > _weaponList.Count - 1)
            {
                weapon = null;
                return false;
            }

            weapon = _weaponList.ElementAt(index);
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

            _weaponList.RemoveAt(index);
            return true;
        }

        public bool TryRemove(WeaponDynamicData weapon)
        {
            CheckWeaponArgumentisNull(weapon);

            if (!_weaponList.Contains(weapon))
            {
                return false;
            }

            _weaponList.Remove(weapon);
            
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
