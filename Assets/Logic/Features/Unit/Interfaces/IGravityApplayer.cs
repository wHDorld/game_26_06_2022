using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Features.Unit.Interfaces
{
    public interface IGravityApplayer
    {
        public void ApplyGravity();
        public void ChangeGravity(Vector3 dir, float power);
    }
}
