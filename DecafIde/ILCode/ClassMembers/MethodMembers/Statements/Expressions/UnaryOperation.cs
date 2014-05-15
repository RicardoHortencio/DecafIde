
namespace DecafIde.ILCode.ClassMembers.MethodMembers.Statements.Expressions
{
    abstract class UnaryOperation : ExpressionBaseClass
    {
        ExpressionBaseClass exp;
        public abstract string getOperatorCommand();

        public override void setGenCode()
        {
            setSingleCommand("expressions", exp);
            setPlaceholder("op", getOperatorCommand());
        }
    }
}
