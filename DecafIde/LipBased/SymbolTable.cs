using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecafIde.LipBased
{
    class SymbolTable
    {
        GlobalScope globals = new GlobalScope();

        protected void initTypeSystem()
        {
            globals.define(new BuiltInTypeSymbol("int"));
            globals.define(new BuiltInTypeSymbol("float"));
            globals.define(new BuiltInTypeSymbol("void"));
        }

        public SymbolTable()
        {
            initTypeSystem();
        }

        public string ToString()
        {
            return globals.ToString();
        }
    }
}
