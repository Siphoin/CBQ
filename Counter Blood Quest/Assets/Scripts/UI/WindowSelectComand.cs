using Match;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class WindowSelectComand : Window, IInitObject
    {
        #region Fields
        private Dictionary<ComandType, ComandUIElement> _buttons;

        private ComandType _selectedComand;
        #endregion


        #region Events
        public event Action<ComandType> onSelect;
        #endregion

        public void Init()
        {
            if (_buttons == null)
            {
            _buttons = new Dictionary<ComandType, ComandUIElement>();

            ComandUIElement[] elements = FindObjectsOfType<ComandUIElement>();

            for (int i = 0; i < elements.Length; i++)
            {
                ComandUIElement element = elements[i];

                _buttons.Add(element.ComandType, element);

                element.OnSelect += Element_onSelect;
            }
            }
            
        }

        private void Element_onSelect(ComandType comand)
        {
            _selectedComand = comand;

            Exit();
        }
        // Use this for initialization
        void Start() => Init();


        public override void Exit()
        {
            foreach (var btn in _buttons)
            {
                btn.Value.OnSelect -= Element_onSelect;
            }


            onSelect?.Invoke(_selectedComand);
            base.Exit();
        }

        public void RefreshDataComand (ComandType comand, ComandUIData data)
        {
            if (data == null)
            {
                throw new WindowSelectComandException("data is null");
            }
            Init();


            if (!_buttons.ContainsKey(comand))
            {
                throw new WindowSelectComandException($"comand {comand} not found");
            }

            string text = $"{data.countPlayers} Players | {data.countBots} Bots";

            _buttons[comand].SetText(text);
        }
    }
}
