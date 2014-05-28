using System;

namespace DecafIde.LipBased
{
    enum VariableType
    {
        vField,
        vParameter
    }
    class VariableSymbol : Symbol
    {
        VariableType variableType;
        int size = 0;

        public int Size
        {
            get { return size; }
            set { size = value; }
        }
        public VariableSymbol(String name, IType type, int theSize = 0, VariableType theVariableType = VariableType.vField)
            : base(name, type)
        {
            variableType = theVariableType;
            size = theSize;
        }

        public bool isArray()
        {
            return size != 0;
        }

        public bool isParameter()
        {
            return variableType == VariableType.vParameter;
        }


    }
}
