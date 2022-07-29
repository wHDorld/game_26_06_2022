using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Logic.MapGenerator.Enums;
using Assets.Logic.MapGenerator.Entities;

namespace Assets.Logic.MapGenerator.Methods
{
    public static class SAreaTypeGenerator
    {
        private static float[,] heights_original;
        private static float[,] heights_override;
        private static int[] usable_zone_anchors;

        public static void GenerateArea(AreaInformation areaInformation)
        {
            if (areaInformation.container.isRoad)
            {
                generateRoadInformation(areaInformation);
            }
            Func<bool> outOfBounds = () =>
            {
                return areaInformation.position.x < 0 ||
                    areaInformation.position.x > areaInformation.terrainData.heightmapResolution ||
                    areaInformation.position.y < 0 ||
                    areaInformation.position.y > areaInformation.terrainData.heightmapResolution;
            };
            Action moveArea = () =>
            {
                Vector2 moving = new Vector2(
                    areaInformation.direction.x * Mathf.PerlinNoise(areaInformation.position.x * areaInformation.container.road_curveness, 0),
                    areaInformation.direction.y * Mathf.PerlinNoise(0, areaInformation.position.y * areaInformation.container.road_curveness)
                    ).normalized * areaInformation.radius / 2f;
                areaInformation.position += new Vector2Int(
                    Mathf.RoundToInt(moving.x),
                    Mathf.RoundToInt(moving.y)
                    );
            };

            heights_original = getAllHeight(areaInformation);
            bool roading = !outOfBounds();
            while (roading)
            {
                roading = false;
                heights_override = getUsableHeight(areaInformation);
                usable_zone_anchors = areaInformation.GetUsableZoneAnchors();
                foreach (var type in areaInformation.container.GenerationTypes)
                    switch (type)
                    { 
                        case EAreaGenerationType.Flattering: Flattering(areaInformation); break;
                        case EAreaGenerationType.Randomizing: Randomizing(areaInformation); break;
                        case EAreaGenerationType.ToMin: ToMin(areaInformation); break;
                        case EAreaGenerationType.ToMax: ToMax(areaInformation); break;
                        case EAreaGenerationType.HalfSphereUp: HalfSphereUp(areaInformation); break;
                        case EAreaGenerationType.HalfSphereDown: HalfSphereDown(areaInformation); break;
                        case EAreaGenerationType.SphereUp: SphereUp(areaInformation); break;
                        case EAreaGenerationType.SphereDown: SphereDown(areaInformation); break;
                    }
                overrideAllHeight(areaInformation);
                if (areaInformation.container.isRoad)
                {
                    roading = !outOfBounds();
                    moveArea();
                }
            }
            areaInformation.terrainData.SetHeights(0, 0, heights_original);
        }

        #region Area types Realization
        public static void Flattering(AreaInformation areaInformation)
        {
            float average_height = 0;
            int counter = 0;
            for (int y = usable_zone_anchors[1]; y < usable_zone_anchors[3]; y++)
                for (int x = usable_zone_anchors[0]; x < usable_zone_anchors[2]; x++)
                {
                    if (heights_override[x, y] == -1) continue;
                    counter++;
                    average_height += heights_override[x, y];
                }
            average_height /= (float)counter;
            overrideAvalibleHeight(areaInformation, average_height);
        }
        public static void Randomizing(AreaInformation areaInformation)
        {
            for (int y = usable_zone_anchors[1]; y < usable_zone_anchors[3]; y++)
                for (int x = usable_zone_anchors[0]; x < usable_zone_anchors[2]; x++)
                {
                    if (heights_override[x, y] == -1) continue;
                    heights_override[x, y] = heights_override[x, y] < 0 ? 0 : (heights_override[x, y] > 1 ? 1 : heights_override[x, y]);
                    heights_override[x, y] +=
                            (Mathf.PerlinNoise(
                                (x / (float)heights_override.GetLength(0)) * 100f,
                                (y / (float)heights_override.GetLength(1)) * 100f
                                ) - 0.5f) / 500;
                }
        }
        public static void ToMin(AreaInformation areaInformation)
        {
            overrideAvalibleHeight(areaInformation, 0);
        }
        public static void ToMax(AreaInformation areaInformation)
        {
            overrideAvalibleHeight(areaInformation, 1);
        }
        public static void HalfSphereUp(AreaInformation areaInformation)
        {
            addAvalibleHeight(areaInformation, (areaInformation.radius / (float)areaInformation.terrainData.heightmapResolution) / 4f);
        }
        public static void HalfSphereDown(AreaInformation areaInformation)
        {
            addAvalibleHeight(areaInformation, -(areaInformation.radius / (float)areaInformation.terrainData.heightmapResolution) / 4f);
        }
        public static void SphereUp(AreaInformation areaInformation)
        {
            addAvalibleHeight(areaInformation, (areaInformation.radius / (float)areaInformation.terrainData.heightmapResolution) / 2f);
        }
        public static void SphereDown(AreaInformation areaInformation)
        {
            addAvalibleHeight(areaInformation, -(areaInformation.radius / (float)areaInformation.terrainData.heightmapResolution) / 2f);
        }
        #endregion

        static void generateRoadInformation(AreaInformation areaInformation)
        {
            float start_x = areaInformation.position.x / (float)areaInformation.terrainData.heightmapResolution;
            float start_y = areaInformation.position.y;
            float dir_x = areaInformation.direction.x;
            float dir_y = areaInformation.direction.y;

            if (start_x < 0.5f)
            {
                start_x = areaInformation.terrainData.heightmapResolution / 50f;
                dir_x = dir_x < 0 ? -dir_x : dir_x;
            }
            else
            {
                start_x = areaInformation.terrainData.heightmapResolution - areaInformation.terrainData.heightmapResolution / 50f;
                dir_x = dir_x > 0 ? -dir_x : dir_x;
            }

            if (start_y < areaInformation.terrainData.heightmapResolution / 2f)
            {
                dir_y = dir_y < 0 ? -dir_y : dir_y;
            }
            else
            {
                dir_y = dir_y > 0 ? -dir_y : dir_y;
            }

            areaInformation.position = new Vector2Int(Mathf.RoundToInt(start_x), Mathf.RoundToInt(start_y));
            areaInformation.direction = new Vector3(dir_x, dir_y, areaInformation.direction.z).normalized;
        }
        static void addAvalibleHeight(AreaInformation areaInformation, float additional)
        {
            for (int y = usable_zone_anchors[1]; y < usable_zone_anchors[3]; y++)
                for (int x = usable_zone_anchors[0]; x < usable_zone_anchors[2]; x++)
                {
                    if (heights_override[x, y] == -1) continue;
                    heights_override[x, y] += additional;
                    heights_override[x, y] = heights_override[x, y] > 1 ? 1 : (heights_override[x, y] < 0 ? 0 : heights_override[x, y]);
                }
        }
        static void overrideAvalibleHeight(AreaInformation areaInformation, float data)
        {
            for (int y = usable_zone_anchors[1]; y < usable_zone_anchors[3]; y++)
                for (int x = usable_zone_anchors[0]; x < usable_zone_anchors[2]; x++)
                {
                    if (heights_override[x, y] == -1) continue;
                    heights_override[x, y] = data;
                    heights_override[x, y] = heights_override[x, y] > 1 ? 1 : (heights_override[x, y] < 0 ? 0 : heights_override[x, y]);
                }
        }

        static bool isInCircle(float x, float y, AreaInformation areaInformation)
        {
            return Mathf.Pow(x - areaInformation.position.x, 2) + Mathf.Pow(y - areaInformation.position.y, 2) <= areaInformation.radius * areaInformation.radius;
        }
        static bool isInLine(float x, float y, AreaInformation areaInformation)
        {
            Vector2 dir = new Vector2(areaInformation.direction.x, areaInformation.direction.y);
            float k = dir.x != 0 ? dir.y / dir.x : 999;
            float b = areaInformation.position.y - k * areaInformation.position.x;
            float b1 = b + areaInformation.radius * Mathf.Sqrt(k * k + 1);
            float b2 = b - areaInformation.radius * Mathf.Sqrt(k * k + 1);
            float c = y - k * x;
            return c > b2 && c < b1;
        }

        static float[,] getUsableHeightInLine(AreaInformation areaInformation)
        {
            float[,] heights_usable = new float[areaInformation.terrainData.heightmapResolution, areaInformation.terrainData.heightmapResolution];
            for (int y = 0; y < heights_usable.GetLength(0); y++)
                for (int x = 0; x < heights_usable.GetLength(1); x++)
                {
                    if (isInLine(x, y, areaInformation)) heights_usable[x, y] = heights_original[x, y];
                    else heights_usable[x, y] = -1;
                }
            return heights_usable;
        }
        static float[,] getUsableHeightInCircle(AreaInformation areaInformation)
        {
            float[,] heights_usable = new float[areaInformation.terrainData.heightmapResolution, areaInformation.terrainData.heightmapResolution];
            for (int y = 0; y < heights_usable.GetLength(0); y++)
                for (int x = 0; x < heights_usable.GetLength(1); x++)
                {
                    if (isInCircle(x, y, areaInformation)) heights_usable[x, y] = heights_original[x, y];
                    else heights_usable[x, y] = -1;
                }
            return heights_usable;
        }
        static float[,] getUsableHeight(AreaInformation areaInformation)
        {
            switch (areaInformation.container.ShapeType)
            {
                case EAreaGenerationShape.Circle: return getUsableHeightInCircle(areaInformation);
                case EAreaGenerationShape.Line: return getUsableHeightInLine(areaInformation);
            }
            return getUsableHeightInCircle(areaInformation);
        }
        static float[,] getAllHeight(AreaInformation areaInformation)
        {
            return areaInformation.terrainData.GetHeights(0, 0, areaInformation.terrainData.heightmapResolution, areaInformation.terrainData.heightmapResolution);
        }

        static void overrideAllHeight(AreaInformation areaInformation)
        {
            switch (areaInformation.container.ShapeType)
            {
                case EAreaGenerationShape.Circle: overrideAllHeightInCircle(areaInformation); break;
                case EAreaGenerationShape.Line: overrideAllHeightInLine(areaInformation); break;
            }
        }
        static void overrideAllHeightInCircle(AreaInformation areaInformation)
        {
            Func<int, int, float> actual_height = (x, y) =>
            {
                float coef = Mathf.Pow(Mathf.Pow(Mathf.Pow(x - areaInformation.position.x, 2) + Mathf.Pow(y - areaInformation.position.y, 2), 0.5f) / areaInformation.radius, areaInformation.container.smooth_pow) * areaInformation.container.smoothness;
                return heights_override[x, y] * (1f - coef) + heights_original[x, y] * coef;
            };
            for (int y = usable_zone_anchors[1]; y < usable_zone_anchors[3]; y++)
                for (int x = usable_zone_anchors[0]; x < usable_zone_anchors[2]; x++)
                {
                    if (heights_override[x, y] != -1) heights_original[x, y] = actual_height(x, y);
                }
        }
        static void overrideAllHeightInLine(AreaInformation areaInformation)
        {
            Vector2 dir = new Vector2(areaInformation.direction.x, areaInformation.direction.y);
            float k = dir.x != 0 ? dir.y / dir.x : 999;
            float b = areaInformation.position.y - k * areaInformation.position.x;
            float b1 = b + areaInformation.radius * Mathf.Sqrt(k * k + 1);

            Func<int, int, float> actual_height = (x, y) =>
            {
                float c = y - k * x;
                float coef = Mathf.Pow((c > b ? (c - b) : (b - c)) / (b1 - b), areaInformation.container.smooth_pow) * areaInformation.container.smoothness;
                return heights_override[x, y] * (1f - coef) + heights_original[x, y] * coef;
            };
            for (int y = usable_zone_anchors[1]; y < usable_zone_anchors[3]; y++)
                for (int x = usable_zone_anchors[0]; x < usable_zone_anchors[2]; x++)
                {
                    if (heights_override[x, y] != -1) heights_original[x, y] = actual_height(x, y);
                }
        }
    }
}
