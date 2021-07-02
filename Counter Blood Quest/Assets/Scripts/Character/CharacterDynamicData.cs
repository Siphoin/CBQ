using SO.Character;
using System;

namespace Character.Data
{
    [Serializable]
  public  class CharacterDynamicData
    {
        #region Fields
        public int currentHealth = 100;
        public int currentArmor = 0;
        public float currentSpeedMovement = 10;

        #endregion


        #region Constructors
        public CharacterDynamicData ()
        {

        }

        public CharacterDynamicData (CharacterDefaultData defaultData)
        {
            if (!defaultData)
            {
                throw new CharacterDataException("defaultdata character is null");
            }

            currentHealth = defaultData.DefaultHealth;
            currentArmor = defaultData.DefaultArmor;
            currentSpeedMovement = defaultData.DefaultSpeedMovement;
        }

        public CharacterDynamicData (int health, int armor, float speedMovement)
        {
            currentHealth = health;
            currentArmor = armor;
            currentSpeedMovement = speedMovement;
        }

        #endregion
    }
}

