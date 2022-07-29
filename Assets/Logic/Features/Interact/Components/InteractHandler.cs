using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Features.Interact.Interfaces;
using Features.Interact.Entities;

namespace Features.Interact.Components
{
    [AddComponentMenu("Features/Interact/Interact Handler")]
    public class InteractHandler : MonoBehaviour, IInteractHandler
    {
        private void Start()
        {
            foreach (var a in GetComponents<IInteractable>())
                OnInteract += a.GetMessage;
        }

        public void Interact(InteractMessage message)
        {
            OnInteract?.Invoke(message);
        }

        public event interactMessagesMethod OnInteract;
        public delegate void interactMessagesMethod (InteractMessage message);
    }
}
