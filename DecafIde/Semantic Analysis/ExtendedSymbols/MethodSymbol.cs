using DecafIde.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecafIde.Semantic_Analysis.ExtendedSymbols
{
    internal class MethodSymbol : Symbol
    {
        List<List<SymbolType>> parameterListAlternatives;

        public List<List<SymbolType>> Parameters
        {
            get { return parameterListAlternatives; }
        }
        
        internal MethodSymbol(SymbolType theReturnType, List<SymbolType> theParameters)
            : base(theReturnType, symbolCategory.Cmethod)
        {
            parameterListAlternatives = new List<List<SymbolType>>();
            parameterListAlternatives.Add(theParameters);
        }

        internal MethodSymbol(SymbolType theReturnType)
            : base(theReturnType, symbolCategory.Cmethod)
        {
            parameterListAlternatives = new List<List<SymbolType>>();
        }

        internal void addParameterList(List<SymbolType> theParameterList){
            parameterListAlternatives.Add(theParameterList);
        }

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
