using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Features.Unit.Interfaces;
using UnityEngine;

namespace Features.Unit.Components
{
    [AddComponentMenu("Features/Unit/Character Controller Gravity Applayer")]
    [RequireComponent(typeof(CharacterController))]
    public class CharacterControllerGravityApplayer : MonoBehaviour, IGravityApplayer
    {
        public bool IsAutoApply;
        public bool IsOnGroundChecking;
        public bool IsLocal;
        public float ApplyingPower = 0.05f;
        public Vector3 Gravity = new Vector3(0, -9.8f, 0);

        new Transform transform;
        CharacterController characterController;

        public void ApplyGravity()
        {
            if (IsLocal)
                characterController.SimpleMove(transform.TransformDirection(Gravity));
            else
                characterController.SimpleMove(Gravity);
        }

        public void ChangeGravity(Vector3 dir, float power)
        {
            Gravity = dir.normalized * power;
        }

        private void Start()
        {
            characterController = GetComponent<CharacterController>();
            transform = GetComponent<Transform>();
        }
        private void Update()
        {
            if (!IsAutoApply)
                return;
            ApplyGravity();
        }

    }
}
