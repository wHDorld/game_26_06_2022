using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Features.Unit.Interfaces;
using UnityEngine;

namespace Features.Unit.Components
{
    [AddComponentMenu("Features/Unit/Transform Free Rotatement")]
    public class TransformFreeRotatement : MonoBehaviour, IRotatement
    {
        public float AngularSpeed = 1;

        new Transform transform;
        void Start()
        {
            transform = GetComponent<Transform>();
        }

        public void AddRotation(Vector3 euler)
        {
            transform.rotation *= Quaternion.Euler(euler * AngularSpeed);
        }

        public void RotateTowards(Vector3 dir)
        {
            transform.rotation = Quaternion.LookRotation(dir, transform.parent.up);
        }

        public void Stop()
        {

        }
    }
}
