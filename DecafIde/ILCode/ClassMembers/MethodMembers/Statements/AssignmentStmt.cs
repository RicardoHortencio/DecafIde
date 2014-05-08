
using DecafIde.ILCode.ClassMembers.MethodMembers.Statements.Expressions;
namespace DecafIde.ILCode.ClassMembers.MethodMembers.Statements
{
    class AssignmentStmt : StatementBaseClass
    {
        LocationStmt theLocation;

        internal LocationStmt TheLocation
        {
            get { return theLocation; }
            set { theLocation = value; }
        }

        ExpressionBaseClass theExpression;

        internal ExpressionBaseClass TheExpression
        {
            get { return theExpression; }
            set { theExpression = value; }
        }

        public override string getTemplateName()
        {
            return "Assignment";
        }

        public override void setGenCode()
        {
            setPlaceholder("loadLocation", theLocation.getLoadLocationCode());
            setSingleCommand(theExpression);
            setPlaceholder("storeLocation", theLocation.getStoreLocationCode());
        }
    }
}
