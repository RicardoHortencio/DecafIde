
namespace DecafIde.ILCode.ClassMembers.MethodMembers.Statements
{
    class ReturnStmt : StatementBaseClass
    {
        ExpressionBaseClass theExpression;

        public ExpressionBaseClass TheExpression
        {
            get { return theExpression; }
            set { theExpression = value; }
        }
        public override string getTemplateName()
        {
            return "Return";
        }

        public override void setGenCode()
        {
            setSingleCommand(theExpression);
        }
    }
}
