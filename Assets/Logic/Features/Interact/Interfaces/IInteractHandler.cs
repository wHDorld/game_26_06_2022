using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Features.Interact.Entities;

namespace Features.Interact.Interfaces
{
    public interface IInteractHandler
    {
        public void Interact(InteractMessage message);
    }
}
