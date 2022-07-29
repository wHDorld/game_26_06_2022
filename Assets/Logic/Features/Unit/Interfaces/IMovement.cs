using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Features.Unit.Interfaces
{
    public interface IMovement
    {
        public void Move(Vector3 dir);
        public void Stop();
    }
}
