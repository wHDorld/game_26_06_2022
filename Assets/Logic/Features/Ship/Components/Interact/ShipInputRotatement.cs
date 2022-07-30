using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Features.Unit.Entities;
using Features.Unit.Components;
using Features.Unit.Interfaces;
using Features.Ship.Interfaces;
using Features.Ship.Entities;

namespace Features.Ship.Components.Interact
{
    [RequireComponent(typeof(TransformValueClampedContainer))]
    public class ShipInputRotatement : MonoBehaviour, IShipConnect
    {
        public Vector2 ValueThreshold;

        IRotatement shipRotatement;
        ClampedValueE leaverX;
        ClampedValueE leaverY;
        ShipIdentity shipIdentity;

        public void Connect(ShipIdentity shipIdentity)
        {
            this.shipIdentity = shipIdentity;
            shipRotatement = shipIdentity.gameObject.GetComponent<IRotatement>();
        }

        void Start()
        {
            leaverX = GetComponent<TransformValueClampedContainer>().GetValues().FirstOrDefault(x => x.Name == "rot X");
            leaverY = GetComponent<TransformValueClampedContainer>().GetValues().FirstOrDefault(x => x.Name == "rot Y");
        }

        void Update()
        {
            float valX = leaverX.value - 0.5f;
            float valY = leaverY.value - 0.5f;
            if (valX > ValueThreshold.x && valX < ValueThreshold.y ||
                valY > ValueThreshold.x && valY < ValueThreshold.y)
                shipRotatement?.Stop();
            else
                shipRotatement?.AddRotation(new Vector3(valX, 0, valY));
        }
    }
}
