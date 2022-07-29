using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Features.Unit.Interfaces;
using System.Collections;

namespace Features.Unit.Components
{
    [AddComponentMenu("Features/Unit/Rigidbody Movement")]
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyMovement : MonoBehaviour, IMovement
    {
        public float Speed = 0.1f;
        public float StopingPower = 1;
        public bool IgnoringMass;
        public bool RelativeForce = false;
        public ForceMode forceMode = ForceMode.Force;

        new Transform transform;
        new Rigidbody rigidbody;

        public void Start()
        {
            transform = GetComponent<Transform>();
            rigidbody = GetComponent<Rigidbody>();
        }

        public void Move(Vector3 dir)
        {
            dir = dir.normalized;
            if (IgnoringMass) dir *= rigidbody.mass;
            if (!RelativeForce)
                rigidbody.AddForce(dir * Speed, forceMode);
            else
                rigidbody.AddRelativeForce(dir * Speed, forceMode);
        }

        public void Stop()
        {
            rigidbody.AddForce(-rigidbody.velocity * rigidbody.mass * StopingPower);
        }
    }
}
