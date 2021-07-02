using Client.Deserializators;
using ExitGames.Client.Photon;
using Photon.Pun;
using System;
using System.Collections;
using UnityEngine;

namespace Match
{
    [RequireComponent(typeof(PhotonView))]
    public class MatchManagerBase : MonoBehaviour, IInitObject, IPunObservable, IInvokerMono
    {
        private DateTime timeMatch = new DateTime();

        #region Events
        public event Action onInstatiate;
        #endregion

        private bool LocalPlayerIsMasterClient { get => PhotonNetwork.IsMasterClient; }
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
            CancelInvoke();


            SetStartTime();



            CallInvokingEveryMethod(TickTimeMatch, 1);

        }

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

        private void SetStartTime ()
        {
            if (LocalPlayerIsMasterClient)
            {
                timeMatch = timeMatch.AddMinutes(5);
            }
        }

        public void Init()
        {
            PhotonPeer.RegisterType(typeof(DateTime), 242, DeserializatorDateTime.SerializeDateTime, DeserializatorDateTime.DeserializeDateTime);
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
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


        public void CallInvokingEveryMethod(Action method, float time)
        {
            InvokeRepeating(method.Method.Name, time, time);
        }

        public void CallInvokingMethod(Action method, float time)
        {
            Invoke(method.Method.Name, time);
        }
    }
}