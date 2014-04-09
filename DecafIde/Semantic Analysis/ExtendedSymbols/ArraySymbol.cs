using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecafIde.Semantic_Analysis.ExtendedSymbols
{
    class ArraySymbol:Symbol
    {
        SymbolType internalType;
        public int length;

        internal SymbolType InternalType
        {
            get { return internalType; }
        }

        internal ArraySymbol(SymbolType theInternalType, int theLength)
            : base(SymbolType.Tarray)
        {
            internalType = theInternalType;
            length = theLength;
        }
    }
}
