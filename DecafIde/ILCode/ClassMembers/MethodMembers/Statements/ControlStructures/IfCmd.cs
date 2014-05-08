
using DecafIde.HelperClasses;
using System.Collections.Generic;
namespace DecafIde.ILCode.ClassMembers.MethodMembers.Statements.ControlStructures
{
    class IfCmd : StatementBaseClass
    {
        ExpressionBaseClass theCondition;

        internal ExpressionBaseClass TheCondition
        {
            get { return theCondition; }
            set { theCondition = value; }
        }
        string labelName = LabelNameGenerator.getLabelName("if");
        List<StatementBaseClass> code;

        public override string getTemplateName()
        {
            return "IfCmd";
        }

        public override void setGenCode()
        {
            setSingleCommand(theCondition);
            setPlaceholder("L0", labelName);
            code.ForEach(c => setPlaceholder("statements", c.getGenCode()));
        }

        public void addStatement(StatementBaseClass theStatement)
        {
            code.Add(theStatement);
        }
    }
}
