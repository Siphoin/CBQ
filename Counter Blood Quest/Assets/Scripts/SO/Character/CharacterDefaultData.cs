using UnityEngine;

namespace SO.Character
{
    [CreateAssetMenu(menuName = "SO/Character/Character Default Settings", order = 0)]
    public class CharacterDefaultData : ScriptableObject
    {
        #region Fields
        [Header("Изначальное значение здоровья персонажа")]
        [SerializeField] private int _defaultHealth = 100;

        [Header("Изначальное значение защиты персонажа")]
        [SerializeField] private int _defaultArmor = 0;

        [Header("Изначальное значение скорости передвижения персонажа")]
        [SerializeField] private float _defaultSpeedMovement = 5;

        [Header("Изначальное значение скорости вращения персонажа")]
        [SerializeField] private int _defaultSpeedRotating = 10;

        #endregion
        #region Properties
        public int DefaultHealth => _defaultHealth;
        public int DefaultArmor => _defaultArmor; 
        public int DefaultSpeedRotating => _defaultSpeedRotating;
        
        public float DefaultSpeedMovement => _defaultSpeedMovement;

        #endregion
    }
}
