using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecafIde.LipBased
{
    class BuiltInTypeSymbol : Symbol, IType
    {
        SymbolType typeIndex;
        public BuiltInTypeSymbol(String name, SymbolType theTypeIndex)
            : base(name) 
        {
            this.typeIndex = theTypeIndex;
        }

        public SymbolType getTypeIndex()
        {
            return typeIndex;
        }

        public override string ToString()
        {
            return getName();
        }
    }
}
