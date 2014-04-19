using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecafIde.LipBased
{
    public interface Scope
    {
        string getScopeName();
        Scope getEnclosingScope();
        void define(Symbol sym);
        Symbol resolve(String name);
    }
}
