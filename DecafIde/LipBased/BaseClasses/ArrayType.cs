using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecafIde.LipBased
{
    class ArrayType : Symbol, IType
    {
        IType elementType;

        public ArrayType(IType theElementType)
            : base(theElementType + "[]")
        {
            this.elementType = theElementType;
        }

        public SymbolType getTypeIndex()
        {
            return SymbolType.Tuser;
        }
    }
}
