using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Features.Interact.Interfaces;
using Features.Interact.Entities;
using Features.Unit.Interfaces;
using UnityEngine;

namespace Features.Interact.Components
{
    [RequireComponent(typeof(IInteractHandler))]
    [RequireComponent(typeof(IRotatement))]
    [AddComponentMenu("Features/Interact/Interact To Rotate")]
    public class InteractToRotate : MonoBehaviour, IInteractable
    {
        public bool IsAutoResetable;
        public float AutoResetSpeed;

        new Transform transform;
        IRotatement rotatement;
        Quaternion original_rotate;
        private void Start()
        {
            transform = GetComponent<Transform>();
            rotatement = GetComponent<IRotatement>();
            original_rotate = transform.localRotation;
        }

        private void LateUpdate()
        {
            if (isMessaging)
            {
                isMessaging = false;
                return;
            }
            if (IsAutoResetable)
                Reset();
        }
        public void Reset()
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, original_rotate, AutoResetSpeed * Time.deltaTime);
        }

        bool isMessaging = false;
        public void GetMessage(InteractMessage message)
        {
            if (message.anotherValues.Count < 3) return;
            Vector3 to = new Vector3(message.anotherValues[0], message.anotherValues[1], message.anotherValues[2]);
            rotatement.RotateTowards(to - transform.position);
            isMessaging = true;
        }
    }
}
