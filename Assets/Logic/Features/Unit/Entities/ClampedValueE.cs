using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Features.Unit.Entities
{
    [Serializable]
    public class ClampedValueE
    {
        public string Name;
        public float value;
        public float minValue;
        public float maxValue;

        public ClampedValueE(float minValue, float maxValue, string name)
        {
            value = 0;
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.Name = name;
        }
        public ClampedValueE(Vector2 values, string name)
        {
            value = 0;
            this.minValue = values.x;
            this.maxValue = values.y;
            this.Name = name;
        }

        public float Range
        {
            get
            {
                return maxValue - minValue;
            }
        }
        public float Middle
        {
            get
            {
                return (maxValue + minValue) / 2f;
            }
        }

        public void ConvertRawToValue(float raw)
        {
            value = (raw - minValue) / Range;
        }
    }
}
