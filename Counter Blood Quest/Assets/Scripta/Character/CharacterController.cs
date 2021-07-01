using GameJoystik;
using System;
using System.Collections;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterController : MonoBehaviour, ISeterSprite, IInitObject, IRemoveObject
    {
        #region Fields
        private Joystik localJoystik;

        private Rigidbody2D body;

        #endregion
        public void Init()
        {

            if (!Joystik.Active)
            {
                throw new CharacterControllerException("Local joystik not found");
            }

            if (!TryGetComponent(out body))
            {
                throw new CharacterControllerException($" character {name} not have component Rightbody2D");
            }

            if (!localJoystik)
            {
                localJoystik = Joystik.Active;

                localJoystik.onMove += Move;
            }
        }
        #region Interactions
        private void Move(Vector3 dir)
        {

            // root to dir joystik local

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            Quaternion root = Quaternion.AngleAxis(angle, Vector3.forward);

            transform.rotation = Quaternion.Slerp(transform.rotation, root, 10 * Time.deltaTime);

            // move

            Vector2 moveVelocity = dir.normalized * 5;

            body.MovePosition(body.position + moveVelocity * Time.fixedDeltaTime);

        }

        #endregion

        public void Remove()
        {
            throw new System.NotImplementedException();
        }

        public void Remove(float time)
        {
           
        }

        public void SetSprite(Sprite sprite)
        {
            
        }

        // Use this for initialization
        void Start()
        {
            Init();
        }
    }
}