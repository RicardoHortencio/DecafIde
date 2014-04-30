using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Antlr4.StringTemplate;

namespace DecafIde
{
    class ILGenerator : NewDecafBaseListener
    {
        public String finalResult;
        TemplateGroup stg = new TemplateGroupFile(@"C:\Users\Paulo\SkyDrive\Visual Studio 2013\Projects\DecafIde\DecafIde\TAC.stg");
        Stack<Template> MainST = new Stack<Template>();
        Stack<Template> StructDecST = new Stack<Template>(); //ya
        Stack<Template> FieldST = new Stack<Template>();
        Stack<Template> TypeNameST = new Stack<Template>();
        Stack<Template> ParameterST = new Stack<Template>(); //ya
        Stack<Template> MainMethodST = new Stack<Template>();
        Stack<Template> MethodST = new Stack<Template>();
        Stack<Template> BinaryOpST = new Stack<Template>();
        Stack<Template> AssignmentST = new Stack<Template>();
        Stack<Template> IfST = new Stack<Template>();
        Stack<Template> IfElseST = new Stack<Template>();
        Stack<Template> WhileST = new Stack<Template>();
        Stack<Template> ReturnST = new Stack<Template>();
        Stack<Template> MethodCallST = new Stack<Template>();
        Stack<Template> VarDeclST = new Stack<Template>();
        Stack<Template> VarStructDeclST = new Stack<Template>();
        Stack<Template> ArrayDeclST = new Stack<Template>();


        public ILGenerator(string theFinalResult)
            : base()
        {
            finalResult = theFinalResult;
        }

        public override void EnterProgram(NewDecafParser.ProgramContext context)
        {
            MainST.Push(stg.GetInstanceOf("Main"));
        }

        public override void ExitProgram(NewDecafParser.ProgramContext context)
        {
            finalResult = MainST.Pop().Render();            
        }

        public override void EnterStructDeclaration(NewDecafParser.StructDeclarationContext context)
        {
            StructDecST.Push(stg.GetInstanceOf("StructDec"));
        }

        public override void ExitStructDeclaration(NewDecafParser.StructDeclarationContext context)
        {
            MainST.Peek().Add("field", StructDecST.Pop().Render());
        }

        public override void EnterMethodDeclaration(NewDecafParser.MethodDeclarationContext context)
        {
            MethodST.Push(stg.GetInstanceOf("Method"));
        }

        public override void ExitMethodDeclaration(NewDecafParser.MethodDeclarationContext context)
        {
            MainST.Peek().Add("method", MethodST.Pop().Render());
        }

        public override void EnterMainMethodDeclaration(NewDecafParser.MainMethodDeclarationContext context)
        {
            MethodST.Push(stg.GetInstanceOf("MainMethod"));
        }

        public override void ExitMainMethodDeclaration(NewDecafParser.MainMethodDeclarationContext context)
        {
            MainST.Peek().Add("method", MethodST.Pop().Render());
        }
        public override void ExitMethodType(NewDecafParser.MethodTypeContext context)
        {
            MethodST.Peek().Add("type", context.GetText());
        }

        public override void EnterSingle_parameterDeclaration(NewDecafParser.Single_parameterDeclarationContext context)
        {
            ParameterST.Push(stg.GetInstanceOf("Parameter"));
        }

        public override void ExitParameterType(NewDecafParser.ParameterTypeContext context)
        {

            ParameterST.Peek().Add("typeName", context.GetText());
        }

        public override void ExitSingle_parameterDeclaration(NewDecafParser.Single_parameterDeclarationContext context)
        {
            ParameterST.Peek().Add("parameterId", context.Id());
            MethodST.Peek().Add("parameters", ParameterST.Pop().Render());
        }

        public override void ExitMethodSignature(NewDecafParser.MethodSignatureContext context)
        {
            MethodST.Peek().Add("id", context.Id().GetText());
        }

        public override void EnterInt_varType(NewDecafParser.Int_varTypeContext context)
        {
            TypeNameST.Push(stg.GetInstanceOf("TypeName"));
        }

        public override void ExitInt_varType(NewDecafParser.Int_varTypeContext context)
        {
            TypeNameST.Peek().Add("typeName", "int32");
        }

        public override void EnterVoid_varType(NewDecafParser.Void_varTypeContext context)
        {
            TypeNameST.Push(stg.GetInstanceOf("TypeName"));
        }

        public override void ExitVoid_varType(NewDecafParser.Void_varTypeContext context)
        {
            TypeNameST.Peek().Add("typeName", "void");
        }

        public override void EnterChar_varType(NewDecafParser.Char_varTypeContext context)
        {
            TypeNameST.Push(stg.GetInstanceOf("TypeName"));
        }

        public override void ExitChar_varType(NewDecafParser.Char_varTypeContext context)
        {
            TypeNameST.Peek().Add("typeName", "char");
        }

        public override void EnterBoolean_varType(NewDecafParser.Boolean_varTypeContext context)
        {
            TypeNameST.Push(stg.GetInstanceOf("TypeName"));
        }

        public override void ExitBoolean_varType(NewDecafParser.Boolean_varTypeContext context)
        {
            TypeNameST.Peek().Add("typeName", "bool");
        }

        public override void EnterStructImpl_varType(NewDecafParser.StructImpl_varTypeContext context)
        {
            TypeNameST.Push(stg.GetInstanceOf("TypeName"));
        }

        public override void ExitStructImpl_varType(NewDecafParser.StructImpl_varTypeContext context)
        {
            TypeNameST.Peek().Add("typeName", context.Id().GetText());
            TypeNameST.Peek().Add("isStruct", true);
        }

        public override void EnterStructDecl_varType(NewDecafParser.StructDecl_varTypeContext context)
        {
            TypeNameST.Push(stg.GetInstanceOf("TypeName"));
        }

        public override void ExitStructDecl_varType(NewDecafParser.StructDecl_varTypeContext context)
        {
            TypeNameST.Peek().Add("typeName", context.structDeclaration().Id().GetText());
            TypeNameST.Peek().Add("isStruct", true);
        }

        public override void EnterSingle_varDeclaration(NewDecafParser.Single_varDeclarationContext context)
        {
            FieldST.Push(stg.GetInstanceOf("Field"));
        }


        public override void ExitSingle_varDeclaration(NewDecafParser.Single_varDeclarationContext context)
        {
            FieldST.Peek().Add("typeName", TypeNameST.Pop().Render());
            FieldST.Peek().Add("id", context.Id().GetText());
            MainST.Peek().Add("field", FieldST.Pop().Render());
        }

        public override void EnterArray_varDeclaration(NewDecafParser.Array_varDeclarationContext context)
        {
            FieldST.Push(stg.GetInstanceOf("Field"));
        }

        public override void ExitArray_varDeclaration(NewDecafParser.Array_varDeclarationContext context)
        {
            FieldST.Peek().Add("isArray", true);
            FieldST.Peek().Add("typeName", TypeNameST.Pop().Render());
            FieldST.Peek().Add("id", context.Id().GetText());
            MainST.Peek().Add("field", FieldST.Pop().Render());
        }
    }
}
