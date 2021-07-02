using System.Collections;
using UnityEngine;

namespace CameraGame
{
    public class GameCamera : MonoBehaviour
    {
        #region Fields
        private Transform target;
        #endregion

        // Update is called once per frame
        void Update()
        {
            if (target)
            {
                Vector3 pos = target.position;

                pos.z = transform.position.z;

                transform.position = pos;
            }
        }
        #region Interaction
        public void SetTarget(Transform target)
        {
            if (target == null)
            {
                throw new GameCameraException("argument target is null");
            }

            this.target = target;
        }
        #endregion
    }
}