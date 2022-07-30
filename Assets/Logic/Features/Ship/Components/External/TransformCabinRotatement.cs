using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Features.Ship.Intefaces;

namespace Features.Ship.Components.External
{
    [AddComponentMenu("Features/Ship/Transform Cabin Rotatement")]
    public class TransformCabinRotatement : MonoBehaviour, ICabinRotatement
    {
        public bool IsAutoApply;
        public float RotateSpeed;

        new Transform transform;
        Transform parent;
        Quaternion baseRotate;
        Quaternion savedRotate;
        void Start()
        {
            transform = GetComponent<Transform>();
            parent = transform.parent;
            baseRotate = transform.localRotation;
        }
        void Update()
        {
            if (!IsAutoApply)
                return;
            Rotate();
        }

        public void Rotate()
        {
            transform.localRotation = baseRotate * currentRotate;
            savedRotate = Quaternion.LerpUnclamped(savedRotate, parent.rotation, RotateSpeed * Time.deltaTime);
        }

        Quaternion currentRotate
        {
            get
            {
                return Quaternion.Inverse(parent.rotation) * savedRotate;
            }
        }
    }
}