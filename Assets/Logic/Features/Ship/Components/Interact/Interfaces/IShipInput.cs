using Features.Unit.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Features.Ship.Components.Interact.Interfaces
{
    public interface IShipInput
    {
        public List<ClampedValueE> GetClampedValue();
        public Transform GetInputObject();
    }
}
