using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecafIde.LipBased
{
    class LocalScope : BaseScope
    {
        public LocalScope(Scope parent) : base(parent) { }

        public string getScopeName() { return "local"; }
    }
}
