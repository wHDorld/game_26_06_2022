using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using Data.ScriptableObjects.Entities;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Player Save Data", menuName = "Player save data")]
    public class PlayerSaveData : ScriptableObject
    {
        public PlayerSaveDataContainer DataContainer;

        public PlayerSaveDataContainer SaveData()
        {
            using (FileStream fs = new FileStream(Application.persistentDataPath + "/Saves/PlayerSave", FileMode.OpenOrCreate))
            {
                (new BinaryFormatter()).Serialize(fs, DataContainer);
            }

            return DataContainer;
        }
        public PlayerSaveDataContainer LoadData()
        {
            if (!Directory.Exists(Application.persistentDataPath + "/Saves"))
                Directory.CreateDirectory(Application.persistentDataPath + "/Saves");

            if (!File.Exists(Application.persistentDataPath + "/Saves/PlayerSave.savedata"))
                return SaveData();

            using (FileStream fs = new FileStream(Application.persistentDataPath + "/Saves/PlayerSave.savedata", FileMode.OpenOrCreate))
            {
                DataContainer = (PlayerSaveDataContainer)(new BinaryFormatter()).Deserialize(fs);
                DataContainer = DataContainer ?? new PlayerSaveDataContainer();
            }

            return DataContainer;
        }
    }
}
