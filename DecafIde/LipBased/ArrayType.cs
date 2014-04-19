using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecafIde.LipBased
{
    class ArrayType : Symbol, Type
    {
        Type elementType;

        public ArrayType(Type theElementType)
            : base(theElementType + "[]")
        {
            this.elementType = theElementType;
        }

        public int getTypeIndex()
        {
            return 0;
        }
    }
}
