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
        private Dictionary<ComandType, ComandUIElement> buttons;

        private ComandType selectedComand;
        #endregion


        #region Events
        public event Action<ComandType> onSelect;
        #endregion

        public void Init()
        {
            buttons = new Dictionary<ComandType, ComandUIElement>();

            ComandUIElement[] elements = FindObjectsOfType<ComandUIElement>();

            for (int i = 0; i < elements.Length; i++)
            {
                ComandUIElement element = elements[i];

                buttons.Add(element.ComandType, element);

                element.onSelect += Element_onSelect;
            }
        }

        private void Element_onSelect(ComandType comand)
        {
            selectedComand = comand;
            Exit();
        }
        // Use this for initialization
        void Start()
        {
            Init();
        }


        public override void Exit()
        {
            foreach (var btn in buttons)
            {
                btn.Value.onSelect -= Element_onSelect;
            }


            onSelect?.Invoke(selectedComand);
            base.Exit();
        }
    }
}