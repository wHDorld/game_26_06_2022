using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Features.Unit.Entities;

namespace Features.Unit.Interfaces
{
    public interface IValueClampedContainer
    {
        public List<ClampedValueE> GetValues();
        public void UpdateValues();
        public void InitiateValues();
    }
}
