using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecafIde.Semantic_Analysis.ExtendedSymbols
{
    internal class StructDeclSymbol : Symbol
    {
        SymbolTable members;

        internal SymbolTable Members
        {
            get { return members; }
        }

        internal StructDeclSymbol(Dictionary<string, Symbol> theMembers)
            : base()
        {
            members = new SymbolTable();
            members.Add(theMembers);
        }

        internal StructDeclSymbol(SymbolTable theMembers)
            : base()
        {
            members = theMembers;
        }
    }
}
