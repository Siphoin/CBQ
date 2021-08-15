using CameraGame;
using Character.Data;
using Client;
using Inventory;
using Photon.Pun;
using SO.Character;
using System;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(PhotonTransformView))]
    public class CharacterBase : NetworkObject
    {
        #region Constants
        private const string PATH_DATA_DEFAULT_CHARACTER = "SO/Character/CharacterDefaultSettings";
        #endregion


        #region Fields
        protected Rigidbody2D _body;

        #endregion


        #region Properties
        public CharacterDefaultData DefaultDataCharacter { get; private set; }

        public CharacterDynamicData CurrentDataCharacter { get; private set; }

        public InventoryCharacter Inventory { get; private set; }
        #endregion

        public virtual void InitCharacter ()
        {
            Init();

            DefaultDataCharacter = Resources.Load<CharacterDefaultData>(PATH_DATA_DEFAULT_CHARACTER);

            if (!DefaultDataCharacter)
            {
                throw new CharacterException("default data character not found");
            }

            if (!TryGetComponent(out _body))
            {
                throw new CharacterException($"character {name} not have component Rightbody2D");
            }

            CurrentDataCharacter = new CharacterDynamicData(DefaultDataCharacter);

            Inventory = new InventoryCharacter();

            FindObjectOfType<GameCamera>().SetTarget(transform);

        }
        /// <summary>
        /// move character to point
        /// </summary>
        /// <param name="dir">target point</param>
        #region Interactions
        public void Move(Vector3 dir)
        {
            if (dir == Vector3.zero)
            {
                return;
            }
            
            // move

            Vector2 moveVelocity = dir.normalized * DefaultDataCharacter.DefaultSpeedMovement;

            _body.MovePosition(_body.position + moveVelocity * Time.fixedDeltaTime);

        }

        /// <summary>
        /// rotate character to angle
        /// </summary>
        /// <param name="dir">direction target angle</param>
        public void Rotate(Vector3 dir) {
       
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            Quaternion root = Quaternion.AngleAxis(angle, Vector3.forward);

            transform.rotation = Quaternion.Slerp(transform.rotation, root, DefaultDataCharacter.DefaultSpeedRotating * Time.deltaTime);
        }
        public void SetSprite(Sprite sprite) => throw new NotImplementedException();

        #endregion
    }
}
