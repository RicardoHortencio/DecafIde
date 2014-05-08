
namespace DecafIde.ILCode.ClassMembers.MethodMembers
{
    class Parameter : Command
    {
        VarType typeName;
        internal VarType TypeName
        {
            get { return typeName; }
            set { typeName = value; }
        }

        string parameterId;
        public string ParameterId
        {
            get { return parameterId; }
            set { parameterId = value; }
        }

        public override string getTemplateName()
        {
            return "Parameter";
        }
        public override void setGenCode()
        {
            setSingleCommand(typeName);
            setPlaceholder("id", parameterId);
        }
    }
}
