using UnityEditor;
using UnityEngine;

namespace SO.Character
{
    [CreateAssetMenu(menuName = "SO/Character/Class/Class Settings", order = 1)]
    public class CharacterClassSettings : ScriptableObject
    {
        #region Fields

        [Header("Название")]
        [SerializeField] private string _nameClass;

        [Header("Описание")]
        [TextArea]
        [SerializeField] private string _description;


        [Header("Иконка")]
        [SerializeField] private Sprite _icon;


        [Header("Мини-иконка")]
        [SerializeField] private Sprite _iconMini;


        [Header("Бонус к здоровью от базового в %")]
        [Range(-100, 100)]
        [SerializeField] private int _bonusHealthProcent = 0;


        [Header("Бонус к защите от базовой в %")]
        [Range(-100, 100)]
        [SerializeField] private int _bonusArmorProcent = 0;

        [Header("Бонус к урону от любого оружия в %")]
        [Range(-100, 100)]
        [SerializeField] private int _bonusDamageProcent = 0;

        [Header("Бонус к оружию любого типа активен")]
        [SerializeField] private bool _bonusDamageEnabled = false;

        [Header("Список оружия, которые дают бонус к урону")]
        [SerializeField] private BonusDamageWeaponSCOElement[] _bonusDamageWeapons;


        #endregion


        #region Properties

        public string NameClass => _nameClass;
        public string Description => _description;
        public Sprite Icon => _icon;
        public Sprite IconMini => _iconMini;
        public int BonusHealthProcent => _bonusHealthProcent;
        public int BonusArmorProcent => _bonusArmorProcent;
        public int BonusDamageProcent => _bonusDamageProcent;
        public bool BonusDamageEnabled => _bonusDamageEnabled;
        public BonusDamageWeaponSCOElement[] BonusDamageWeapons => _bonusDamageWeapons;
        #endregion
    }
}