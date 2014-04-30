using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecafIde.ILCode.ClassMembers
{
    class MethodDeclaration : Command
    {
        public override string getTemplateName()
        {
            return "Method";
        }

        public override void setGenCode()
        {
            throw new NotImplementedException();
        }
    }
}
