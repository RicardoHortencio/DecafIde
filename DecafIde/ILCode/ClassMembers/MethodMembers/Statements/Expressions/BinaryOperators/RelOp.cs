
using System;
namespace DecafIde.ILCode.ClassMembers.MethodMembers.Statements.Expressions.BinaryOperators
{
    public enum RelationalOperators
    {
        lessThan,
        greaterThan,
        lessOrEqualTo,
        greaterOrEqualTo,
    }
    class RelOp : BinaryOperator, BooleanExpression
    {
        RelationalOperators currentOperation;

        public RelationalOperators CurrentOperation
        {
            get { return currentOperation; }
            set { currentOperation = value; }
        }


        public bool RequireNot()
        {
            switch (CurrentOperation)
            {
                case RelationalOperators.lessThan:
                case RelationalOperators.greaterThan:
                    return false;
                case RelationalOperators.lessOrEqualTo:
                case RelationalOperators.greaterOrEqualTo:
                    return true;
                default:
                    throw new Exception("Relational Operator Expected");
            }
        }

        public string getOperatorCommand()
        {
            switch (CurrentOperation)
            {
                case RelationalOperators.lessThan:
                case RelationalOperators.greaterOrEqualTo:
                    return "clt";
                case RelationalOperators.lessOrEqualTo:
                case RelationalOperators.greaterThan:
                    return "cgt";
                default:
                    throw new Exception("Relational Operator Expected");
            }
        }
    }
}
