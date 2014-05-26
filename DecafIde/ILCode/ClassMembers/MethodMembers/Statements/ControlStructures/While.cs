using DecafIde.HelperClasses;
using System.Collections.Generic;
namespace DecafIde.ILCode.ClassMembers.MethodMembers.Statements.ControlStructures
{
    class While : StatementBaseClass
    {
        ExpressionBaseClass theCondition;

        internal ExpressionBaseClass TheCondition
        {
            get { return theCondition; }
            set { theCondition = value; }
        }
        string labelName = LabelNameGenerator.getLabelName("loop");
        string endLabelName = LabelNameGenerator.getLabelName("endWhile");

        List<StatementBaseClass> code = new List<StatementBaseClass>();

        public override string getTemplateName()
        {
            return "IfCmd";
        }

        public override void setGenCode()
        {
            setSingleCommand(theCondition);
            setPlaceholder("L0", labelName);
            setPlaceholder("L1", endLabelName);
            code.ForEach(c => setPlaceholder("statements", c.getGenCode()));
        }

        public void addStatement(StatementBaseClass theStatement)
        {
            code.Add(theStatement);
        }
    }
}
