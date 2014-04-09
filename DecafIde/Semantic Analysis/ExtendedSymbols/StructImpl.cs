using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecafIde.Semantic_Analysis.ExtendedSymbols
{
    internal class StructImpl : Symbol
    {
        String parentStructName;

        internal String ParentStructName
        {
            get { return parentStructName; }
        }

        internal StructImpl(string name)
            : base(SymbolType.TstructImpl, symbolCategory.Cvariable)
        {
            parentStructName = name;
        }
    }
}
