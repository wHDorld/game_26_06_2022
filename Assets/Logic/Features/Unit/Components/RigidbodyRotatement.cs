using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Features.Unit.Interfaces;
using UnityEngine;

namespace Features.Unit.Components
{
    [AddComponentMenu("Features/Unit/Rigidbody Rotatement")]
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyRotatement : MonoBehaviour, IRotatement
    {
        public float AngularSpeed = 1;
        public float StoppingPower = 1;
        public bool IgnoringMass;
        public ForceMode forceMode = ForceMode.Force;

        new Transform transform;
        new Rigidbody rigidbody;
        void Start()
        {
            transform = GetComponent<Transform>();
            rigidbody = GetComponent<Rigidbody>();
        }

        public void AddRotation(Vector3 euler)
        {
            euler *= AngularSpeed;
            if (IgnoringMass) euler *= rigidbody.mass;
            rigidbody.AddRelativeTorque(euler, forceMode);
        }

        public void RotateTowards(Vector3 dir)
        {
            rigidbody.AddTorque(
                (Quaternion.LookRotation(dir, Vector3.up) * Quaternion.Inverse(transform.rotation)).eulerAngles
                );
        }

        public void Stop()
        {
            rigidbody.AddTorque(-rigidbody.angularVelocity * rigidbody.mass * StoppingPower);
        }
    }
}
