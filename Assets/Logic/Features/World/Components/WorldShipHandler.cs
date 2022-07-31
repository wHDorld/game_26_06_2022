using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using Features.World.Entities;
using System.Linq;
using Data.ScriptableObjects.Entities;
using Data.ScriptableObjects;
using System;
using Features.Ship.Entities;

namespace Features.World.Components
{
    [AddComponentMenu("Features/World/World Ship Handler")]
    [RequireComponent(typeof(WorldSceneHandler))]
    public class WorldShipHandler : MonoBehaviour
    {
        public static List<ShipConnect> ShipConnections = new List<ShipConnect>();

        private PlayerSaveDataContainer PlayerSaveDataContainer;
        private static WorldSceneHandler WorldSceneHandler;

        void Start()
        {
            WorldSceneHandler = GetComponent<WorldSceneHandler>();
            PlayerSaveDataContainer = (Resources.Load("SaveData/PlayerSaveData") as PlayerSaveData).LoadData();

            WorldSceneHandler.OnExternalSceneLoaded += OnExternalSceneLoaded;
            WorldSceneHandler.OnInternalSceneLoaded += OnInternalSceneLoaded;
            WorldSceneHandler.OnAllSceneLoaded += OnAllSceneLoaded;
        }

        private void OnAllSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            InitializeWorld();
        }
        private void OnExternalSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            //
        }
        private void OnInternalSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            //
        }

        public void InitializeWorld()
        {
            InitiateShipConnections();
        }

        void InitiateShipConnections()
        {
            //Player Initialize
            var inst = InstantiateShip("ShipTest");

            GameObject playerGO = Instantiate(Resources.Load("Prefabs/Player/Player")) as GameObject;

            playerGO.transform.SetParent(
                inst.InternalShip.GetComponentsInChildren<Transform>()
                .Where(x => x.name == "PlayerSpawnPoint")
                .FirstOrDefault().parent
                );
            playerGO.transform.position =
                inst.InternalShip.GetComponentsInChildren<Transform>()
                .Where(x => x.name == "PlayerSpawnPoint")
                .FirstOrDefault().position;

            WorldService.ProvideShipConnection(inst.InternalShip);
        }

        public static ShipConnect InstantiateShip(string name)
        {
            var identity = new ShipIdentityObject(Guid.NewGuid());

            GameObject externalGO = WorldService.GetExternalShipGOByName(name, identity);
            GameObject internalGO = WorldService.GetInternalShipGOByName(name, identity);

            SceneManager.MoveGameObjectToScene(externalGO, WorldContainer.ExternalScene);
            SceneManager.MoveGameObjectToScene(internalGO, WorldContainer.InternalScene);

            WorldService.ProvideShipConnection(externalGO);
            WorldService.ProvideShipConnection(internalGO);

            ShipConnections.Add(new ShipConnect(identity, externalGO, internalGO));

            return ShipConnections.Last();
        }
    }
}
