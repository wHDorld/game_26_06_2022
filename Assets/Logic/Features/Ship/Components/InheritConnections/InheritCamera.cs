using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Features.Ship.Components.InheritConnections
{
    public class InheritCamera : InheritTransform
    {
        public bool IsFovInheriting = true;

        Camera myCamera;
        Camera otherCamera;

        public override void Connect(ShipIdentity shipIdentity)
        {
            base.Connect(shipIdentity);

            myCamera = GetComponent<Camera>();
            otherCamera = shipIdentity.GetComponentsInChildren<Transform>()
                .Where(x => x.name == OtherName)
                .FirstOrDefault().GetComponent<Camera>();

            if (myCamera == null || otherCamera == null)
                return;
            if (!IsFovInheriting)
                return;
            otherCamera.fieldOfView = myCamera.fieldOfView;
        }
    }
}
