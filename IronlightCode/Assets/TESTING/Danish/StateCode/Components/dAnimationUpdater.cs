using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace brian.Components
{
    public class dAnimationUpdater
    {
        private Animator anim;
        private Vector2 moveBlendValue = Vector2.zero;

        public void Init(Animator _anim)
        {
            anim = _anim;
        }

        public void Tick(Vector2 moveInput)
        {
            UpdateMovementBlendTree(moveInput);
        }

        void UpdateMovementBlendTree(Vector2 vector)
        {
            if(moveBlendValue != vector)
            {
                moveBlendValue = Vector2.Lerp(moveBlendValue, vector, Time.deltaTime*5);
            }

            anim.SetFloat("Forward", moveBlendValue.y);
            anim.SetFloat("Strafe", moveBlendValue.x);
        }
    }
}