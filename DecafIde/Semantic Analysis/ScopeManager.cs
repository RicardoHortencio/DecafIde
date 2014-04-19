using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        Stack<SymbolTable> theStack;
        SymbolType returnType = SymbolType.Tvoid;
        Boolean hasReturn = false;


        public void resetReturn()
        {
            hasReturn = false;
        }

        public Boolean HasReturn
        {
            get { return hasReturn; }
        }

        public SymbolType ReturnType
        {
            get { return returnType; }
            set
            {
                returnType = value;
                hasReturn = true;
            }
        }

        /// <summary>
        /// Gets and pops the current scope
        /// </summary>
        /// <returns>The current scope</returns>
        internal SymbolTable GetAndExitScope()
        {
            return theStack.Pop();
        }

        /// <summary>
        /// Initializes a new scope manager instance
        /// </summary>
        public ScopeManager()
        {
            theStack = new Stack<SymbolTable>();
        }

        /// <summary>
        /// Creates a new scope and sets it as the current scope
        /// </summary>
        internal void EnterScope()
        {
            theStack.Push(new SymbolTable());
        }

        public Symbol currentScopeFindSymbol(string id, symbolCategory theCategory)
        {
            Symbol result = null;

            result = theStack.Peek().FindSymbol(id, theCategory);
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
            Symbol result = null;
            Boolean symbolFound = false;
            Stack<SymbolTable> tempStack = new Stack<SymbolTable>();

            while ((!symbolFound) && (theStack.Count > 0))
            {
                result = theStack.Peek().FindSymbol(id, theCategory);
                symbolFound = result != null;
                if (!symbolFound) tempStack.Push(theStack.Pop());
            }


            if (theStack.Count <= 0) throw new
            Exception("Symbol " + id + " not found in symbolTable.");
            while (tempStack.Count > 0)
                theStack.Push(tempStack.Pop());



            return result;
        }

        /// <summary>
        /// Adds a symbol to the current scope symbol table
        /// </summary>
        /// <param name="id">The new symbol id</param>
        /// <param name="x">The new symbol instance</param>
        public void AddSymbol(string id, Symbol x)
        {
            theStack.Peek().Add(id, x);
        }

        /// <summary>
        /// Adds a symbol entrie to the current scope symbol table
        /// </summary>
        /// <param name="thePair">The keyValuePair valu</param>
        public void AddSymbol(KeyValuePair<string, Symbol> thePair)
        {
            theStack.Peek().Add(thePair);
        }

        /// <summary>
        /// Checks if the symbol with certain id and category is present in the current scope
        /// </summary>
        /// <param name="id">The symbol supposed id</param>
        /// <param name="theCategory">The symbol supposed category</param>
        /// <returns></returns>
        internal bool CheckScope(string id, symbolCategory theCategory)
        {
            Boolean symbolFound = false;
            Stack<SymbolTable> tempStack = new Stack<SymbolTable>();

            while ((!symbolFound) && (theStack.Count > 0))
            {
                symbolFound = theStack.Peek().CheckSymbol(id, theCategory);
                if (!symbolFound) tempStack.Push(theStack.Pop());
            }

            while (tempStack.Count > 0)
                theStack.Push(tempStack.Pop());

            return symbolFound;
        }

        /// <summary>
        /// Pops the current scope
        /// </summary>
        internal SymbolTable ExitScope()
        {
            return theStack.Pop();
        }

        internal void PushSymbolTable(SymbolTable theTable)
        {
            theStack.Push(theTable);
        }
    }
}
