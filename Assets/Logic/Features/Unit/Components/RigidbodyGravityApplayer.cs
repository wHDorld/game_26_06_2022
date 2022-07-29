using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Features.Unit.Interfaces;
using Features.Unit.Abstract;

namespace Features.Unit.Components
{
    [AddComponentMenu("Features/Unit/Rigidbody Gravity Applayer")]
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyGravityApplayer : MonoBehaviour, IGravityApplayer
    {
        public bool IsAutoApply;
        public bool IsLocal;
        public bool IsOnGroundChecking;
        public bool IgnoringMass = true;
        public float ApplyingPower = 0.05f;
        public Vector3 Gravity = new Vector3(0, -9.8f, 0);

        new Transform transform;
        new Rigidbody rigidbody;
        Transform parent;
        AStateContainer? stateContainer;
        float applyingPower_changable;
        void Start()
        {
            stateContainer = GetComponent<AStateContainer>();
            IsOnGroundChecking = IsOnGroundChecking && (stateContainer != null);
            applyingPower_changable = IsOnGroundChecking ? 0 : ApplyingPower;

            transform = GetComponent<Transform>();
            rigidbody = GetComponent<Rigidbody>();
            parent = gameObject.GetComponentsInParent<Transform>().Skip(1).FirstOrDefault(x => x.tag != "Cabin");
            GetComponent<Rigidbody>().useGravity = false;
        }
        void FixedUpdate()
        {
            if (!IsAutoApply)
                return;
            GroundChecking();
            ApplyGravity();
        }

        public void ApplyGravity()
        {
            float massMul = IgnoringMass ? rigidbody.mass : 1;
            if (IsLocal)
                rigidbody.AddForce(parent.TransformDirection(Gravity) * applyingPower_changable * massMul, ForceMode.Force);
            else
                rigidbody.AddForce(Gravity * applyingPower_changable * massMul, ForceMode.Force);
        }
        public void GroundChecking()
        {
            if (IsOnGroundChecking)
                applyingPower_changable = stateContainer.IsOnGround ?
                    0 :
                    Mathf.Lerp(applyingPower_changable, ApplyingPower, 1f * Time.deltaTime);
        }

        public void ChangeGravity(Vector3 dir, float power)
        {
            Gravity = dir.normalized * power;
        }
    }
}
