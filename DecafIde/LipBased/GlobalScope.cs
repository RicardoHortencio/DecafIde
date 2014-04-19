using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecafIde.LipBased
{
    class GlobalScope: BaseScope
    {
        public GlobalScope()
            : base(null)
        { }

        public string getScopeName()
        {
            return "global";
        }
    }
}
