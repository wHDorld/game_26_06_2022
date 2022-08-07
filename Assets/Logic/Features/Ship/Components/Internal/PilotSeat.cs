using Features.Interact.Entities;
using Features.Interact.Interfaces;
using Features.Ship.Components.Interact;
using Features.Ship.Components.Interact.Interfaces;
using Features.Unit.Entities;
using Features.Unit.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Features.Ship.Components.Internal
{
    public class PilotSeat : MonoBehaviour, IMovement, ISwitchControllers<IMovement>
    {
        public GameObject ShipInputMovementGameObject;
        public GameObject ShipInputRotatementGameObject;

        private IShipInput ShipInputMovement;
        private IShipInput ShipInputRotatement;

        //Поинтеры для установки локальных пространств для перерасчета направления движения в глобальные координаты относительно рычагов
        public Transform ShipInputMovementPointer;
        public Transform ShipInputRotatementPointer;

        private IInteractHandler ShipInputMovementInteract;
        private IInteractHandler ShipInputRotatementInteract;

        private Transform ShipInputMovementTransform;
        private Transform ShipInputRotatementTransform;

        private IMovement savedController;
        private string currentKey;
        private bool isSwitch;

        private void Start()
        {
            ShipInputMovement = ShipInputMovementGameObject.GetComponent<IShipInput>();
            ShipInputRotatement = ShipInputRotatementGameObject.GetComponent<IShipInput>();

            ShipInputMovementTransform = ShipInputMovement.GetInputObject();
            ShipInputRotatementTransform = ShipInputRotatement.GetInputObject();

            ShipInputMovementInteract = ShipInputMovementTransform.GetComponent<IInteractHandler>();
            ShipInputRotatementInteract = ShipInputRotatementTransform.GetComponent<IInteractHandler>();
        }

        public void Move(Vector3 dir)
        {
            Vector3 pos = ShipInputRotatementPointer.position + ShipInputRotatementPointer.TransformDirection(dir) * 2f;
            InteractMessage message = new InteractMessage(
                gameObject,
                "",
                1,
                new List<float>() { pos.x, pos.y, pos.z }
                );

            ShipInputRotatementInteract.Interact(message);
        }

        public void Stop()
        {

        }

        public IMovement Switch(IMovement controller, string key)
        {
            if (isSwitch)
            {
                if (currentKey == key)
                    return Release();
                return controller;
            }

            currentKey = key;
            savedController = controller;
            isSwitch = true;
            return this;
        }

        public IMovement Release()
        {
            isSwitch = false;
            return savedController;
        }
    }
}
