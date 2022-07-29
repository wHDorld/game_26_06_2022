using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Features.Unit.Interfaces;
using UnityEngine;

namespace Features.Unit.Components
{
    [AddComponentMenu("Features/Unit/Transform Rotatement")]
    public class TransformRotatement : MonoBehaviour, IRotatement
    {
        public bool IsConstrainX;
        public Vector2 X_Constraints;
        [Space(1)]
        public bool IsConstrainY;
        public Vector2 Y_Constraints;
        [Space(1)]
        public bool IsConstrainZ;
        public Vector2 Z_Constraints;
        [Space(2)]
        public float AngularSpeed = 1;

        new Transform transform;
        Quaternion center;
        void Start()
        {
            transform = GetComponent<Transform>();
            center = transform.localRotation;
        }

        public void AddRotation(Vector3 euler)
        {
            transform.localEulerAngles += euler * AngularSpeed;
            ApplyConstraints();
            //euler *= AngularSpeed;
            /*Quaternion xRotation = Quaternion.AngleAxis(euler.x, Vector3.right);
            Quaternion yRotation = Quaternion.AngleAxis(euler.y, Vector3.up);
            Quaternion zRotation = Quaternion.AngleAxis(euler.z, Vector3.forward);

            if (!IsConstrainX || IsRotationValid(xRotation, X_Constraints)) transform.localRotation *= xRotation;
            if (!IsConstrainY || IsRotationValid(yRotation, Y_Constraints)) transform.localRotation *= yRotation;
            if (!IsConstrainZ || IsRotationValid(zRotation, Z_Constraints)) transform.localRotation *= zRotation;*/
        }

        public void RotateTowards(Vector3 dir)
        {
            transform.rotation = Quaternion.LookRotation(dir, transform.parent.up);

            ApplyConstraints();
        }

        bool IsRotationValid(Quaternion rotation, Vector2 constraints)
        {
            float angle = Quaternion.Angle(center, rotation * transform.localRotation);
            return angle < constraints.y && angle > constraints.x;
        }
        void ApplyConstraints()
        {
            Func<Vector2, bool, float, float> constrain = (c, isC, val) =>
            {
                if (!isC)
                    return val;

                float ret;
                if (val > 180) val -= 360;
                ret = val > c.y ? c.y : (val < c.x ? c.x : val);
                if (ret < 0) ret += 360;

                return ret;
            };
            Vector3 euler = transform.localEulerAngles - center.eulerAngles;

            euler.x = constrain(X_Constraints, IsConstrainX, euler.x);
            euler.y = constrain(Y_Constraints, IsConstrainY, euler.y);
            euler.z = constrain(Z_Constraints, IsConstrainZ, euler.z);

            transform.localEulerAngles = euler + center.eulerAngles;
        }

        public void Stop()
        {

        }
    }
}
