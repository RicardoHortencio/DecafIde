using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecafIde.Semantic_Analysis
{
    /// <summary>
    /// Enum which contains most of the "primitive" types.
    /// </summary>
    public enum SymbolType
    {
        /// <summary>
        /// Int type
        /// </summary>
        Tint,
        /// <summary>
        /// Char type
        /// </summary>
        Tchar,
        /// <summary>
        /// Boolean type (True|False)
        /// </summary>
        Tboolean,
        /// <summary>
        /// Void type (can't be initialized)
        /// </summary>
        Tvoid,
        /// <summary>
        /// Simple Array Type
        /// </summary>
        Tarray,
        /// <summary>
        /// Implicit structure implementation type
        /// </summary>
        TstructImpl 
    }

    /// <summary>
    /// Symbol categories you can find as a symbol
    /// </summary>
    public enum symbolCategory
    {
        /// <summary>
        /// A variable is basically just a value container.
        /// </summary>
        Cvariable,
        /// <summary>
        /// A struct declaration is both a value container and a logical scope
        /// </summary>
        CstructDecl,
        /// <summary>
        /// A method is also a value container which returns a primitive type
        /// </summary>
        Cmethod
    }

    /// <summary>
    /// The Symbol types represents every symbol in our table.
    /// </summary>
    public class Symbol
    {
        SymbolType type;
        symbolCategory category;
        
        /// <summary>
        /// The category at which the symbol belongs. (i.e. variable, struct, method)
        /// </summary>
        public symbolCategory Category
        {
            get { return category; }
        }

        /// <summary>
        /// The type is the primitive type that the symbol has. (i.e. int, char, bool,...)
        /// </summary>
        public SymbolType Type
        {
            get { return type; }
        }

        /// <summary>
        /// Intializes a struct declaration, which has a void type.
        /// </summary>
        public Symbol()
        {
            type = SymbolType.Tvoid;
            category = symbolCategory.CstructDecl;
        }

        /// <summary>
        /// Initializes a variable declaration with the specified type.
        /// </summary>
        /// <param name="theSymbolType">The variable type</param>
        public Symbol(SymbolType theSymbolType)
        {
            type = theSymbolType;
            category = symbolCategory.Cvariable;
        }
        /// <summary>
        /// Initializes a symbol declaration with the specified type and category.
        /// </summary>
        /// <param name="theSymbolType">The current symbol type</param>
        /// <param name="theCategory">The current symbol category</param>
        public Symbol(SymbolType theSymbolType, symbolCategory theCategory)
        {
            type = theSymbolType;
            category = theCategory;
        }
    }
}
