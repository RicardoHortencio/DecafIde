using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecafIde.LipBased
{
    class BuiltInTypeSymbol : Symbol, Type
    {
        public BuiltInTypeSymbol(String name) : base(name) { }
    }
}
