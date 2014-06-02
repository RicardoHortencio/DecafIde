using DecafIde.Semantic_Analysis.ExtendedSymbols;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DecafIde.Semantic_Analysis
{
    /// <summary>
    /// A symbol table is essentially a variable, methods and structs dictionary.
    /// </summary>
    public class SymbolTable
    {
        public SymbolTable parentScope;
        private Dictionary<string, Symbol> theVariableTable;
        private Dictionary<string, Symbol> theMethodTable;
        private Dictionary<string, Symbol> theStructDeclTable;

        public SymbolTable()
        {
            parentScope = null;
            theVariableTable = new Dictionary<string, Symbol>();
            theMethodTable = new Dictionary<string, Symbol>();
            theStructDeclTable = new Dictionary<string, Symbol>();
        }

        public SymbolTable(SymbolTable theParentScope)
        {
            parentScope = theParentScope;
            theVariableTable = new Dictionary<string, Symbol>();
            theMethodTable = new Dictionary<string, Symbol>();
            theStructDeclTable = new Dictionary<string, Symbol>();
        }

        /// <summary>
        /// Joins the current symbol table with the passed dictionary.
        /// </summary>
        /// <param name="theTable">The dictionary you're joining</param>
        public void Add(Dictionary<string, Symbol> theTable)
        {
            foreach (KeyValuePair<string, Symbol> singleSymbol in theTable)
            {
                this.Add(singleSymbol);
            }
        }

        /// <summary>
        /// Adds the current keyValuePair to the corresponding dictionary.
        /// </summary>
        /// <param name="thePair">the current keyValuePair</param>
        public void Add(KeyValuePair<string, Symbol> thePair)
        {
            this.Add(thePair.Key, thePair.Value);
        }

        /// <summary>
        /// Adds the current name-symbol pair to the corresponding dictionary.
        /// </summary>
        /// <param name="name">the symbol name</param>
        /// <param name="theSymbol">the symbol value</param>
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
                            throw new Exception("Redeclared identifier " + name + " on symbol declaration" + Environment.NewLine);
                    }
                    break;
                case symbolCategory.CstructDecl:
                    theStructDeclTable.Add(name, theSymbol);
                    break;
                default:
                    theVariableTable.Add(name, theSymbol);
                    break;
            }

        }

        /// <summary>
        /// Checks whether the symbol id is contained within the specified category
        /// </summary>
        /// <param name="id">The id to look for</param>
        /// <param name="theCategory">The category that you should try to find it into</param>
        /// <returns>True if the symbol is found, false otherwise.</returns>
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

        /// <summary>
        /// Finds and retrieves the symbol by Id and category.
        /// </summary>
        /// <param name="id">The id you're looking for</param>
        /// <param name="theCategory">The category you're looking into</param>
        /// <returns>The symbol or null if not found.</returns>
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

        /// <summary>
        /// Finds and retrieves the specified symbol type by ID and Category
        /// </summary>
        /// <param name="id">The id you're looking for.</param>
        /// <param name="theCategory">The category you're looking into.</param>
        /// <returns></returns>
        public SymbolType? GetSymbolType(string id, symbolCategory theCategory)
        {
            Symbol tempSymbol = FindSymbol(id, theCategory);
            if (tempSymbol != null) return tempSymbol.Type;
            else return null;
        }

        public SymbolTable getEnclosingScope()
        {
            return parentScope;
        }
    }
}
