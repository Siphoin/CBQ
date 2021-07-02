using Photon.Pun;
using System;
using System.Collections;
using UnityEngine;

namespace Client
{
    [RequireComponent(typeof(PhotonView))]
    public class NetworkObject : MonoBehaviour, IInitObject, IRemoveObject, IInvokerMono
    {
        #region Fields
        private PhotonView networkView;

        #endregion

        #region Properties

        public bool IsMine        
        {
            get
            {
                return networkView == null ? false : networkView.IsMine;


            } }

        public string OwnerNickName { get
            {
                return networkView == null ? string.Empty : networkView.Owner.NickName;
            } }


        #endregion

        #region Init components

        void Start()
        {
            Init();
        }
        public virtual void Init()
        {
           if (!TryGetComponent(out networkView))
            {
                throw new NetworkObjectException($"{name} not have component Photon View");
            }
        }

        #endregion

        public void Remove()
        {
            if (PhotonNetwork.IsConnected)
            {
                if (IsMine)
                {
                    PhotonNetwork.Destroy(gameObject);
                }
            }

            else
            {
                Destroy(gameObject);
            }
        }

        public void Remove(float time)
        {
            CallInvokingMethod(Remove, time);
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