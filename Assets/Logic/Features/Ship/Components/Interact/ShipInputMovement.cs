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
    [AddComponentMenu("Features/Ship/Interact/Ship Input Movement")]
    [RequireComponent(typeof(TransformValueClampedContainer))]
    public class ShipInputMovement : MonoBehaviour, IShipConnect, IShipInput
    {
        public Vector3 Forward = new Vector3(0, 0, 1);
        public Vector2 ValueThreshold;

        IMovement shipMovement;
        ClampedValueE leaver;
        ShipIdentity shipIdentity;
        new Transform transform;

        public void Connect(ShipIdentity shipIdentity)
        {
            this.shipIdentity = shipIdentity;
            shipMovement = shipIdentity.gameObject.GetComponent<IMovement>();
        }

        void Start()
        {
            transform = gameObject.transform;
            leaver = GetComponent<TransformValueClampedContainer>().GetValues().FirstOrDefault(x => x.Name == "rot X");
        }

        void FixedUpdate()
        {
            float val = leaver.value - 0.5f;
            if (val > ValueThreshold.x && val < ValueThreshold.y)
                shipMovement?.Stop();
            else
                shipMovement?.Move(Forward * (leaver.value - 0.5f));
        }

        public List<ClampedValueE> GetClampedValue()
        {
            return new List<ClampedValueE>() { leaver };
        }

        public Transform GetInputObject()
        {
            return transform;
        }
    }
}
