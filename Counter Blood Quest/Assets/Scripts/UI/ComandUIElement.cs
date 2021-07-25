using Match;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class ComandUIElement : MonoBehaviour, IInitObject, ISeterText
    {
        #region Fields
        private Button button;

        [SerializeField]
        [Header("Команда")]
        private ComandType comandType = ComandType.Blue;

        [SerializeField]
        [Header("Текст информации о команде")]
        private TextMeshProUGUI textInfo;

        #endregion
        #region Properties
        public ComandType ComandType { get => comandType; }

        #endregion

        #region Events
        public event Action<ComandType> onSelect;
        #endregion


        #region Init components
        public void Init()
        {

            if (!textInfo)
            {
                throw new ComandUIElementException($"button ui comand {name} not seted text info");
            }


            if (button == null)
            {
           if (!TryGetComponent(out button))
            {
                throw new ComandUIElementException($"{name} not have component UnityEngine.UI Button");
            }

                button.onClick.AddListener(Select);
            }
 
        }

        public void SetText(string text) => textInfo.text = text;

        #endregion

        private void Select() => onSelect?.Invoke(comandType);
        

        // Use this for initialization
        void Start() => Init();
        
    }
}
