using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Features.PInput.Interfaces;
using Features.Interact.Interfaces;
using Features.Interact.Entities;

namespace Features.PInput.Components
{
    [AddComponentMenu("Features/Player Input/Mouse Input Interactable")]
    public class MouseInputIneractable : MonoBehaviour, IInputable
    {
        public string InteractAxis = "Fire1";
        public bool IsHold;
        public float MaxDistance = 1;
        public Transform Camera;

        bool isHoldingNow;
        IInteractHandler holdingHandler;
        float holdingDistance;
        float axisValue;
        private void Start()
        {
            CacheInputFields();
        }

        public void CacheInputFields()
        {
            GlobalInputContainer.CacheAxis(InteractAxis);
        }

        private void Update()
        {
            axisValue = GlobalInputContainer.GetAxis(InteractAxis);
            Interact();
        }

        private void Interact()
        {
            if (!IsInteracting())
            {
                isHoldingNow = false;
                return;
            }
            Vector3 holdingPoint = HoldingPoint();
            holdingHandler.Interact(new InteractMessage(
                gameObject,
                InteractAxis,
                axisValue,
                new List<float>() {
                    holdingPoint.x,
                    holdingPoint.y,
                    holdingPoint.z
                }
                ));
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(HoldingPoint(), 0.1f);
        }
        private bool IsInteracting()
        {
            if (axisValue < 0.5f) return false;
            if (isHoldingNow) return true;

            RaycastHit hit;
            if (!Physics.Raycast(new Ray(Camera.position, Camera.forward), out hit, MaxDistance))
                return false;
            if (hit.collider.gameObject.GetComponent<IInteractHandler>() == null) return false;

            holdingDistance = hit.distance;
            holdingHandler = hit.collider.gameObject.GetComponent<IInteractHandler>();
            isHoldingNow = IsHold;

            return true;
        }
        private Vector3 HoldingPoint()
        {
            return Camera.transform.position + Camera.forward * holdingDistance;
        }
    }
}
