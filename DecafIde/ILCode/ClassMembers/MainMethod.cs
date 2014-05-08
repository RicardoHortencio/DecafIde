using DecafIde.ILCode.ClassMembers.MethodMembers;
using System.Collections.Generic;

namespace DecafIde.ILCode.ClassMembers
{
    class MainMethod : Command
    {
        int stackSize = 1;

        public int StackSize
        {
            get { return stackSize; }
            set { stackSize = value; }
        }
        List<LocalVar> locals = new List<LocalVar>();
        List<StatementBaseClass> statements = new List<StatementBaseClass>();
        public override string getTemplateName()
        {
            return "MainMethod";
        }

        public override void setGenCode()
        {
            setPlaceholder("stackSize", stackSize.ToString());
            setCommandList(locals);
            setCommandList(statements);
        }

        public void addCommand(LocalVar theVariable)
        {
            locals.Add(theVariable);
        }

        public void addCommand(StatementBaseClass theStatement)
        {
            statements.Add(theStatement);
        }
    }
}
