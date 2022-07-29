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
    [AddComponentMenu("Features/Unit/Transform Gravity Applayer")]
    public class TransformGravityApplayer : MonoBehaviour, IGravityApplayer
    {
        public bool IsAutoApply;
        public bool IsLocal;
        public bool IsOnGroundChecking;
        public float ApplyingPower = 0.05f;
        public Vector3 Gravity = new Vector3(0, -9.8f, 0);

        new Transform transform;
        Transform parent;
        AStateContainer? stateContainer;
        float applyingPower_changeble;
        void Start()
        {
            if (GetComponent<Rigidbody>()) GetComponent<Rigidbody>().useGravity = false;
            stateContainer = GetComponent<AStateContainer>();
            IsOnGroundChecking = IsOnGroundChecking && (stateContainer != null);
            applyingPower_changeble = IsOnGroundChecking ? 0 : ApplyingPower;

            transform = GetComponent<Transform>();
            parent = gameObject.GetComponentsInParent<Transform>().Skip(1).FirstOrDefault(x => x.tag != "Cabin");
        }
        void Update()
        {
            if (!IsAutoApply)
                return;
            GroundChecking();
            ApplyGravity();
        }

        public void ApplyGravity()
        {
            if (IsLocal)
                transform.position += parent.TransformDirection(Gravity) * applyingPower_changeble;
            else
                transform.position += Gravity * applyingPower_changeble;
        }
        public void ChangeGravity(Vector3 dir, float power)
        {
            Gravity = dir.normalized * power;
        }
        public void GroundChecking()
        {
            if (IsOnGroundChecking)
                applyingPower_changeble = stateContainer.IsOnGround ?
                    0 :
                    Mathf.Lerp(applyingPower_changeble, ApplyingPower, 1f * Time.deltaTime);
        }

    }
}
