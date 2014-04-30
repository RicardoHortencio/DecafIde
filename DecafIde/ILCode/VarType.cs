using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecafIde.ILCode
{
    class VarType : Command
    {
        string typeName;

        public string TypeName
        {
            get { return typeName; }
            set { typeName = TypeMapper.getMappedType(value); }
        }
        bool isStruct;

        public bool IsStruct
        {
            get { return isStruct; }
            set { isStruct = value; }
        }
        public override string getTemplateName()
        {
            return "TypeName";
        }

        public override void setGenCode()
        {
            setPlaceholder("isStruct", isStruct);
            setPlaceholder("typeName", typeName);
        }        
    }
}
