
namespace DecafIde.ILCode.ClassMembers.MethodMembers.Statements.Expressions
{
    abstract class LocationStmt : ExpressionBaseClass
    {
        public abstract string getLoadLocationCode();
        public abstract string getStoreLocationCode();
    }
}
