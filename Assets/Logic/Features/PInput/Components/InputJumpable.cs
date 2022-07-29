using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Features.Unit.Interfaces;
using Features.PInput.Interfaces;
using Features.Unit.Abstract;

namespace Features.PInput.Components
{
    [AddComponentMenu("Features/Player Input/Input Jumpable")]
    [RequireComponent(typeof(IJumpable))]
    public class InputJumpable : MonoBehaviour, IInputable
    {
        public string JumpAxis = "Jump";
        public float Force = 1;

        IJumpable jumpable;
        AStateContainer? stateContainer;

        void Start()
        {
            CacheInputFields();
            jumpable = GetComponent<IJumpable>();
            stateContainer = GetComponent<AStateContainer>();
        }

        void FixedUpdate()
        {
            Jump();
        }

        bool alreadyPressedJump = false;
        void Jump()
        {
            if (GlobalInputContainer.GetAxis(JumpAxis) < 0.5f)
            {
                alreadyPressedJump = false;
                return;
            }
            if (alreadyPressedJump) return;
            if (!stateContainer?.IsOnGround ?? false) return;

            jumpable.Jump(Vector3.up, Force);
            alreadyPressedJump = true;
        }

        public void CacheInputFields()
        {
            GlobalInputContainer.CacheAxis(JumpAxis);
        }
    }
}
