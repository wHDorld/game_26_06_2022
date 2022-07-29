using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Features.Unit.Interfaces;

namespace Features.Unit.Components
{
    [AddComponentMenu("Features/Unit/Transform Movement")]
    public class TransformMovement : MonoBehaviour, IMovement
    {
        public float Speed = 0.1f;
        public bool IsLocal;

        new Transform transform;

        public void Start()
        {
            transform = GetComponent<Transform>();
        }

        public void Move(Vector3 dir)
        {
            dir = dir.normalized;
            if (!IsLocal)
                transform.Translate(new Vector3(dir.x, 0, dir.z) * Speed, Space.World);
            else
                transform.Translate(new Vector3(dir.x, 0, dir.z) * Speed, Space.Self);
        }

        public void Stop()
        {

        }
    }
}
