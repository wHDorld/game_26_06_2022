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
    [AddComponentMenu("Features/Unit/Rigidbody Transform Movement")]
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyTransformMovement : MonoBehaviour, IMovement
    {
        public float Speed = 0.1f;
        public float StopingPower = 1;
        public bool IsLocal;

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
            if (IsLocal)
                rigidbody.position += transform.TransformDirection(dir) * Speed;
            else
                rigidbody.position += dir * Speed;
        }

        public void Stop()
        {
            rigidbody.AddForce(-rigidbody.velocity * rigidbody.mass * StopingPower);
        }
    }
}
