using DecafIde.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecafIde.Semantic_Analysis.ExtendedSymbols
{
    /// <summary>
    /// Represents a method symbol.
    /// </summary>
    internal class MethodSymbol : Symbol
    {
        /// <summary>
        /// Stores all the possible parameter lists. For method overloading.
        /// </summary>
        List<List<SymbolType>> parameterListAlternatives;

        /// <summary>
        /// Gets the method parameters.
        /// </summary>
        public List<List<SymbolType>> Parameters
        {
            get { return parameterListAlternatives; }
        }
        
        /// <summary>
        /// Initializes a new method with the specified return type and parameters.
        /// </summary>
        /// <param name="theReturnType">The method return type.</param>
        /// <param name="theParameters">The method parameters list.</param>
        internal MethodSymbol(SymbolType theReturnType, List<SymbolType> theParameters)
            : base(theReturnType, symbolCategory.Cmethod)
        {
            parameterListAlternatives = new List<List<SymbolType>>();
            parameterListAlternatives.Add(theParameters);
        }

        /// <summary>
        /// Initializes an empty method with the specified return type.
        /// </summary>
        /// <param name="theReturnType">The return type you want.</param>
        internal MethodSymbol(SymbolType theReturnType)
            : base(theReturnType, symbolCategory.Cmethod)
        {
            parameterListAlternatives = new List<List<SymbolType>>();
        }

        /// <summary>
        /// Adds the method parameter list to method overloading.
        /// </summary>
        /// <param name="theParameterList">The new parameters list.</param>
        internal void addParameterList(List<SymbolType> theParameterList){
            parameterListAlternatives.Add(theParameterList);
        }

        /// <summary>
        /// Checks if the parameter list is already on the overloading options.
        /// </summary>
        /// <param name="theParameterList">The method parameters list.</param>
        /// <returns>Returns true if the parameters list is compatible with a certain method declaration.</returns>
        internal bool checkParameterList(List<SymbolType> theParameterList)
        {
            bool theResult=false;
            foreach (List<SymbolType> singleList in parameterListAlternatives)
            {
                theResult = (theResult || TypeHelper.checkLists(theParameterList, singleList));
            }

            return theResult;
        }

    }
}
