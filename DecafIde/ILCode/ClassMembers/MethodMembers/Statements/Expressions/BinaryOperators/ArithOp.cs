
using System;
namespace DecafIde.ILCode.ClassMembers.MethodMembers.Statements.Expressions.BinaryOperators
{
    public enum ArithmeticOperations
    {
        add,
        substract,
        multiplication,
        division,
        remainder
    }
    class ArithOp : BinaryOperator
    {
        ArithmeticOperations currentOperation;

        public ArithmeticOperations CurrentOperation
        {
            get { return currentOperation; }
            set { currentOperation = value; }
        }

        public string getOperatorCommand()
        {
            switch (CurrentOperation)
            {
                case ArithmeticOperations.add:
                    return "add";
                case ArithmeticOperations.substract:
                    return "sub";
                case ArithmeticOperations.multiplication:
                    return "mul";
                case ArithmeticOperations.division:
                    return "div";
                case ArithmeticOperations.remainder:
                    return "rem";
                default:
                    throw new Exception("Arithmetic Operator Expected");
            }
        }
    }
}
