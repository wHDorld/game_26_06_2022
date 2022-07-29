using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Features.Unit.Components;
using Features.Unit.Interfaces;
using Features.Unit.Abstract;
using Features.PInput.Interfaces;

namespace Features.PInput.Components
{
    [AddComponentMenu("Features/Player Input/Input Movement")]
    [RequireComponent(typeof(IMovement))]
    public class InputMovement : MonoBehaviour, IInputable
    {
        public string HorizontalAxis = "Horizontal";
        public string VerticalAxis = "Vertical";

        new Transform transform;
        IMovement movement;
        private void Start()
        {
            CacheInputFields();
            if (GetComponent<IMovement>() == null)
                gameObject.AddComponent<TransformMovement>();
            movement = GetComponent<IMovement>();
            transform = GetComponent<Transform>();
        }

        private void FixedUpdate()
        {
            movement.Move(inputDir);
        }

        public void CacheInputFields()
        {
            GlobalInputContainer.CacheAxis(HorizontalAxis);
            GlobalInputContainer.CacheAxis(VerticalAxis);
        }

        Vector3 inputDir
        {
            get
            {
                return
                        new Vector3(
                            GlobalInputContainer.GetAxis(HorizontalAxis),
                            0,
                            GlobalInputContainer.GetAxis(VerticalAxis)
                            );
            }
        }
    }
}
