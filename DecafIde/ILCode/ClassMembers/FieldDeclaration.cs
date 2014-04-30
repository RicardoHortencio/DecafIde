using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecafIde.ILCode.ClassMembers
{
    class FieldDeclaration : Command
    {
        VarType theVartype;
        internal VarType TheVartype
        {
            get { return theVartype; }
            set { theVartype = value; }
        }

        string fieldId;
        public string FieldId
        {
            get { return fieldId; }
            set { fieldId = value; }
        }

        bool isArray;
        public bool IsArray
        {
            get { return isArray; }
            set { isArray = value; }
        }

        public override string getTemplateName()
        {
            return "Field";
        }
        public override void setGenCode()
        {
            setSingleCommand(theVartype);
            setPlaceholder("id", fieldId);
            setPlaceholder("isArray", isArray);
        }
    }
}
