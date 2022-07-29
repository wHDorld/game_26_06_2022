using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Features.Unit.Interfaces
{
    public interface IJumpable
    {
        public void Jump(Vector3 dir, float force);
    }
}
