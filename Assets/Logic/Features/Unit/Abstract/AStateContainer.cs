using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Features.Unit.Enums;

namespace Features.Unit.Abstract
{
    public abstract class AStateContainer : MonoBehaviour
    {
        public bool IsOnGroundChecking = true;
        public RaycastType RaycastType;
        public float GroundRayLength = 0.1f;
        public int GroundRayDensity = 3;
        public float GroundRayfieldRadius = 1;
        [Space(20)]

        new Transform transform;
        private void Start()
        {
            transform = GetComponent<Transform>();
        }
        private void FixedUpdate()
        {
            OnGroundUpdate();
        }

        #region IS ON GROUND
        bool isOnGround;
        public bool IsOnGround { get { return isOnGround; } }

        private void OnGroundUpdate()
        {
            if (!IsOnGroundChecking) return;
            switch (RaycastType)
            {
                case RaycastType.Ray: RaycastingGroundCheck(); break;
                case RaycastType.Sphere: SphereGroundCheck(); break;
            }
        }
        private void RaycastingGroundCheck()
        {
            RaycastHit hit;
            Ray ray;
            for (float y = -GroundRayfieldRadius; y <= GroundRayfieldRadius; y += (GroundRayfieldRadius * 2) / (GroundRayDensity - 1))
            {
                for (float x = -GroundRayfieldRadius; x <= GroundRayfieldRadius; x += (GroundRayfieldRadius * 2) / (GroundRayDensity - 1))
                {
                    ray = new Ray(transform.TransformPoint(new Vector3(x, 0, y)), -transform.up);
                    isOnGround = Physics.Raycast(
                                ray,
                                out hit,
                                GroundRayLength
                                );
                    Debug.DrawRay(ray.origin, ray.direction);
                    if (isOnGround) break;
                }
                if (isOnGround) break;
            }
        }
        private void SphereGroundCheck()
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, -transform.up);
            isOnGround = Physics.SphereCast(
                        ray,
                        GroundRayfieldRadius, 
                        out hit,
                        GroundRayLength
                        );
            Debug.DrawRay(ray.origin, ray.direction);
        }
        #endregion
    }
}
