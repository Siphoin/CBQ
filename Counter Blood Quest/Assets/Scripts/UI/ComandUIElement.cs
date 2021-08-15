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
        private Button _button;

        [SerializeField]
        [Header("Команда")]
        private ComandType _comandType = ComandType.Blue;

        [SerializeField]
        [Header("Текст информации о команде")]
        private TextMeshProUGUI _textInfo;

        public event Action<ComandType> OnSelect;

        public ComandType ComandType => _comandType;

        #region Init components
        public void Init()
        {

            if (!_textInfo)
            {
                throw new ComandUIElementException($"button ui comand {name} not seted text info");
            }


            if (_button == null)
            {
           if (!TryGetComponent(out _button))
            {
                throw new ComandUIElementException($"{name} not have component UnityEngine.UI Button");
            }

                _button.onClick.AddListener(Select);
            }
 
        }

        public void SetText(string text) => _textInfo.text = text;

        #endregion

        void Start() => Init();

        private void Select() => OnSelect?.Invoke(_comandType);
        

        
    }
}
