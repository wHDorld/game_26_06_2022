using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features.Ship.Entities
{
    [Serializable]
    public struct ShipIdentityObject
    {
        public Guid ShipId;

        public ShipIdentityObject(Guid ShipId)
        {
            this.ShipId = ShipId;
        }
    }
}
