﻿using Client;
using SO.Character;
using System.Collections;
using UnityEngine;

namespace Character
{
    public class CharacterBase : NetworkObject
    {
        #region Constants
        private const string PATH_DATA_DEFAULT_CHARACTER = "SO/Character/CharacterDefaultSettings";
        #endregion


        #region Fields
        protected Rigidbody2D body;
        #endregion


        #region Properties
        public CharacterDefaultData DefaultDataCharacter { get; protected set; }
        #endregion
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

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

        }

        #region Interactions
        public void Move(Vector3 dir)
        {
            if (dir == Vector3.zero)
            {
                return;
            }
            // root to dir joystik local

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            Quaternion root = Quaternion.AngleAxis(angle, Vector3.forward);

            transform.rotation = Quaternion.Slerp(transform.rotation, root, DefaultDataCharacter.DefaultSpeedRotating * Time.deltaTime);

            // move

            Vector2 moveVelocity = dir.normalized * DefaultDataCharacter.DefaultSpeedMovement;

            body.MovePosition(body.position + moveVelocity * Time.fixedDeltaTime);

        }



        public void SetSprite(Sprite sprite)
        {

        }

        #endregion
    }
}