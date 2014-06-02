
using DecafIde.HelperClasses;
using System.Collections.Generic;
namespace DecafIde.ILCode.ClassMembers.MethodMembers.Statements
{
    class MethodCallStatement : StatementBaseClass
    {
        string methodType;

        public string MethodType
        {
            get { return methodType; }
            set { methodType = TypeMapper.getMappedType(value); }
        }
        string methodName;

        public string MethodName
        {
            get { return methodName; }
            set { methodName = value; }
        }

        List<ExpressionBaseClass> parameterList = new List<ExpressionBaseClass>();

        public override string getTemplateName()
        {
            return "MethodCall";
        }

        public override void setGenCode()
        {
            setCommandList(parameterList);
            setPlaceholder("methodType", methodType);
            setPlaceholder("methodName", methodName);
            parameterList.ForEach(p => setPlaceholder("parameterType", TypeMapper.getMappedType(p.ExpressionType)));
        }

        public void addExpression(ExpressionBaseClass theVariable)
        {
            parameterList.Add(theVariable);
        }
    }
}
