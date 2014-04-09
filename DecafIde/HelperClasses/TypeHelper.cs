using DecafIde.Semantic_Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecafIde.HelperClasses
{
    static class TypeHelper
    {
        public static bool isNumeric(SymbolType theSymbol)
        {
            return (theSymbol == SymbolType.Tchar || theSymbol == SymbolType.Tint);
        }

        public static bool isNumeric(SymbolType leftType, SymbolType rightType)
        {
            return (isNumeric(leftType) && isNumeric(rightType));
        }

        public static bool isBoolean(SymbolType theSymbol)
        {
            return (theSymbol == SymbolType.Tboolean);
        }

        public static bool isBoolean(SymbolType leftType, SymbolType rightType)
        {
            return (isBoolean(leftType) && isBoolean(rightType));
        }

        public static bool isEquivalentType(SymbolType leftType, SymbolType rightType)
        {
            return isBoolean(leftType, rightType) || isNumeric(leftType, rightType) || (leftType==rightType);
        }

        public static SymbolType getGreatestType(SymbolType leftType, SymbolType rightType)
        {
            if (!isEquivalentType(leftType, rightType))
                throw new Exception("Incomparable types");
            else
            {
                if (leftType == rightType)
                    return leftType;
                else
                    return SymbolType.Tint;
            }

        }

        public static bool checkLists(List<SymbolType> theParameterList, List<SymbolType> theProbableList)
        {
            bool theResult = false;
            
            
            for (int i = 0; i < theParameterList.Count; i++)
            {
                if (theProbableList != null && theParameterList != null) {
                    
                    if (theProbableList.Count == theParameterList.Count)
                        theResult = (theResult || isEquivalentType(theParameterList.ElementAt(i), theProbableList.ElementAt(i)));
                }
            }

            if (theParameterList.Count == 0 && theProbableList.Count == 0)
                        theResult = true;

            return theResult;
        }
    }
}
