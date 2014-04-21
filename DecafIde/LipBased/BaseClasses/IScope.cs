using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecafIde.LipBased
{
    public interface IScope
    {
        string getScopeName();
        IScope getEnclosingScope();
        void define(Symbol sym);
        Symbol resolve(String name);
    }
}
