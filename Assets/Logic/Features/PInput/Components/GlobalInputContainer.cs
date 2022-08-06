using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Features.PInput.Entities;

namespace Features.PInput.Components
{
    [AddComponentMenu("Features/Player Input/Global Input Container")]
    public class GlobalInputContainer : MonoBehaviour
    {
        private static List<InputCacheE> InputCache = new List<InputCacheE>();
        public bool HideMouse;

        private void Start()
        {
            if (HideMouse)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        private void Update()
        {
            foreach (var a in InputCache)
                    a.SetValue(Input.GetAxisRaw(a.field));
        }

        public static void CacheAxis(string field, bool oncePressable = false)
        {
            if (InputCache.Any(x => x.field == field))
                return;
            InputCache.Add(new InputCacheE(field, 0, oncePressable));
        }

        public static float GetAxis(string field)
        {
            return InputCache.FirstOrDefault(x => x.field == field).GetValue();
        }
    }
}
