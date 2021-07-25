using CameraGame;
using Character.Data;
using Client;
using Inventory;
using Photon.Pun;
using SO.Character;
using System.Collections;
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
        protected Rigidbody2D body;

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

            if (!TryGetComponent(out body))
            {
                throw new CharacterException($"character {name} not have component Rightbody2D");
            }

            CurrentDataCharacter = new CharacterDynamicData(DefaultDataCharacter);

            Inventory = new InventoryCharacter();

            FindObjectOfType<GameCamera>().SetTarget(transform);

        }

        #region Interactions
        public void Move(Vector3 dir)
        {
            if (dir == Vector3.zero)
            {
                return;
            }
           
            
            // move

            Vector2 moveVelocity = dir.normalized * DefaultDataCharacter.DefaultSpeedMovement;

            body.MovePosition(body.position + moveVelocity * Time.fixedDeltaTime);

        }



        public void SetSprite(Sprite sprite)
        {
         // нет реализации
        }
        
        public void Rotate(Vector3 dir) {
        
        
            // root to dir joystik local

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            Quaternion root = Quaternion.AngleAxis(angle, Vector3.forward);

            transform.rotation = Quaternion.Slerp(transform.rotation, root, DefaultDataCharacter.DefaultSpeedRotating * Time.deltaTime);
        }

        #endregion
    }
}
