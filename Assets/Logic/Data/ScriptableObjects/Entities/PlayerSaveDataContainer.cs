using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ScriptableObjects.Entities
{
    [Serializable]
    public class PlayerSaveDataContainer
    {
        public string CurrentPlayerShip = "ShipTest";

        public string CurrentPlayerShipExternal { get { return CurrentPlayerShip + "External"; } }
        public string CurrentPlayerShipInternal { get { return CurrentPlayerShip + "Internal"; } }
    }
}
