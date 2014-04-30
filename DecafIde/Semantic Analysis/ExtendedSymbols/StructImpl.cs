using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecafIde.Semantic_Analysis.ExtendedSymbols
{
    /// <summary>
    /// Represents an struct implementation.
    /// </summary>
    internal class StructImpl : Symbol
    {
        /// <summary>
        /// Represents the parent struct name.
        /// </summary>
        String parentStructName;

        /// <summary>
        /// Gets the parent struct name.
        /// </summary>
        internal String ParentStructName
        {
            get { return parentStructName; }
        }

        /// <summary>
        /// Initializes a new struct implementation with the specified parent struct name.
        /// </summary>
        /// <param name="TheParentStructName">The parent struct name.</param>
        internal StructImpl(string TheParentStructName)
            : base(SymbolType.TstructImpl, symbolCategory.Cvariable)
        {
            parentStructName = TheParentStructName;
        }
    }
}
