using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecafIde.Semantic_Analysis.ExtendedSymbols
{
    /// <summary>
    /// Represents an array symbol
    /// </summary>
    class ArraySymbol:Symbol
    {
        /// <summary>
        /// The internal symbol type
        /// </summary>
        SymbolType internalType;
        /// <summary>
        /// The array length
        /// </summary>
        public int length;

        /// <summary>
        /// Gets the internal symbol type.
        /// </summary>
        internal SymbolType InternalType
        {
            get { return internalType; }
        }

        /// <summary>
        /// Initializes a new array with the specified type and length.
        /// </summary>
        /// <param name="theInternalType">The internal symbol type.</param>
        /// <param name="theLength">The array length.</param>
        internal ArraySymbol(SymbolType theInternalType, int theLength)
            : base(SymbolType.Tarray)
        {
            internalType = theInternalType;
            length = theLength;
        }
    }
}
