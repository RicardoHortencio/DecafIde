using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecafIde.LipBased
{
    abstract class BaseScope : Scope
    {
        Scope enclosingScope;
        Dictionary<string, Symbol> symbols = new Dictionary<string, Symbol>();

        public BaseScope(Scope parent)
        {
            this.enclosingScope = parent;
        }

        public Scope getEnclosingScope()
        {
            return enclosingScope;
        }

        public void define(Symbol sym)
        {
            symbols.Add(sym.getName(), sym);
            sym.Scope = this;
        }

        public Symbol resolve(string name)
        {
            Symbol s;
            if (!symbols.TryGetValue(name, out s))
                return s;
            else
                if (enclosingScope != null)
                    return enclosingScope.resolve(name);
                else
                    return null;
        }

        public override string ToString()
        {
            return symbols.Keys.ToString();
        }
    }
}
