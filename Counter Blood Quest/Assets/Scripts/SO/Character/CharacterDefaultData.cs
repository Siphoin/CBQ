using UnityEngine;

namespace SO.Character
{
    [CreateAssetMenu(menuName = "SO/Character/Character Default Settings", order = 0)]
    public class CharacterDefaultData : ScriptableObject
    {
        #region Fields
        [Header("Изначальное значение здоровья персонажа")]
        [SerializeField] private int defaultHealth = 100;

        [Header("Изначальное значение защиты персонажа")]
        [SerializeField] private int defaultArmor = 0;

        [Header("Изначальное значение скорости передвижения персонажа")]
        [SerializeField] private int defaultSpeedMovement = 5;

        [Header("Изначальное значение скорости вращения персонажа")]
        [SerializeField] private int defaultSpeedRotating = 10;

        #endregion
        #region Properties
        public int DefaultHealth { get => defaultHealth; }
        public int DefaultArmor { get => defaultArmor; }
        public int DefaultSpeedMovement { get => defaultSpeedMovement; }
        public int DefaultSpeedRotating { get => defaultSpeedRotating; }

        #endregion
    }
}