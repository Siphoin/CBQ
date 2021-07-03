using Client.Deserializators;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using UnityEngine;
using System.Linq;

namespace Match
{
    [RequireComponent(typeof(PhotonView))]
    public class MatchManagerBase : MonoBehaviourPunCallbacks, IInitObject, IPunObservable, IInvokerMono, IOnEventCallback, IRPCSender
    {

        #region Constants
        private const byte EVENT_CODE_CREATE_CHARACTER = 133;
        #endregion
        #region Fields
        private DateTime timeMatch = new DateTime();
        #endregion

        #region Events
        public event Action onInstatiate;
        #endregion

        #region Properties
        private bool LocalPlayerIsMasterClient { get => PhotonNetwork.IsMasterClient; }
        #endregion
        // Use this for initialization
        void Start()
        {
            Init();

                onInstatiate?.Invoke();

#if UNITY_EDITOR
                Debug.Log("match manager created and working...");
#endif
            
        }


        public virtual void NewRound ()
        {
            // cancel all old invokes on mono


            CancelInvoke();

            // set start time
            SetStartTime();


            // call tick match time
            CallInvokingEveryMethod(TickTimeMatch, 1);

        }
        #region Time
        private void TickTimeMatch()
        {
            if (LocalPlayerIsMasterClient)
            {
                try
                {
                    timeMatch = timeMatch.AddSeconds(-1);
                }
                catch
                {


                }
            }
        }

        private void SetStartTime()
        {
            if (LocalPlayerIsMasterClient)
            {
                timeMatch = timeMatch.AddMinutes(5);
            }
        }
        #endregion

        #region Init match manager
        public void Init()
        {
            PhotonPeer.RegisterType(typeof(DateTime), 242, DeserializatorDateTime.SerializeDateTime, DeserializatorDateTime.DeserializeDateTime);
            PhotonPeer.RegisterType(typeof(ComandType), 5, DeserializatorComandType.SerializeComandType, DeserializatorComandType.DeserializeComandType);
        }
        #endregion


        #region Photon Callbacks
        public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(timeMatch);
            }

            else
            {
                timeMatch = (DateTime)stream.ReceiveNext();
                Debug.Log(timeMatch.ToString("mm:ss"));
            }
        }


        public void OnEvent(EventData photonEvent)
        {
#if UNITY_EDITOR
            Debug.Log($"match manager catched event from raise event: Code: {photonEvent.Code} Parameters count: {photonEvent.Parameters.Count}");
#endif
            if (photonEvent.Code == EVENT_CODE_CREATE_CHARACTER)
            {
                Debug.Log("character create event");

                foreach (var item in photonEvent.Parameters)
                {
                    if (item.Value is object[])
                    {
                        object[] data = (object[])item.Value;
                        ComandType comand = (ComandType)data[0];
                        int classIndex = (int)data[1];
                        string nickName = (string)data[2];

                        Player target = PhotonNetwork.PlayerList.First(pl => pl.NickName == nickName);

                        photonView.RpcSecure(nameof(CreateCharacter), target, true, comand, classIndex);
                        break;
                    }
                }

            }
        }

        #region RPC
        [PunRPC]
        private void CreateCharacter(ComandType comand, int classIndex)
        {
#if UNITY_EDITOR
            Debug.Log($"creating character: Comand {comand}: Class {classIndex}");
#endif
        }


        #endregion
        #endregion


        public void CallInvokingEveryMethod(Action method, float time)
        {
            InvokeRepeating(method.Method.Name, time, time);
        }

        public void CallInvokingMethod(Action method, float time)
        {
            Invoke(method.Method.Name, time);
        }

        public void SendRPC(Action action, RpcTarget target = RpcTarget.All, params object[] parameters)
        {
            photonView.RPC(action.Method.Name, target, parameters);
        }

        public void SendSecureRPC(Action action, RpcTarget target = RpcTarget.All, bool encrypt = true, params object[] parameters)
        {
            photonView.RpcSecure(action.Method.Name, target, encrypt, parameters);
        }
    }
}