using DecafIde.ILCode.ClassMembers.MethodMembers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecafIde.ILCode.ClassMembers
{
    class MainMethod : Command
    {
        VarType methodType;
        int stackSize;
        List<LocalVariable> locals = new List<LocalVariable>();
        public override string getTemplateName()
        {
            return "MainMethod";
        }

        public override void setGenCode()
        {
            throw new NotImplementedException();
        }
    }
}
