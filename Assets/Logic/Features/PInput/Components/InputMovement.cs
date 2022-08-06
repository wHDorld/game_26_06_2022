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
using Features.Ship.Components.Internal;

namespace Features.PInput.Components
{
    [AddComponentMenu("Features/Player Input/Input Movement")]
    [RequireComponent(typeof(IMovement))]
    public class InputMovement : MonoBehaviour, IInputable
    {
        public string HorizontalAxis = "Horizontal";
        public string VerticalAxis = "Vertical";
        public string SecondInputAxis = "E";

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
        private void Update()
        {
            if (GlobalInputContainer.GetAxis(SecondInputAxis) > 0.5f)
            {
                FindPilotSeat();
            }
        }

        void FindPilotSeat()
        {
            Debug.Log("findpilotseat");
            var pilotSeat = FindObjectsOfType<PilotSeat>()?
                .Where(x => Vector3.Distance(x.transform.position, transform.position) < 3)
                .FirstOrDefault();

            if (pilotSeat == null)
                return;

            movement = pilotSeat.Switch(movement, "player");
        }

        public void CacheInputFields()
        {
            GlobalInputContainer.CacheAxis(HorizontalAxis);
            GlobalInputContainer.CacheAxis(VerticalAxis);
            GlobalInputContainer.CacheAxis(SecondInputAxis, true);
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
