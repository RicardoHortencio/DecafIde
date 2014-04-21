using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecafIde.LipBased
{
    class StructSymbol: ScopedSymbol, IType, IScope
    {
        Dictionary<String, Symbol> fields = new Dictionary<string, Symbol>();

        public StructSymbol(string name, IScope parent) : base(name, parent) { }

        public Symbol resolveMember(string name)
        {
            Symbol result = null;
            fields.TryGetValue(name, out result);
            return result;
        }

        override public Dictionary<String, Symbol> getMembers()
        {
            return fields;
        }

        public override string ToString()
        {
            return "struct"+getName()+":{"+
                stripBrackets(fields.Keys.ToString()) + "}";
        }

        public SymbolType getTypeIndex()
        {
            return SymbolType.Tuser;
        }
    }
}
