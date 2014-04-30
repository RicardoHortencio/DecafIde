using Antlr4.StringTemplate;
using DecafIde.ILCode.ClassMembers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecafIde.ILCode
{
    class MainClass : Command
    {

        List<StructDeclaration> structDeclList = new List<StructDeclaration>();
        List<FieldDeclaration> fieldDeclList = new List<FieldDeclaration>();
        List<MethodDeclaration> methodDeclList = new List<MethodDeclaration>();
        MainMethod mainMethod = new MainMethod();

        internal MainMethod MainMethod
        {
            get { return mainMethod; }
            set { mainMethod = value; }
        }

        public override string getTemplateName()
        {
            return "Main";
        }
        public override void setGenCode()
        {
            setCommandList(structDeclList);
            setCommandList(fieldDeclList);
            setSingleCommand(mainMethod);
            setCommandList(methodDeclList);

            //UNDONE ConstructorImplementation
        }

        public void addCommand(StructDeclaration theCommand)
        {
            structDeclList.Add(theCommand);
        }
        public void addComand(FieldDeclaration theCommand)
        {
            fieldDeclList.Add(theCommand);
        }
        public void addCommand(MethodDeclaration theCommand)
        {
            methodDeclList.Add(theCommand);
        }
        public void addCommand(MainMethod theCommand)
        {
            mainMethod = theCommand;
        }

        public MainClass() : base() { }

        public MainClass(List<StructDeclaration> theStructDeclList, 
            List<FieldDeclaration> theFieldDeclList,
            List<MethodDeclaration> theMethodDeclList)
            : this()
        {
            structDeclList = theStructDeclList;
            fieldDeclList = theFieldDeclList;
            methodDeclList = theMethodDeclList;
        }
    }
}
