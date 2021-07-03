using UnityEditor;
using UnityEngine;

namespace SO.Character
{
    [CreateAssetMenu(menuName = "SO/Character/Class/Class Settings", order = 1)]
    public class CharacterClassSettings : ScriptableObject
    {
        #region Fields

        [Header("Название")]
        [SerializeField] private string nameClass;

        [Header("Описание")]
        [TextArea]
        [SerializeField] private string description;


        [Header("Иконка")]
        [SerializeField] private Sprite icon;


        [Header("Мини-иконка")]
        [SerializeField] private Sprite iconMini;


        [Header("Бонус к здоровью от базового в %")]
        [Range(-100, 100)]
        [SerializeField] private int bonusHealthProcent = 0;


        [Header("Бонус к защите от базовой в %")]
        [Range(-100, 100)]
        [SerializeField] private int bonusArmorProcent = 0;

        [Header("Бонус к урону от любого оружия в %")]
        [Range(-100, 100)]
        [SerializeField] private int bonusDamageProcent = 0;

        [Header("Бонус к оружию любого типа активен")]
        [SerializeField] private bool bonusDamageEnabled = false;

        [Header("Список оружия, которые дают бонус к урону")]
        [SerializeField] private BonusDamageWeaponSCOElement[] bonusDamageWeapons;


        #endregion


        #region Properties

        public string NameClass { get => nameClass; }
        public string Description { get => description; }
        public Sprite Icon { get => icon; }
        public Sprite IconMini { get => iconMini; }
        public int BonusHealthProcent { get => bonusHealthProcent; }
        public int BonusArmorProcent { get => bonusArmorProcent; }
        public int BonusDamageProcent { get => bonusDamageProcent; }
        public bool BonusDamageEnabled { get => bonusDamageEnabled; }
        public BonusDamageWeaponSCOElement[] BonusDamageWeapons { get => bonusDamageWeapons; }
        #endregion
    }
}