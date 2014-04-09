using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecafIde.Semantic_Analysis.ExtendedSymbols
{
    class StructArraySymbol:ArraySymbol
    {
        String parentStructName;

        internal String ParentStructName
        {
            get { return parentStructName; }
        }

        internal StructArraySymbol(SymbolType theInternalType, int theLength, String theParentStructName)
            : base(theInternalType, theLength)
        {
            parentStructName = theParentStructName;
        }
    }
}
