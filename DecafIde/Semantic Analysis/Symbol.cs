using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecafIde.Semantic_Analysis
{
    public enum SymbolType
    {
        Tint,
        Tchar,
        Tboolean,
        Tvoid,
        Tarray,
        TstructImpl //declaración de estructuras
    }

    public enum symbolCategory
    {
        Cvariable,
        CstructDecl,
        Cmethod
    }

    public class Symbol
    {
        SymbolType type;
        symbolCategory category;

        public symbolCategory Category
        {
            get { return category; }
        }
        public SymbolType Type
        {
            get { return type; }
        }

        public Symbol()
        {
            type = SymbolType.Tvoid;
            category = symbolCategory.CstructDecl;
        }

        public Symbol(SymbolType theSymbolType)
        {
            type = theSymbolType;
            category = symbolCategory.Cvariable;
        }
        public Symbol(SymbolType theSymbolType, symbolCategory theCategory)
        {
            type = theSymbolType;
            category = theCategory;
        }
    }
}
