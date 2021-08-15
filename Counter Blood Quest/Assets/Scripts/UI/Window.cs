using System;
using UnityEngine;
namespace UI
{


    public class Window : MonoBehaviour
    {
        public event Action OnExit;
        public virtual void Exit()
        {
            OnExit?.Invoke();

            Destroy(gameObject);
        }


    }

}