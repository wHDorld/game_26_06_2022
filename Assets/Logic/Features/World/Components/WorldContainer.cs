using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Features.World.Components
{
    public class WorldContainer
    {
        public static string ExternalSceneName = "ExternalScene";
        public static string InternalSceneName = "InternalScene";

        public static string ExternalTag = "External";
        public static string InternalTag = "Internal";

        public static string ExternalPath = "Prefabs/Ships/External/";
        public static string InternalPath = "Prefabs/Ships/Internal/";

        public static Scene ExternalScene;
        public static Scene InternalScene;
    }
}
