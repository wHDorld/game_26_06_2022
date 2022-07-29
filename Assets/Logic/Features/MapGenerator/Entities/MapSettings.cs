using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Logic.MapGenerator.Methods;

namespace Assets.Logic.MapGenerator.Entities
{
    [System.Serializable]
    public class MapSettings
    {
        public float Density;
        public float InitiateHeight = 1;
        public Terrain terrain;

        public List<AreaContainer> AreaContainers = new List<AreaContainer>();

    }
}
