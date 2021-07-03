using Client.SO;
using Match;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System;
using System.Collections;
using UI;
using UI.LoadingSystem;
using UnityEngine;

namespace Match
{
    public class JoinerMatch : MonoBehaviourPunCallbacks, IInitObject, ICallerLoadingWait
    {
        #region Contstants
        private const string PATH_PREFAB_WINDOW_SELECT_COMAND = "Prefabs/UI/WindowSelectComand";
        private const string PATH_SETTINGS_LOCAL_PLAYER_HASHTABLE = "SO/Client/Match/PlayerMatchSettings";
        private const string PATH_PREFAB_WINDOW_SELECT_CLASS = "Prefabs/UI/WindowSelectClass";



        #endregion
        #region Fields
        private WindowSelectComand windowSelectComandPrefab;

        private WindowSelectComand windowSelectComandActive;

        private WindowSelectPlayerClass windowSelectPlayerClassPrefab;

        private LoadingWait lastLoadingWait;

        [Header("Объект режима матча")]
        [SerializeField] private MatchManagerBase matchManager;

        private MatchManagerBase activeMatchManager = null;

        private PlayerMatchSettings playerMatchSettings;
        #endregion
        // Use this for initialization
        void Start()
        {

            if (!matchManager)
            {
                throw new JoinerMatchException("match manager not seted");
            }


            Init();

            CreateLoadingWait();


            if (!PhotonNetwork.IsConnected)
            {
                PhotonNetwork.ConnectUsingSettings();
            }
        }


        #region Photon Callbacks
        public override void OnConnectedToMaster()
        {          
                PhotonNetwork.JoinOrCreateRoom("TestRoom", new RoomOptions(), TypedLobby.Default);
            
        }

        public override void OnJoinedRoom()
        {
            CreateMatchManager();

            if (!PhotonNetwork.IsMasterClient)
            {
                activeMatchManager = FindObjectOfType<MatchManagerBase>();


                CreateActiveWindowSelectComand();
            }


        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
        {
            if (targetPlayer == PhotonNetwork.LocalPlayer)
            {

#if UNITY_EDITOR

                var hashtable = targetPlayer.CustomProperties;
                Debug.Log($"You seted comand {(ComandType)hashtable["team"]}");
#endif
                RemoveLoadingWait();

                Instantiate(windowSelectPlayerClassPrefab);
                Destroy(gameObject);
            }
        }

        #endregion


        #region Init components
        public void Init()
        {
            windowSelectComandPrefab = Resources.Load<WindowSelectComand>(PATH_PREFAB_WINDOW_SELECT_COMAND);

            if (!windowSelectComandPrefab)
            {
                throw new JoinerMatchException("prefab window select comand not found");
            }

            windowSelectPlayerClassPrefab = Resources.Load<WindowSelectPlayerClass>(PATH_PREFAB_WINDOW_SELECT_CLASS);

            if (!windowSelectPlayerClassPrefab)
            {
                throw new JoinerMatchException("prefab window select class not found");
            }

            playerMatchSettings = Resources.Load<PlayerMatchSettings>(PATH_SETTINGS_LOCAL_PLAYER_HASHTABLE);

            if (!playerMatchSettings)
            {
                throw new JoinerMatchException("player match settings not found");
            }



        }

        #endregion

        private void CreateActiveWindowSelectComand ()
        {
            windowSelectComandActive = Instantiate(windowSelectComandPrefab);
            windowSelectComandActive.onSelect += SelectComand;
            windowSelectComandActive.onExit += UncribeEvents;
            RemoveLoadingWait();
        }

        private void CreateMatchManager ()
        {
            if (PhotonNetwork.IsMasterClient)
            {


             GameObject go =   PhotonNetwork.InstantiateRoomObject(matchManager.name, Vector3.zero, Quaternion.identity);

                if (!go.TryGetComponent(out activeMatchManager))
                {
                    throw new JoinerMatchException($"match manager {name} not have component Match Manager");
                }

                activeMatchManager.onInstatiate += MatchManagerCreated;
            }
        }

        private void MatchManagerCreated()
        {
            activeMatchManager.onInstatiate -= MatchManagerCreated;
            CreateActiveWindowSelectComand();
        }

        private void UncribeEvents()
        {
            windowSelectComandActive.onSelect -= SelectComand;
            windowSelectComandActive.onExit -= UncribeEvents;
        }

        private void SelectComand(ComandType comand)
        {
            Player localPlayer = PhotonNetwork.LocalPlayer;
            localPlayer.SetCustomProperties(playerMatchSettings.CreateNewHashtablePlayer(comand));

            // create loading wait, because wait setting custom properyies local player
            CreateLoadingWait();
        }

        public void CreateLoadingWait()
        {
            lastLoadingWait = LoadingWaitManager.Manager.NewLoadingWait();
        }

        public void RemoveLoadingWait()
        {
            if (lastLoadingWait)
            {
                lastLoadingWait.Exit();
            }

        }
    }
}