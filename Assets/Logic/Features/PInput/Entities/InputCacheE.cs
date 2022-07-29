using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features.PInput.Entities
{
    public class InputCacheE
    {
        public string field;
        public float value;

        public InputCacheE(string field, float value)
        {
            this.field = field;
            this.value = value;
        }
    }
}
