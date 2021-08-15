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
        private Joystik _localJoystik;

        private Rigidbody2D _body;
    

        public override void InitCharacter()
        {
            base.InitCharacter();


            if (!Joystik.Active)
            {
                throw new CharacterException("Local joystik not found");
            }


            if (!_localJoystik)
            {
                _localJoystik = Joystik.Active;

            }


        }

        private void Update()
        {
            Move(localJoystik.InputDir);
            
            Rotate(localJoystik.InputDir);
        }
        
       void Start() => InitCharacter();
    }
}
