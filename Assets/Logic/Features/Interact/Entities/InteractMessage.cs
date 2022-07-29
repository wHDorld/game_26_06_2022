using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Features.Interact.Interfaces;
using UnityEngine;

namespace Features.Interact.Entities
{
    public struct InteractMessage
    {
        public GameObject sender;
        public string interactField;
        public float interactValue;
        public List<float> anotherValues;

        public InteractMessage(GameObject sender, string interactField, float interactValue, List<float> anotherValues)
        {
            this.sender = sender;
            this.interactField = interactField;
            this.interactValue = interactValue;
            this.anotherValues = anotherValues;
        }
        public InteractMessage(GameObject sender, string interactField, float interactValue)
        {
            this.sender = sender;
            this.interactField = interactField;
            this.interactValue = interactValue;
            this.anotherValues = new List<float>();
        }
    }
}
