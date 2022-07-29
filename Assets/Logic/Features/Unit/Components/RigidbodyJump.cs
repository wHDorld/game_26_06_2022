using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Features.Unit.Interfaces;

namespace Features.Unit.Components
{
    [AddComponentMenu("Features/Unit/Rigidbody Jump")]
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyJump : MonoBehaviour, IJumpable
    {
        public bool IsRelative;
        public ForceMode forceMode;

        new Rigidbody rigidbody;
        IGravityApplayer? gravity;
        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            gravity = GetComponent<IGravityApplayer>();
        }

        public void Jump(Vector3 dir, float force)
        {
            if (IsRelative)
                rigidbody.AddRelativeForce(dir * force, forceMode);
            else
                rigidbody.AddForce(dir * force, forceMode);
        }
    }
}
