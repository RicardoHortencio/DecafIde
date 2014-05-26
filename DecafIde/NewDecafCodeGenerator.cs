using Antlr4.Runtime.Tree;
using DecafIde.ILCode;
using DecafIde.ILCode.ClassMembers;
using System;

namespace DecafIde
{
    class NewDecafCodeGenerator : NewDecafBaseListener
    {
        public String finalResult;
        MainClass mainClass = new MainClass();

        ParseTreeProperty<Command> nodeCmd = new ParseTreeProperty<Command>();
        public void setNodeCommand(IParseTree node, Command value) { nodeCmd.Put(node, value); }
        public Command getNodeCommand(IParseTree node) { return nodeCmd.Get(node); }

        public override void EnterProgram(NewDecafParser.ProgramContext context)
        {
            setNodeCommand(context, mainClass);
        }

        public override void ExitProgram(NewDecafParser.ProgramContext context)
        {
            finalResult = mainClass.getGenCode();
        }

        public override void ExitSingle_varDeclaration(NewDecafParser.Single_varDeclarationContext context)
        {
            FieldDeclaration FD = new FieldDeclaration();
            VarType currentTypeName = getNodeCommand(context.varType()) as VarType;
            FD.TypeName = currentTypeName;
            FD.FieldId = context.Id().GetText();
            FD.IsArray = false;

            mainClass.addCommand(FD);

            setNodeCommand(context, FD);
        }

        public override void ExitArray_varDeclaration(NewDecafParser.Array_varDeclarationContext context)
        {
            FieldDeclaration FD = new FieldDeclaration();
            VarType currentTypeName = getNodeCommand(context.varType()) as VarType;

            FD.TypeName = currentTypeName;
            FD.FieldId = context.Id().GetText();
            FD.IsArray = true;

            mainClass.addCommand(FD);


            ArrayDeclaration AD = new ArrayDeclaration
            {
                Type = currentTypeName.getGenCode(),
                Name = FD.FieldId,
                ArraySize = Convert.ToInt32(context.Num().GetText())
            };

            mainClass.addCommand(AD);
        }

        public override void EnterStructDeclaration(NewDecafParser.StructDeclarationContext context)
        {
            StructDeclaration SD = new StructDeclaration();
            setNodeCommand(context, SD);
        }

        public override void EnterSingle_structVarDeclaration(NewDecafParser.Single_structVarDeclarationContext context)
        {
            FieldDeclaration FD = new FieldDeclaration();
            VarType currentTypeName = getNodeCommand(context.varType()) as VarType;

            FD.TypeName = currentTypeName;
            FD.FieldId = context.Id().GetText();
            FD.IsArray = false;

            (getNodeCommand(context.Parent) as StructDeclaration).addCommand(FD);

            setNodeCommand(context, FD);
        }

        public override void EnterArray_structVarDeclaration(NewDecafParser.Array_structVarDeclarationContext context)
        {
            FieldDeclaration FD = new FieldDeclaration();
            VarType currentTypeName = getNodeCommand(context.varType()) as VarType;
            FD.TypeName = currentTypeName;
            FD.FieldId = context.Id().GetText();
            FD.IsArray = true;

            (getNodeCommand(context.Parent) as StructDeclaration).addCommand(FD);

            setNodeCommand(context, FD);

        }

        public override void ExitStructDeclaration(NewDecafParser.StructDeclarationContext context)
        {
            StructDeclaration SD = getNodeCommand(context) as StructDeclaration;
            SD.NameField = context.Id().GetText();

            mainClass.addCommand(SD);
        }

    }
}
