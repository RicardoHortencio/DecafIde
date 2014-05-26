namespace DecafIde.ILCode.ClassMembers.MethodMembers.Statements.Expressions
{
    abstract class BinaryOperation : ExpressionBaseClass
    {
        ExpressionBaseClass exp1;
        ExpressionBaseClass exp2;
        BinaryOperator operation;

        internal BinaryOperator Operation
        {
            get { return operation; }
            set { operation = value; }
        }

        internal ExpressionBaseClass Exp2
        {
            get { return exp2; }
            set { exp2 = value; }
        }

        internal ExpressionBaseClass Exp1
        {
            get { return exp1; }
            set { exp1 = value; }
        }

        public override string getTemplateName()
        {
            return "BinaryOp";
        }

        public override void setGenCode()
        {
            setSingleCommand("expressionOne", exp1);
            setSingleCommand("expressionTwo", exp2);
            setPlaceholder("op", operation.getOperatorCommand());
            if (operation is BooleanExpression)
            {
                setPlaceholder("requireNot", (operation as BooleanExpression).RequireNot());
            }
        }
    }
}
