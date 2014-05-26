using System;
namespace DecafIde.ILCode.ClassMembers.MethodMembers.Statements.Expressions.BinaryOperators
{
    public enum ConditionalOperators
    {
        and,
        or
    }

    class CondOp : BinaryOperator, BooleanExpression
    {
        ConditionalOperators currentOperation;

        public ConditionalOperators CurrentOperation
        {
            get { return currentOperation; }
            set { currentOperation = value; }
        }

        public string getOperatorCommand()
        {
            switch (CurrentOperation)
            {
                case ConditionalOperators.and:
                    return "and";
                case ConditionalOperators.or:
                    return "or";
                default:
                    throw new Exception("Conditional Operator Expected");
            }
        }

        public bool RequireNot()
        {
            return false;
        }
    }
}
