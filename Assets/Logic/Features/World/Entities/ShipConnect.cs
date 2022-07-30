using Features.Ship.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Features.World.Entities
{
    public class ShipConnect
    {
        public ShipIdentityObject ShipIdentityObject;
        public GameObject ExternalShip;
        public GameObject InternalShip;

        public ShipConnect(
            ShipIdentityObject ShipIdentityObject,
            GameObject ExternalShip,
            GameObject InternalShip
            )
        {
            this.ShipIdentityObject = ShipIdentityObject;
            this.ExternalShip = ExternalShip;
            this.InternalShip = InternalShip;
        }
    }
}
