using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features.Ship.Interfaces
{
    public interface IShipConnect
    {
        public void Connect(ShipIdentity shipIdentity);
    }
}
