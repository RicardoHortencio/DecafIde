using System;
using System.Collections.Generic;
namespace DecafIde.Semantic_Analysis
{
    /// <summary>
    /// Manages the scopes creation and destruction
    /// </summary>
    class ScopeManager
    {
        /// <summary>
        /// Store for the symbol tables
        /// </summary>
        SymbolTable currentScope;
        /// <summary>
        /// Stores the current return value Type
        /// </summary>
        SymbolType returnType = SymbolType.Tvoid;
        Boolean hasReturn = false;

        /// <summary>
        /// Resets the return to false;
        /// </summary>
        public void resetReturn()
        {
            hasReturn = false;
        }


        /// <summary>
        /// Whether or not the current scope has a return value
        /// </summary>
        public Boolean HasReturn
        {
            get { return hasReturn; }
        }

        /// <summary>
        /// Sets the return value type to something meaningful
        /// </summary>
        public SymbolType ReturnType
        {
            get { return returnType; }
            set
            {
                returnType = value;
                hasReturn = true;
            }
        }

        internal SymbolTable GetEnclosingScope()
        {
            return currentScope.getEnclosingScope();
        }

        /// <summary>
        /// Gets and pops the current scope
        /// </summary>
        /// <returns>The current scope</returns>
        internal SymbolTable GetAndExitScope()
        {
            currentScope = currentScope.getEnclosingScope();
            return currentScope;
        }

        /// <summary>
        /// Initializes a new scope manager instance
        /// </summary>
        public ScopeManager()
        {
            currentScope = new SymbolTable();
        }

        /// <summary>
        /// Initializes a new scopemanager instance
        /// </summary>
        public ScopeManager(SymbolTable theCurrentScope)
        {
            currentScope = theCurrentScope;
        }

        /// <summary>
        /// Creates a new scope and sets it as the current scope
        /// </summary>
        internal void EnterScope()
        {
            currentScope = new SymbolTable(currentScope);
        }

        /// <summary>
        /// Performs and superficial search of a certain symbol
        /// </summary>
        /// <param name="id">The symbol ID</param>
        /// <param name="theCategory">The symbol category</param>
        /// <returns></returns>
        public Symbol currentScopeFindSymbol(string id, symbolCategory theCategory)
        {
            Symbol result = null;

            result = currentScope.FindSymbol(id, theCategory);
            return result;
        }

        /// <summary>
        /// Finds the symbol based on its ID and category (Variable, method or structDeclaration)
        /// </summary>
        /// <param name="id">The symbol ID</param>
        /// <param name="theCategory">The symbol category</param>
        /// <returns></returns>
        public Symbol FindSymbol(string id, symbolCategory theCategory)
        {
            Symbol s = currentScope.FindSymbol(id, theCategory);
            if (s != null) return s;
            if (currentScope.getEnclosingScope() != null)
                return currentScope.getEnclosingScope().FindSymbol(id, theCategory);

            return null;

            //Symbol result = null;
            //Boolean symbolFound = false;
            //Stack<SymbolTable> tempStack = new Stack<SymbolTable>();

            //while ((!symbolFound) && (currentScope.Count > 0))
            //{
            //    result = currentScope.Peek().FindSymbol(id, theCategory);
            //    symbolFound = result != null;
            //    if (!symbolFound) tempStack.Push(currentScope.Pop());
            //}


            //if (currentScope.Count <= 0) throw new
            //Exception("Symbol " + id + " not found in symbolTable.");
            //while (tempStack.Count > 0)
            //    currentScope.Push(tempStack.Pop());



            //return result;
        }

        /// <summary>
        /// Adds a symbol to the current scope symbol table
        /// </summary>
        /// <param name="id">The new symbol id</param>
        /// <param name="x">The new symbol instance</param>
        public void AddSymbol(string id, Symbol x)
        {
            currentScope.Add(id, x);
        }

        /// <summary>
        /// Adds a symbol entrie to the current scope symbol table
        /// </summary>
        /// <param name="thePair">The keyValuePair valu</param>
        public void AddSymbol(KeyValuePair<string, Symbol> thePair)
        {
            currentScope.Add(thePair);
        }

        /// <summary>
        /// Checks if the symbol with certain id and category is present in the current scope
        /// </summary>
        /// <param name="id">The symbol supposed id</param>
        /// <param name="theCategory">The symbol supposed category</param>
        /// <returns></returns>
        internal bool CheckScope(string id, symbolCategory theCategory)
        {
            bool symbolInScope = currentScope.CheckSymbol(id, theCategory);
            if (symbolInScope) return symbolInScope;
            if (currentScope.getEnclosingScope() != null)
                return currentScope.getEnclosingScope().CheckSymbol(id, theCategory);

            return false;

            //Boolean symbolFound = false;
            //Stack<SymbolTable> tempStack = new Stack<SymbolTable>();

            //while ((!symbolFound) && (currentScope.Count > 0))
            //{
            //    symbolFound = currentScope.Peek().CheckSymbol(id, theCategory);
            //    if (!symbolFound) tempStack.Push(currentScope.Pop());
            //}

            //while (tempStack.Count > 0)
            //    currentScope.Push(tempStack.Pop());

            //return symbolFound;
        }

        /// <summary>
        /// Pops the current scope
        /// </summary>
        internal SymbolTable ExitScope()
        {
            currentScope = currentScope.getEnclosingScope();
            return currentScope;
        }

        /// <summary>
        /// Pushes a certain symboTable as the current scope.
        /// </summary>
        /// <param name="theTable"></param>
        internal void PushSymbolTable(SymbolTable theTable)
        {
            currentScope = new SymbolTable(theTable);
        }
    }
}
