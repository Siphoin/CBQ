using Match;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using UI;
using UnityEngine;

namespace Match
{
    public class JoinerMatch : MonoBehaviourPunCallbacks, IInitObject
    {
        #region Contstants
        private const string PATH_PREFAB_WINDOW_SELECT_COMAND = "Prefabs/UI/WindowSelectComand";

        #endregion
        #region Fields
        private WindowSelectComand windowSelectComandPrefab;

        private WindowSelectComand windowSelectComandActive;

        [Header("Объект режима матча")]
        [SerializeField] private MatchManagerBase matchManager;

        private MatchManagerBase activeMatchManager = null;
        #endregion
        // Use this for initialization
        void Start()
        {

            if (!matchManager)
            {
                throw new JoinerMatchException("match manager not seted");
            }


            Init();


            if (!PhotonNetwork.IsConnected)
            {
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
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

        

        public void Init()
        {
            windowSelectComandPrefab = Resources.Load<WindowSelectComand>(PATH_PREFAB_WINDOW_SELECT_COMAND);

            if (!windowSelectComandPrefab)
            {
                throw new JoinerMatchException("prefab window select comand not found");
            }



        }

        private void CreateActiveWindowSelectComand ()
        {
            windowSelectComandActive = Instantiate(windowSelectComandPrefab);
            windowSelectComandActive.onSelect += SelectComand;
            windowSelectComandActive.onExit += UncribeEvents;
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

        private void SelectComand(ComandType obj)
        {
            if (activeMatchManager != null)
            {
            activeMatchManager.NewRound();
            }

        }


    }
}