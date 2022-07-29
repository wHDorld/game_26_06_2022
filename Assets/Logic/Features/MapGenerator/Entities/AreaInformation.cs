using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Logic.MapGenerator.Entities
{
    public class AreaInformation : ICloneable
    {
        public TerrainData terrainData;
        public Vector2Int position;
        public Vector3 direction;
        public int radius;
        public AreaContainer container;

        public AreaInformation(TerrainData terrainData, Vector2Int position, Vector3 direction, int radius, AreaContainer container)
        {
            this.terrainData = terrainData;
            this.position = position;
            this.direction = direction;
            this.radius = radius;
            this.container = container;
        }

        public object Clone()
        {
            return new AreaInformation(
                this.terrainData,
                this.position,
                this.direction,
                this.radius,
                this.container
                );
        }

        public int[] GetUsableZoneAnchors()
        {
            switch (container.ShapeType)
            {
                case Enums.EAreaGenerationShape.Circle: return GetUsableZoneAnchorsInCircle();
                case Enums.EAreaGenerationShape.Line: return GetUsableZoneAnchorsInLine();

                default: return GetUsableZoneAnchorsInCircle();
            }
        }
        public int[] GetUsableZoneAnchorsInCircle()
        {
            var ret = new int[4]
            {
                position.x - radius,
                position.y - radius,
                position.x + radius,
                position.y + radius
            };
            ret[0] = ret[0] < 0 ? 0 : ret[0];
            ret[2] = ret[2] > terrainData.heightmapResolution ? terrainData.heightmapResolution : ret[2];
            ret[1] = ret[1] < 0 ? 0 : ret[1];
            ret[3] = ret[3] > terrainData.heightmapResolution ? terrainData.heightmapResolution : ret[3];
            return ret;
        }
        public int[] GetUsableZoneAnchorsInLine()
        {
            return new int[4]
            {
                0,
                0,
                terrainData.heightmapResolution,
                terrainData.heightmapResolution
            };
        }
    }
}
