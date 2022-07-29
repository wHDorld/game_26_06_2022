using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Features.Unit.Interfaces;
using UnityEngine;

namespace Features.Unit.Components
{
    [AddComponentMenu("Features/Unit/Character Controller Movement")]
    [RequireComponent(typeof(CharacterController))]
    public class CharacterControllerMovement : MonoBehaviour, IMovement
    {
        public float Speed;
        public bool IsLocal;
        new Transform transform;
        CharacterController characterController;
        private void Start()
        {
            characterController = GetComponent<CharacterController>();
            transform = GetComponent<Transform>();
        }

        public void Move(Vector3 dir)
        {
            if (IsLocal)
                characterController.SimpleMove(transform.TransformDirection(dir.normalized) * Speed);
            else
                characterController.SimpleMove(dir.normalized * Speed);
        }

        public void Stop()
        {

        }
    }
}
