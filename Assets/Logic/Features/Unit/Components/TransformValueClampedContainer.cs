using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Features.Unit.Interfaces;
using Features.Unit.Enums;
using Features.Unit.Entities;

namespace Features.Unit.Components
{
    public class TransformValueClampedContainer : MonoBehaviour, IValueClampedContainer
    {
        new Transform transform;
        public List<ClampedValueE> AllValues = new List<ClampedValueE>()
        {
            new ClampedValueE(new Vector2(0, 0), "pos X"),
            new ClampedValueE(new Vector2(0, 0), "pos Y"),
            new ClampedValueE(new Vector2(0, 0), "pos Z"),
            new ClampedValueE(new Vector2(0, 360), "rot X"),
            new ClampedValueE(new Vector2(0, 360), "rot Y"),
            new ClampedValueE(new Vector2(0, 360), "rot Z"),
            new ClampedValueE(new Vector2(0, 1), "siz X"),
            new ClampedValueE(new Vector2(0, 1), "siz Y"),
            new ClampedValueE(new Vector2(0, 1), "siz Z")
        };

        private void Start()
        {
            transform = GetComponent<Transform>();
            InitiateValues();
        }
        private void Update()
        {
            UpdateValues();
        }

        public void InitiateValues()
        {

        }
        public void UpdateValues()
        {
            AllValues[0]?.ConvertRawToValue(transform.localPosition.x);
            AllValues[1]?.ConvertRawToValue(transform.localPosition.y);
            AllValues[2]?.ConvertRawToValue(transform.localPosition.z);
            AllValues[3]?.ConvertRawToValue(transform.localEulerAngles.x < 180 ? transform.localEulerAngles.x : (transform.localEulerAngles.x - 360));
            AllValues[4]?.ConvertRawToValue(transform.localEulerAngles.y < 180 ? transform.localEulerAngles.y : (transform.localEulerAngles.y - 360));
            AllValues[5]?.ConvertRawToValue(transform.localEulerAngles.z < 180 ? transform.localEulerAngles.z : (transform.localEulerAngles.z - 360));
            AllValues[6]?.ConvertRawToValue(transform.localScale.x);
            AllValues[7]?.ConvertRawToValue(transform.localScale.y);
            AllValues[8]?.ConvertRawToValue(transform.localScale.z);
        }
        public List<ClampedValueE> GetValues()
        {
            return AllValues;
        }
    }
}
