using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecafIde.LipBased
{
    class VariableSymbol: Symbol
    {
        public VariableSymbol(String name, IType type) : base(name, type) {}
    }
}
