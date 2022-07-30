using Data.ScriptableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Features.World.Components
{
    [RequireComponent(typeof(WorldShipHandler))]
    public class WorldSceneHandler : MonoBehaviour
    {
        public void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;

            SceneManager.LoadScene(WorldContainer.ExternalSceneName, LoadSceneMode.Additive);
            SceneManager.LoadScene(WorldContainer.InternalSceneName, LoadSceneMode.Additive);
        }
        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            if (arg0.name.Contains(WorldContainer.ExternalTag))
            {
                WorldContainer.ExternalScene = arg0;
                OnExternalSceneLoaded?.Invoke(arg0, arg1);
            }
            if (arg0.name.Contains(WorldContainer.InternalTag))
            {
                WorldContainer.InternalScene = arg0;
                OnInternalSceneLoaded?.Invoke(arg0, arg1);
            }
            if (IsBothSceneLoaded) OnAllSceneLoaded?.Invoke(arg0, arg1);
        }

        public bool IsBothSceneLoaded
        {
            get
            {
                return SceneManager.GetSceneByName(WorldContainer.ExternalSceneName).isLoaded &&
                    SceneManager.GetSceneByName(WorldContainer.InternalSceneName).isLoaded;
            }
        }

        public delegate void SceneLoad(Scene arg0, LoadSceneMode arg1);
        public event SceneLoad OnExternalSceneLoaded;
        public event SceneLoad OnInternalSceneLoaded;
        public event SceneLoad OnAllSceneLoaded;
    }
}
