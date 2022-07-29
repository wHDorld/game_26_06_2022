using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Logic.MapGenerator.Enums;

namespace Assets.Logic.MapGenerator.Entities
{
    [System.Serializable]
    public class AreaContainer : IComparable
    {
        [Header("Area Random-Generated Connection Prorepties")]
        public float smoothness;
        public float smooth_pow;
        [Header("Area Random-Generated Generation Prorepties")]
        public float chance;
        public Vector2 radius_randomizing;
        public Vector2 direction_x_randomizing;
        public Vector2 direction_y_randomizing;
        public Vector2 direction_z_randomizing;

        [Header("---")]

        [Header("Area Random-Generated Type")]
        public EAreaGenerationShape ShapeType;
        public List<EAreaGenerationType> GenerationTypes = new List<EAreaGenerationType>();
        public bool isRoad;
        public float road_curveness;

        public int CompareTo(object obj)
        {
            if (obj is AreaContainer) return chance.CompareTo(((AreaContainer)obj).chance);
            else throw new NotImplementedException();
        }
    }
}
