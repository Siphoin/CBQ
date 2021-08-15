using Photon.Pun;
using System;
using System.Collections;
using UnityEngine;

namespace Client
{
    [RequireComponent(typeof(PhotonView))]
    public class NetworkObject : MonoBehaviour, IInitObject, IRemoveObject, IInvokerMono
    {
        private PhotonView _networkView;

        public bool IsMine => _networkView == null ? false : _networkView.IsMine;

        public string OwnerNickName => _networkView == null ? string.Empty : _networkView.Owner.NickName;

        public virtual void Init()
        {
           if (!TryGetComponent(out _networkView))
            {
                throw new NetworkObjectException($"{name} not have component Photon View");
            }
        }


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
        
        private void Start() => Init();

        public void Remove(float time) => CallInvokingMethod(Remove, time);

        public void CallInvokingEveryMethod(Action method, float time) => InvokeRepeating(method.Method.Name, time, time);

        public void CallInvokingMethod(Action method, float time) => Invoke(method.Method.Name, time);
    }
}
