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
        [SerializeField] private string _nameWeapon;

        [Header("Описание оружия")]
        [TextArea]
        [SerializeField] private string _descriptionWeapon;

        [Header("Максимальное кол-во патронов в обойме")]
        [SerializeField] private int _maxAmmunition;

        [Header("Текущее кол-во патронов в обойме")]
        [SerializeField] private int _startAmmunition;

        [Header("Урон")]
        [SerializeField] private uint _damage;

        [Header("Иконка")]
        [SerializeField] private Sprite _icon;

        [Header("Тип оружия")]
        [SerializeField] private WeaponType _type = WeaponType.Gun;

        #endregion


        #region Properties
        public string NameWeapon => _nameWeapon;
        public string DescriptionWeapon => _descriptionWeapon;
        public int MaxAmmunition => _maxAmmunition;
        public int StartAmmunition => _startAmmunition;
        public int Damage => (int)_damage;
        public Sprite Icon => _icon;
        public WeaponType Type => _type;

        #endregion
    }
}
