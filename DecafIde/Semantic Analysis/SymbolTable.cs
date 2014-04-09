using DecafIde.Semantic_Analysis.ExtendedSymbols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecafIde.Semantic_Analysis
{
    class SymbolTable
    {
        private Dictionary<string, Symbol> theVariableTable;
        private Dictionary<string, Symbol> theMethodTable;
        private Dictionary<string, Symbol> theStructDeclTable;

        public SymbolTable()
        {
            theVariableTable = new Dictionary<string, Symbol>();
            theMethodTable = new Dictionary<string, Symbol>();
            theStructDeclTable = new Dictionary<string, Symbol>();
        }

        public void Add(Dictionary<string, Symbol> theTable)
        {
            foreach (KeyValuePair<string, Symbol> singleSymbol in theTable)
            {
                this.Add(singleSymbol.Key, singleSymbol.Value);
            }
        }

        public void Add(KeyValuePair<string, Symbol> thePair)
        {
            this.Add(thePair.Key, thePair.Value);
        }

        public void Add(string name, Symbol theSymbol)
        {

            switch (theSymbol.Category)
            {
                case symbolCategory.Cmethod:
                    Symbol tempSymbol;
                    if (!theMethodTable.TryGetValue(name, out tempSymbol))
                        theMethodTable.Add(name, theSymbol);
                    else
                    {
                        MethodSymbol typedSymbol = tempSymbol as MethodSymbol;
                        MethodSymbol parameteredSymbol = theSymbol as MethodSymbol;
                        List<SymbolType> theParameteredList = parameteredSymbol.Parameters.ElementAt(0);
                        if (!typedSymbol.checkParameterList(theParameteredList))
                            typedSymbol.addParameterList(theParameteredList);
                        else
                            throw new Exception("Redeclared identifier " + name + " on symbol declaration"+Environment.NewLine);                    }
                    break;
                case symbolCategory.CstructDecl:
                    theStructDeclTable.Add(name, theSymbol);
                    break;
                default:
                    theVariableTable.Add(name, theSymbol);
                    break;
            }

        }

        public bool CheckSymbol(string id, symbolCategory theCategory)
        {
            switch (theCategory)
            {
                case symbolCategory.Cvariable:
                    return theVariableTable.ContainsKey(id);
                case symbolCategory.CstructDecl:
                    return theStructDeclTable.ContainsKey(id);
                case symbolCategory.Cmethod:
                    return theMethodTable.ContainsKey(id);
                default:
                    throw new Exception("Internal symbolCategory error");
            }
        }

        public Symbol FindSymbol(string id, symbolCategory theCategory)
        {
            Symbol theResult;
            switch (theCategory)
            {
                case symbolCategory.Cvariable:
                    if (theVariableTable.TryGetValue(id, out theResult))
                        return theResult;
                    else
                        return null;
                case symbolCategory.CstructDecl:
                    if (theStructDeclTable.TryGetValue(id, out theResult))
                        return theResult;
                    else
                        return null;
                case symbolCategory.Cmethod:
                    if (theMethodTable.TryGetValue(id, out theResult))
                        return theResult;
                    else
                        return null;
                default:
                    throw new Exception("Internal symbolCategory error");
            }
        }

        public SymbolType? GetSymbolType(string id, symbolCategory theCategory)
        {
            Symbol tempSymbol = FindSymbol(id, theCategory);
            if (tempSymbol != null) return tempSymbol.Type;
            else return null;
        }
    }
}
