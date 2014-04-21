using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecafIde.LipBased
{
    public interface IType
    {
        string getName();
        SymbolType getTypeIndex();
    }
}
