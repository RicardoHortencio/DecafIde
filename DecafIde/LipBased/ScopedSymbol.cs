using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecafIde.LipBased
{
    abstract class ScopedSymbol: Symbol, Scope
    {
        Scope enclosingScope;

        public ScopedSymbol(string name, Type type, Scope theEnclosingScope)
            :base(name, type)
        {
            this.enclosingScope = theEnclosingScope;
        }

        public ScopedSymbol(string name, Scope theEnclosingScope)
            : base(name)
        {
            this.enclosingScope = theEnclosingScope;
        }

        public Symbol resolve(string name)
        {
            Symbol s;
            if (getMembers().TryGetValue(name, out s))
            {
                return s;
            }
            else
                if (getEnclosingScope() != null)
                    return getEnclosingScope().resolve(name);
                else
                    return null;
        }
        
        public Symbol resolveType(String name)
        {
            return resolve(name);
        }

        public void define(Symbol sym)
        {
            getMembers().Add(sym.getName(), sym);
            sym.Scope = this;
        }
        
        public Scope getEnclosingScope()
        {
            return enclosingScope;
        }

        public string getScopeName()
        {
            return getName();
        }
        
        public abstract Dictionary<String, Symbol> getMembers();
    }
}
