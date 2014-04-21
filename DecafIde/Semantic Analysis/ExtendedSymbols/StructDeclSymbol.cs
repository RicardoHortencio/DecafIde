using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecafIde.Semantic_Analysis.ExtendedSymbols
{
    internal class StructDeclSymbol : Symbol
    {
        SymbolTable members;

        /// <summary>
        /// Gets the current member dictionary.
        /// </summary>
        internal SymbolTable Members
        {
            get { return members; }
        }

        /// <summary>
        /// Initializes a struct declaration by passing a dictionary as the current members.
        /// </summary>
        /// <param name="theMembers">The dictionary with keyvaluepairs you know</param>
        internal StructDeclSymbol(Dictionary<string, Symbol> theMembers)
            : base()
        {
            members = new SymbolTable();
            members.Add(theMembers);
        }

        /// <summary>
        /// Initializes a struct declaration by passing a symbolTable as the current members.
        /// </summary>
        /// <param name="theMembers">the symbol table you want as default</param>
        internal StructDeclSymbol(SymbolTable theMembers)
            : base()
        {
            members = theMembers;
        }
    }
}
