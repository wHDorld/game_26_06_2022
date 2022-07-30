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
    [AddComponentMenu("Features/World/World Scene Handler")]
    [RequireComponent(typeof(WorldShipHandler))]
    public class WorldSceneHandler : MonoBehaviour
    {
        public void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;

            LoadMainScenes();
        }

        bool isReloading;
        public void ReloadScenes()
        {
            isReloading = true;
            SceneManager.UnloadSceneAsync(WorldContainer.ExternalScene);
            SceneManager.UnloadSceneAsync(WorldContainer.InternalScene);
        }

        private void LoadMainScenes()
        {
            isReloading = false;
            SceneManager.LoadSceneAsync(WorldContainer.ExternalSceneName, LoadSceneMode.Additive);
            SceneManager.LoadSceneAsync(WorldContainer.InternalSceneName, LoadSceneMode.Additive);
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
        private void OnSceneUnloaded(Scene arg0)
        {
            if (arg0.name.Contains(WorldContainer.ExternalTag))
            {
                WorldContainer.ExternalScene = arg0;
                OnExternalSceneUnloaded?.Invoke(arg0);
            }
            if (arg0.name.Contains(WorldContainer.InternalTag))
            {
                WorldContainer.InternalScene = arg0;
                OnInternalSceneUnloaded?.Invoke(arg0);
            }
            if (IsBothSceneUnloaded)
            {
                OnAllSceneUnloaded?.Invoke(arg0);
                if (isReloading)
                    LoadMainScenes();
            }
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

        public bool IsBothSceneUnloaded
        {
            get
            {
                return !SceneManager.GetSceneByName(WorldContainer.ExternalSceneName).isLoaded &&
                    !SceneManager.GetSceneByName(WorldContainer.InternalSceneName).isLoaded;
            }
        }
        public delegate void SceneUnoad(Scene arg0);
        public event SceneUnoad OnExternalSceneUnloaded;
        public event SceneUnoad OnInternalSceneUnloaded;
        public event SceneUnoad OnAllSceneUnloaded;
    }
}
