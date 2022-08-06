using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Features.Unit.Entities;
using Features.Unit.Components;
using Features.Unit.Interfaces;
using Features.Ship.Interfaces;
using Features.Ship.Entities;
using Features.Ship.Components.Interact.Interfaces;

namespace Features.Ship.Components.Interact
{
    [AddComponentMenu("Features/Ship/Interact/Ship Input Rotatement")]
    [RequireComponent(typeof(TransformValueClampedContainer))]
    public class ShipInputRotatement : MonoBehaviour, IShipConnect, IShipInput
    {
        public Vector2 ValueThreshold;
        public Vector2 ValueMultiply;

        IRotatement shipRotatement;
        ClampedValueE leaverX;
        ClampedValueE leaverY;
        ShipIdentity shipIdentity;
        new Transform transform;

        public void Connect(ShipIdentity shipIdentity)
        {
            this.shipIdentity = shipIdentity;
            shipRotatement = shipIdentity.gameObject.GetComponent<IRotatement>();
        }

        void Start()
        {
            transform = gameObject.transform;
            leaverX = GetComponent<TransformValueClampedContainer>().GetValues().FirstOrDefault(x => x.Name == "rot X");
            leaverY = GetComponent<TransformValueClampedContainer>().GetValues().FirstOrDefault(x => x.Name == "rot Y");
        }

        void Update()
        {
            float valX = leaverX.value - 0.5f;
            float valY = leaverY.value - 0.5f;
            if ((valX > ValueThreshold.x && valX < ValueThreshold.y)
                && (valY > ValueThreshold.x && valY < ValueThreshold.y))
                shipRotatement?.Stop();
            else
                shipRotatement?.AddRotation(new Vector3(valX * ValueMultiply.x, 0, valY * ValueMultiply.y));
        }

        public List<ClampedValueE> GetClampedValue()
        {
            return new List<ClampedValueE>() { leaverX, leaverY };
        }

        public Transform GetInputObject()
        {
            return transform;
        }
    }
}
