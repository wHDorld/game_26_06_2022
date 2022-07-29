using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Features.Interact.Entities;
using Features.Interact.Components;
using UnityEngine;

namespace Features.Interact.Interfaces
{
    public interface IInteractable
    {
        public void GetMessage(InteractMessage message);
        public void Reset();
    }
}
