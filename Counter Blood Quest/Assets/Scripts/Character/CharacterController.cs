using Client;
using GameJoystik;
using System;
using System.Collections;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterController : CharacterBase, ISeterSprite, IInitObject
    {
        #region Fields
        private Joystik localJoystik;

        private Rigidbody2D body;

        #endregion


        // Use this for initialization
        void Start()
        {
            InitCharacter();
        }

        public override void InitCharacter()
        {
            base.InitCharacter();


            if (!Joystik.Active)
            {
                throw new CharacterException("Local joystik not found");
            }


            if (!localJoystik)
            {
                localJoystik = Joystik.Active;

            }


        }

        private void Update()
        {
            Move(localJoystik.InputDir);
        }
    }
}