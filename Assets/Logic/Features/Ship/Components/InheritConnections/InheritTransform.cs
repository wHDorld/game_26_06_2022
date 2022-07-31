using Features.Ship.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Features.Ship.Components.InheritConnections
{
    public class InheritTransform : MonoBehaviour, IShipConnect
    {
        public string OtherName;
        public bool IsLocal = true;
        public bool IsPositionInheriting = true;
        public bool IsRotationInheriting = true;
        public bool IsScaleInheriting = false;

        ShipIdentity ShipIdentity = null;
        Transform myShipTransform;
        Transform myTransform;
        Transform otherTransform;

        public virtual void Connect(ShipIdentity shipIdentity)
        {
            this.ShipIdentity = shipIdentity;

            myTransform = transform;
            myShipTransform = GetComponentInParent<ShipIdentity>().transform;
            otherTransform = shipIdentity.GetComponentsInChildren<Transform>()
                .Where(x => x.name == OtherName)
                .FirstOrDefault();
        }

        public void Update()
        {
            InheritUpdate();
        }

        public virtual void InheritUpdate()
        {
            if (ShipIdentity == null)
                return;

            InheritPosition();
            InheritRotation();
            InheritScale();
        }

        private void InheritPosition()
        {
            if (!IsPositionInheriting)
                return;

            if (IsLocal)
                otherTransform.localPosition = myShipTransform.InverseTransformPoint(myTransform.position);
            else
                otherTransform.position = myTransform.position;
        }
        private void InheritRotation()
        {
            if (!IsRotationInheriting)
                return;

            if (IsLocal)
                otherTransform.localRotation = Quaternion.LookRotation(myShipTransform.InverseTransformDirection(myTransform.forward));
            else
                otherTransform.rotation = myTransform.rotation;
        }
        private void InheritScale()
        {
            if (!IsScaleInheriting)
                return;

            if (IsLocal)
                otherTransform.localScale = myTransform.localScale;
            else
                otherTransform.localScale = myTransform.lossyScale;
        }
    }
}
