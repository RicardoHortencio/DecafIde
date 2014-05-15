
namespace DecafIde.ILCode.ClassMembers.MethodMembers.Statements.Expressions.BinaryOperators
{
    public enum EqualityOperators
    {
        equal,
        notEqual
    }
    class EqOp : BinaryOperator, BooleanExpression
    {
        EqualityOperators currentOperation;

        public EqualityOperators CurrentOperation
        {
            get { return currentOperation; }
            set { currentOperation = value; }
        }
        public override string getOperatorCommand()
        {
            return "ceq";
        }

        public bool RequireNot()
        {
            return currentOperation == EqualityOperators.notEqual;
        }
    }
}
