using GameJoystik;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterController : CharacterBase, ISeterSprite, IInitObject
    {
        private Joystik _localJoystik;

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
            Move(_localJoystik.InputDir);
            
            Rotate(_localJoystik.InputDir);
        }
        
       void Start() => InitCharacter();
    }
}
