using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecafIde.LipBased
{
    class MethodSymbol:ScopedSymbol
    {
        Dictionary<String, Symbol> orderedArgs = new Dictionary<string, Symbol>();

        public MethodSymbol(String name, IType retType, IScope parent):base(name, retType, parent) {}

        public override Dictionary<string, Symbol> getMembers()
        {
            return orderedArgs;
        }

        public String getName() {
            return getName() + "(" + stripBrackets(orderedArgs.Keys.ToString()) + ")";
        }
    }
}
