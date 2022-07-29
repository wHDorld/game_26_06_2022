using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Logic.MapGenerator.Entities;
using Assets.Logic.MapGenerator.Enums;
using Assets.Logic.MapGenerator.Methods;

public class DEMO_MapGeneration : MonoBehaviour
{
    public int Seed;
    public MapSettings settings;

    void Start()
    {
        if (Seed != 0)
            Random.InitState(Seed);
        Generate1();
    }

    void Generate1()
    {
        var heights = settings.terrain.terrainData.GetHeights(0, 0, settings.terrain.terrainData.heightmapResolution, settings.terrain.terrainData.heightmapResolution);
        for (int y = 0; y < heights.GetLength(0); y++)
            for (int x = 0; x < heights.GetLength(1); x++)
                heights[x, y] = Mathf.PerlinNoise((x / (float)heights.GetLength(0)) * settings.Density, (y / (float)heights.GetLength(1)) * settings.Density) * settings.InitiateHeight;
        settings.terrain.terrainData.SetHeights(0, 0, heights);

        float randAngle = Random.Range(0f, Mathf.PI);
        SAreaTypeGenerator.GenerateArea(
            new AreaInformation(
                settings.terrain.terrainData, 
                new Vector2Int(Mathf.RoundToInt(Random.Range(1, heights.GetLength(0) - 1)), Mathf.RoundToInt(Random.Range(1, heights.GetLength(0) - 1))), 
                new Vector3(Mathf.Cos(randAngle), Mathf.Sin(randAngle)),
                Mathf.RoundToInt(Random.Range(settings.AreaContainers[0].radius_randomizing.x, settings.AreaContainers[0].radius_randomizing.y)), 
                settings.AreaContainers[0]
                )
            );
    }
}
