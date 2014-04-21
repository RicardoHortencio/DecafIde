using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecafIde.LipBased
{
    abstract class BaseScope : IScope
    {
        IScope enclosingScope;
        Dictionary<string, Symbol> symbols = new Dictionary<string, Symbol>();

        public BaseScope(IScope parent)
        {
            this.enclosingScope = parent;
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

        public void define(Symbol sym)
        {
            symbols.Add(sym.getName(), sym);
            sym.Scope = this;
        }

        public IScope getEnclosingScope()
        {
            return enclosingScope;
        }

        public override string ToString()
        {
            return symbols.Keys.ToString();
        }

        public string getScopeName()
        {
            return "";
        }
    }
}
