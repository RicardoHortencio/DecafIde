using DecafIde.ILCode.ClassMembers.MethodMembers;
using System.Collections.Generic;

namespace DecafIde.ILCode.ClassMembers
{
    class MethodDeclaration : Command
    {
        VarType typeName;

        internal VarType TypeName
        {
            get { return typeName; }
            set { typeName = value; }
        }
        string methodId;

        public string MethodId
        {
            get { return methodId; }
            set { methodId = value; }
        }
        int stackSize = 1;

        public int StackSize
        {
            get { return stackSize; }
            set { stackSize = value; }
        }
        List<Parameter> parameters = new List<Parameter>();
        List<LocalVar> locals = new List<LocalVar>();
        List<StatementBaseClass> statements = new List<StatementBaseClass>();
        public override string getTemplateName()
        {
            return "Method";
        }
        public override void setGenCode()
        {
            setSingleCommand(typeName);
            setPlaceholder("id", methodId);
            setCommandList(parameters);
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

        public void addCommand(Parameter theParameter)
        {
            parameters.Add(theParameter);
        }
    }
}
