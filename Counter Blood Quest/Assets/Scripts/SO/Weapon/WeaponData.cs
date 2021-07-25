using UnityEditor;
using UnityEngine;
using Weapon;

namespace SO.Weapon
{
    [ExecuteAlways]
    [CreateAssetMenu(menuName = "SO/Weapon/Weapon Data", order = 0)]
    public class WeaponData : ScriptableObject
    {
        #region Fields
        [Header("Название оружия")]
        [SerializeField] private string nameWeapon;

        [Header("Описание оружия")]
        [TextArea]
        [SerializeField] private string descriptionWeapon;

        [Header("Максимальное кол-во патронов в обойме")]
        [SerializeField] private int maxAmmunition;

        [Header("Текущее кол-во патронов в обойме")]
        [SerializeField] private int startAmmunition;

        [Header("Урон")]
        [SerializeField] private uint damage;

        [Header("Иконка")]
        [SerializeField] private Sprite icon;

        [Header("Тип оружия")]
        [SerializeField] private WeaponType type = WeaponType.Gun;

        #endregion


        #region Properties
        public string NameWeapon { get => nameWeapon; }
        public string DescriptionWeapon { get => descriptionWeapon; }
        public int MaxAmmunition { get => maxAmmunition; }
        public int StartAmmunition { get => startAmmunition; }
        public int Damage { get => (int)damage; }
        public Sprite Icon { get => icon; }
        public WeaponType Type { get => type; }

        #endregion
    }
}
