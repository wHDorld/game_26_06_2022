using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Features.Unit.Components;
using Features.Unit.Interfaces;
using Features.PInput.Interfaces;

namespace Features.PInput.Components
{
    [AddComponentMenu("Features/Player Input/Input Rotatement")]
    [RequireComponent(typeof(IRotatement))]
    public class InputRotatement : MonoBehaviour, IInputable
    {
        public string XAxis = "Mouse Y";
        public string YAxis = "Mouse X";

        IRotatement movement;
        private void Start()
        {
            CacheInputFields();
            if (GetComponent<IRotatement>() == null)
                gameObject.AddComponent<TransformRotatement>();
            movement = GetComponent<IRotatement>();
        }

        private void Update()
        {
            movement.AddRotation(inputDir);
        }

        public void CacheInputFields()
        {
            GlobalInputContainer.CacheAxis(XAxis);
            GlobalInputContainer.CacheAxis(YAxis);
        }

        Vector3 inputDir
        {
            get
            {
                return new Vector3(
                    -GlobalInputContainer.GetAxis(XAxis),
                    GlobalInputContainer.GetAxis(YAxis)
                    );
            }
        }
    }
}
