using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Features.Unit.Interfaces
{
    public interface IRotatement : ISwitchable
    {
        public void AddRotation(Vector3 euler);
        public void RotateTowards(Vector3 dir);
        public void Stop();
    }
}
