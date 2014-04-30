using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecafIde.Semantic_Analysis.ExtendedSymbols
{
    /// <summary>
    /// Represents an struct array symbol.
    /// Has an extra parentStructName to match with parent struct.
    /// </summary>
    class StructArraySymbol:ArraySymbol
    {
        /// <summary>
        /// The parent struct name.
        /// </summary>
        String parentStructName;

        /// <summary>
        /// The parent struct name property.
        /// </summary>
        internal String ParentStructName
        {
            get { return parentStructName; }
        }

        /// <summary>
        /// Initializes a new struct array with the specified type, length and structname.
        /// </summary>
        /// <param name="theInternalType">The internal symbol type.</param>
        /// <param name="theLength">The array's length.</param>
        /// <param name="theParentStructName">The parent struct name.</param>
        internal StructArraySymbol(SymbolType theInternalType, int theLength, String theParentStructName)
            : base(theInternalType, theLength)
        {
            parentStructName = theParentStructName;
        }
    }
}
