
namespace DecafIde.ILCode.ClassMembers.MethodMembers.Statements
{
    abstract class ExpressionBaseClass : StatementBaseClass
    {
        private string expressionType;

        public string ExpressionType
        {
            get { return expressionType; }
            set { expressionType = value; }
        }


        private string pushCode;

        public string PushCode
        {
            get { return pushCode; }
            set { pushCode = value; }
        }

        public override string getTemplateName()
        {
            return "Expression";
        }
    }
}
