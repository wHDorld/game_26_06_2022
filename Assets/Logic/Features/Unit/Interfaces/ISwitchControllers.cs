using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features.Unit.Interfaces
{
    public interface ISwitchControllers<T> where T: ISwitchable
    {
        public T Switch(T controller, string key);
        public T Release();
    }
}
