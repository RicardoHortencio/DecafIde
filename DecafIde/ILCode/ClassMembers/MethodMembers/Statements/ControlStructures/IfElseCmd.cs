
using DecafIde.HelperClasses;
using System.Collections.Generic;
namespace DecafIde.ILCode.ClassMembers.MethodMembers.Statements.ControlStructures
{
    class IfElseCmd : StatementBaseClass
    {
        ExpressionBaseClass theCondition;

        internal ExpressionBaseClass TheCondition
        {
            get { return theCondition; }
            set { theCondition = value; }
        }
        string labelName = LabelNameGenerator.getLabelName("if");
        string elseLabelName = LabelNameGenerator.getLabelName("else");
        List<StatementBaseClass> code;
        List<StatementBaseClass> elseCode;

        public override string getTemplateName()
        {
            return "IfElse";
        }

        public override void setGenCode()
        {
            setSingleCommand(theCondition);
            setPlaceholder("L0", labelName);
            code.ForEach(c => setPlaceholder("ifCode", c.getGenCode()));
            setPlaceholder("L1", labelName);
            elseCode.ForEach(c => setPlaceholder("elseCode", c.getGenCode()));
        }

        public void addStatement(StatementBaseClass theStatement)
        {
            code.Add(theStatement);
        }
        public void addElseStatement(StatementBaseClass theStatement)
        {
            elseCode.Add(theStatement);
        }
    }
}
