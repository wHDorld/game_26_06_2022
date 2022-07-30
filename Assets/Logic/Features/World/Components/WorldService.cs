using Features.Ship.Entities;
using Features.Ship.Interfaces;
using Logic.Features.Ship.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Features.World.Components
{
    public class WorldService
    {
        public static UnityEngine.Object GetExternalShipByName(string name) => 
            Resources.Load(GetExternalPathByName(name));

        public static GameObject GetExternalShipGOByName(string name, ShipIdentityObject identity)
        {
            var result = UnityEngine.Object.Instantiate(GetExternalShipByName(name)) as GameObject;
            result.GetComponent<ShipIdentity>().ShipIdentityObject = identity;
            return result;
        }         

        public static UnityEngine.Object GetInternalShipByName(string name) =>
            Resources.Load(GetInternalPathByName(name));

        public static GameObject GetInternalShipGOByName(string name, ShipIdentityObject identity)
        {
            var result = UnityEngine.Object.Instantiate(GetInternalShipByName(name)) as GameObject;
            result.GetComponent<ShipIdentity>().ShipIdentityObject = identity;
            return result;
        }

        public static string GetExternalPathByName(string name) =>
            WorldContainer.ExternalPath + name + WorldContainer.ExternalTag;

        public static string GetInternalPathByName(string name) =>
            WorldContainer.InternalPath + name + WorldContainer.InternalTag;

        public static void ProvideShipConnection(GameObject ship, ShipIdentityObject identity)
        {
            ShipIdentity shipIdentity = null;

            if (ship.GetComponent<ShipIdentity>().ShipType == ShipType.Internal)
                shipIdentity = WorldContainer.ExternalScene.GetRootGameObjects()
                    .Select(x => x.GetComponent<ShipIdentity>())
                    .Where(x => x != null)
                    .Where(x => x.ShipIdentityObject.ShipId == identity.ShipId)
                    .FirstOrDefault();

            if (ship.GetComponent<ShipIdentity>().ShipType == ShipType.External)
                shipIdentity = WorldContainer.InternalScene.GetRootGameObjects()
                    .Select(x => x.GetComponent<ShipIdentity>())
                    .Where(x => x != null)
                    .Where(x => x.ShipIdentityObject.ShipId == identity.ShipId)
                    .FirstOrDefault();

            foreach (var a in ship.GetComponentsInChildren<IShipConnect>())
                a.Connect(shipIdentity);
        }
        public static void ProvideShipConnection(GameObject ship) =>
            ProvideShipConnection(ship, ship.GetComponent<ShipIdentity>().ShipIdentityObject);
    }
}
