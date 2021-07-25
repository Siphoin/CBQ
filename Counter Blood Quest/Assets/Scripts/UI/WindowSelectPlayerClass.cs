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


        private int currentIndexSelectedClass = 0;


        private CharacterClassSettings[] classes;


        [Header("Кнопка назад")]
        [SerializeField] private Button buttonBack;

        [Header("Кнопка вперед")]
        [SerializeField] private Button buttonNext;

        [Header("Кнопка выбрать")]
        [SerializeField] private Button buttonSelect;

        [Header("Пиктограмма класса персонажа")]
        [SerializeField] private Image pictogram;

        [Header("Мини-иконка класса")]
        [SerializeField] private Image pictogramMini;


        [Header("Текст названия класса")]
        [SerializeField] private TextMeshProUGUI label;

        [Header("Текст описания класса")]
        [SerializeField] private TextMeshProUGUI textDescription;

        private LoadingWait lastLoadingWait;



        #region Delegates
        delegate void ArrowInteraction();

        private ArrowInteraction arrowBackInteraction = null;
        private ArrowInteraction arrowNextInteraction = null;
        #endregion

        #endregion
        #region Init window
        // Use this for initialization
        void Start()
        {
            Init();
        }

        public void Init()
        {
            if (!buttonSelect)
            {
                throw new WindowSelectPlayerClassException("button select not seted");
            }


            if (!buttonBack)
            {
                throw new WindowSelectPlayerClassException("button back not seted");
            }

            if (!buttonNext)
            {
                throw new WindowSelectPlayerClassException("button next not seted");
            }


            if (!pictogram)
            {
                throw new WindowSelectPlayerClassException("pictogram not seted");
            }

            if (!pictogramMini)
            {
                throw new WindowSelectPlayerClassException("pictogram mini not seted");
            }

            if (!textDescription)
            {
                throw new WindowSelectPlayerClassException("text description not seted");
            }

            if (!label)
            {
                throw new WindowSelectPlayerClassException("label not seted");
            }


            classes = Resources.LoadAll<CharacterClassSettings>(PATH_CLASSES);

            if (classes.Length == 0)
            {
                throw new WindowSelectPlayerClassException("list classes of character not found");
            }

            AddCombineListener(arrowBackInteraction, buttonBack, BackClass);
            AddCombineListener(arrowNextInteraction, buttonNext, NextClass);

            buttonSelect.onClick.AddListener(Select);

            ShowCurrentInfoClass();

        }

        private void Select()
        {
            ExitGames.Client.Photon.Hashtable hashTablePlayer = PhotonNetwork.LocalPlayer.CustomProperties;


            hashTablePlayer.Add("class", currentIndexSelectedClass);

            PhotonNetwork.LocalPlayer.SetCustomProperties(hashTablePlayer);


            // create loading wait, because wait setting custom properyies local player


            CreateLoadingWait();

        }

        #endregion

        #region Interaction

        #region Arrows UI
        private void BackClass()
        {
            if (currentIndexSelectedClass == 0)
            {
                return;
            }

            currentIndexSelectedClass--;
            


        }

        private void NextClass()
        {
            if (currentIndexSelectedClass >= classes.Length - 1)
            {
                return;
            }

            currentIndexSelectedClass++;
        }


        #endregion

        private void ShowCurrentInfoClass ()
        {
            CharacterClassSettings currentClass = classes[currentIndexSelectedClass];

            label.text = currentClass.NameClass;

            textDescription.text = currentClass.Description;
            pictogram.sprite = currentClass.Icon;
            pictogramMini.sprite = currentClass.IconMini;

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
            RemoveCombineListener(arrowBackInteraction, buttonBack, BackClass);
            RemoveCombineListener(arrowNextInteraction, buttonNext, NextClass);

            RemoveLoadingWait();


            // send event on create local player character
           
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.MasterClient };

            object data = new object[] { PhotonNetwork.LocalPlayer.CustomProperties["team"], currentIndexSelectedClass, PhotonNetwork.LocalPlayer.NickName };


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

        public void CreateLoadingWait() => lastLoadingWait = LoadingWaitManager.Manager.NewLoadingWait();

        public void RemoveLoadingWait() => if (lastLoadingWait) { lastLoadingWait.Exit(); }
        
        
        #endregion

    }
}
