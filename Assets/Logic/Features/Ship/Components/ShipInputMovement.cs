using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Features.Unit.Entities;
using Features.Unit.Components;
using Features.Unit.Interfaces;

namespace Features.Ship.Components
{
    [RequireComponent(typeof(TransformValueClampedContainer))]
    public class ShipInputMovement : MonoBehaviour
    {
        public Vector3 Forward = new Vector3(0, 0, 1);
        public Vector2 ValueThreshold;

        IMovement shipMovement;
        ClampedValueE leaver;
        new Transform transform;
        void Start()
        {
            GameObject ship = gameObject.transform.GetComponentsInParent<Transform>().FirstOrDefault(x => x.tag == "Ship").gameObject;
            shipMovement = ship.GetComponent<IMovement>();
            transform = ship.GetComponent<Transform>();
            leaver = GetComponent<TransformValueClampedContainer>().GetValues().FirstOrDefault(x => x.Name == "rot X");
        }

        void FixedUpdate()
        {
            float val = leaver.value - 0.5f;
            if (val > ValueThreshold.x && val < ValueThreshold.y)
                shipMovement.Stop();
            else
                shipMovement.Move(Forward * (leaver.value - 0.5f));
        }
    }
}
