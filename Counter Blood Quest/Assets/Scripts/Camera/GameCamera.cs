using System.Collections;
using UnityEngine;

namespace CameraGame
{
    public class GameCamera : MonoBehaviour
    {
        private Transform _target;

        void Update()
        {
            if (_target)
            {
                Vector3 pos = _target.position;

                pos.z = transform.position.z;

                transform.position = pos;
            }
        }
        
        public void SetTarget(Transform target)
        {
            if (target == null)
            {
                throw new GameCameraException("argument target is null");
            }

            _target = target;
        }
    }
}
