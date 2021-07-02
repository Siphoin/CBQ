using System;
using UnityEngine;
namespace UI
{


    public class Window : MonoBehaviour
    {
        #region Events
        public event Action onExit;
        #endregion
        // Use this for initialization


        public virtual void Exit()
        {
            onExit?.Invoke();

            Destroy(gameObject);
        }


    }

}