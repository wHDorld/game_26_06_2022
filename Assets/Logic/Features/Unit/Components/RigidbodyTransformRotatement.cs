using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Features.Unit.Interfaces;
using UnityEngine;

namespace Features.Unit.Components
{
    [AddComponentMenu("Features/Unit/Rigidbody Transform Rotatement")]
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyTransformRotatement : MonoBehaviour, IRotatement
    {
        public float AngularSpeed = 1;
        public float StoppingPower = 1;

        new Transform transform;
        new Rigidbody rigidbody;
        void Start()
        {
            transform = GetComponent<Transform>();
            rigidbody = GetComponent<Rigidbody>();
        }

        public void AddRotation(Vector3 euler)
        {
            rigidbody.rotation *= Quaternion.Euler(euler * AngularSpeed);
        }

        public void RotateTowards(Vector3 dir)
        {
            rigidbody.rotation = Quaternion.LookRotation(dir, transform.parent.up);
        }

        public void Stop()
        {
            rigidbody.AddTorque(-rigidbody.angularVelocity * rigidbody.mass * StoppingPower);
        }
    }
}
