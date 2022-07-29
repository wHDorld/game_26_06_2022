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
    public class ShipInputRotatement : MonoBehaviour
    {
        public Vector2 ValueThreshold;

        IRotatement shipRotatement;
        ClampedValueE leaverX;
        ClampedValueE leaverY;
        new Transform transform;
        void Start()
        {
            GameObject ship = gameObject.transform.GetComponentsInParent<Transform>().FirstOrDefault(x => x.tag == "Ship").gameObject;
            shipRotatement = ship.GetComponent<IRotatement>();
            transform = ship.GetComponent<Transform>();
            leaverX = GetComponent<TransformValueClampedContainer>().GetValues().FirstOrDefault(x => x.Name == "rot X");
            leaverY = GetComponent<TransformValueClampedContainer>().GetValues().FirstOrDefault(x => x.Name == "rot Y");
        }

        void Update()
        {
            float valX = leaverX.value - 0.5f;
            float valY = leaverY.value - 0.5f;
            if (valX > ValueThreshold.x && valX < ValueThreshold.y ||
                valY > ValueThreshold.x && valY < ValueThreshold.y)
                shipRotatement.Stop();
            else
                shipRotatement.AddRotation(new Vector3(valX, 0, valY));
        }
    }
}
