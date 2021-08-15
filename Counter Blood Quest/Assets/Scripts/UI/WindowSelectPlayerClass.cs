using Match;
using Photon.Pun;
using Photon.Realtime;
using SO.Character;
using System;
using System.Collections;
using TMPro;
using UI.LoadingSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WindowSelectPlayerClass : MonoBehaviourPunCallbacks, IInitObject, ICallerLoadingWait
    {
        #region Constants
        private const string PATH_CLASSES = "SO/Character/Classes";

        private const byte EVENT_CODE_CREATE_CHARACTER = 133;
        #endregion

        #region Fields


        private int _currentIndexSelectedClass = 0;


        private CharacterClassSettings[] _classes;


        [Header("Кнопка назад")]
        [SerializeField] private Button _buttonBack;

        [Header("Кнопка вперед")]
        [SerializeField] private Button _buttonNext;

        [Header("Кнопка выбрать")]
        [SerializeField] private Button _buttonSelect;

        [Header("Пиктограмма класса персонажа")]
        [SerializeField] private Image _pictogram;

        [Header("Мини-иконка класса")]
        [SerializeField] private Image _pictogramMini;


        [Header("Текст названия класса")]
        [SerializeField] private TextMeshProUGUI _label;

        [Header("Текст описания класса")]
        [SerializeField] private TextMeshProUGUI _textDescription;

        private LoadingWait _lastLoadingWait;



        #region Delegates
        delegate void ArrowInteraction();

        private ArrowInteraction arrowBackInteraction = null;
        private ArrowInteraction arrowNextInteraction = null;
        #endregion

        #endregion

        #region Init window

        public void Init()
        {
            if (!_buttonSelect)
            {
                throw new WindowSelectPlayerClassException("button select not seted");
            }


            if (!_buttonBack)
            {
                throw new WindowSelectPlayerClassException("button back not seted");
            }

            if (!_buttonNext)
            {
                throw new WindowSelectPlayerClassException("button next not seted");
            }


            if (!_pictogram)
            {
                throw new WindowSelectPlayerClassException("pictogram not seted");
            }

            if (!_pictogramMini)
            {
                throw new WindowSelectPlayerClassException("pictogram mini not seted");
            }

            if (!_textDescription)
            {
                throw new WindowSelectPlayerClassException("text description not seted");
            }

            if (!_label)
            {
                throw new WindowSelectPlayerClassException("label not seted");
            }


            _classes = Resources.LoadAll<CharacterClassSettings>(PATH_CLASSES);

            if (_classes.Length == 0)
            {
                throw new WindowSelectPlayerClassException("list classes of character not found");
            }

            AddCombineListener(arrowBackInteraction, _buttonBack, BackClass);
            AddCombineListener(arrowNextInteraction, _buttonNext, NextClass);

            _buttonSelect.onClick.AddListener(Select);

            ShowCurrentInfoClass();

        }

        private void Select()
        {
            ExitGames.Client.Photon.Hashtable hashTablePlayer = PhotonNetwork.LocalPlayer.CustomProperties;

            hashTablePlayer.Add("class", _currentIndexSelectedClass);

            PhotonNetwork.LocalPlayer.SetCustomProperties(hashTablePlayer);

            // create loading wait, because wait setting custom properyies local player

            CreateLoadingWait();

        }

        #endregion

        #region Interaction

        #region Arrows UI
        private void BackClass()
        {
            if (_currentIndexSelectedClass == 0)
            {
                return;
            }

            _currentIndexSelectedClass--;
            


        }

        private void NextClass()
        {
            if (_currentIndexSelectedClass >= _classes.Length - 1)
            {
                return;
            }

            _currentIndexSelectedClass++;
        }


        #endregion

        private void ShowCurrentInfoClass ()
        {
            CharacterClassSettings currentClass = _classes[_currentIndexSelectedClass];

            _label.text = currentClass.NameClass;

            _textDescription.text = currentClass.Description;
            _pictogram.sprite = currentClass.Icon;
            _pictogramMini.sprite = currentClass.IconMini;

        }

        private void AddCombineListener (ArrowInteraction arrowDelegate, Button button, Action action)
        {
            arrowDelegate += action.Invoke;
            arrowDelegate += ShowCurrentInfoClass;

            button.onClick.AddListener(() => arrowDelegate());
        }

        private void RemoveCombineListener(ArrowInteraction arrowDelegate, Button button, Action action)
        {
            arrowDelegate -= action.Invoke;
            arrowDelegate -= ShowCurrentInfoClass;

            button.onClick.RemoveAllListeners();
        }


        #endregion

        public void Exit()
        {
            RemoveCombineListener(arrowBackInteraction, _buttonBack, BackClass);
            RemoveCombineListener(arrowNextInteraction, _buttonNext, NextClass);

            RemoveLoadingWait();


            // send event on create local player character
           
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.MasterClient };

            object data = new object[] { PhotonNetwork.LocalPlayer.CustomProperties["team"], _currentIndexSelectedClass, PhotonNetwork.LocalPlayer.NickName };


            ExitGames.Client.Photon.SendOptions sendOptions = new ExitGames.Client.Photon.SendOptions();


            PhotonNetwork.RaiseEvent(EVENT_CODE_CREATE_CHARACTER, data, raiseEventOptions, sendOptions);

            Destroy(gameObject);
        }

        #region Photon Callbacks
        public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
        {
            if (targetPlayer == PhotonNetwork.LocalPlayer)
            {
#if UNITY_EDITOR
                string endDataPlayer = $"Player Data {targetPlayer.NickName}\n\n";

                foreach (var item in targetPlayer.CustomProperties)
                {
                    endDataPlayer += $"{item.Key}: {item.Value}\n";
                }

                Debug.Log(endDataPlayer);
#endif
                Exit();
            }
        }

        public void RemoveLoadingWait() {
            if (_lastLoadingWait) _lastLoadingWait.Exit();
        }


        void Start() => Init();
        public void CreateLoadingWait() => _lastLoadingWait = LoadingWaitManager.Manager.NewLoadingWait();


        #endregion

    }
}
