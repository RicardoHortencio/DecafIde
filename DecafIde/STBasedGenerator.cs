
using Antlr4.Runtime.Tree;
using Antlr4.StringTemplate;
using DecafIde.HelperClasses;
using DecafIde.LipBased;
namespace DecafIde
{
    class STBasedGenerator : NewDecafBaseListener
    {
        static TemplateGroup stg = new TemplateGroupFile(@"C:\Users\Paulo\SkyDrive\Visual Studio 2013\Projects\DecafIde\DecafIde\TAC.stg");
        public Template mainTemplate = stg.GetInstanceOf("Main");

        ParseTreeProperty<Template> nodeTemplate = new ParseTreeProperty<Template>();
        ParseTreeProperty<Template> nodeInitTemplate = new ParseTreeProperty<Template>();
        ParseTreeProperty<bool> nodeRequiresInit = new ParseTreeProperty<bool>();

        ParseTreeProperty<IScope> scopes = new ParseTreeProperty<IScope>();

        public void setNodeTemplate(IParseTree node, Template value) { nodeTemplate.Put(node, value); }
        public Template getNodeTemplate(IParseTree node) { return nodeTemplate.Get(node); }


        public void setNodeInitTemplate(IParseTree node, Template value) { nodeInitTemplate.Put(node, value); }
        public Template getNodeInitTemplate(IParseTree node) { return nodeInitTemplate.Get(node); }

        public void setNodeRequiresInit(IParseTree node, bool value) { nodeRequiresInit.Put(node, value); }
        public bool getNodeRequiresInit(IParseTree node) { return nodeRequiresInit.Get(node); }

        public IScope getNodeScope(IParseTree node) { return scopes.Get(node); }
        public STBasedGenerator(ParseTreeProperty<IScope> scopedTree)
            : base()
        {
            scopes = scopedTree;
        }

        public override void EnterProgram(NewDecafParser.ProgramContext context)
        {
            setNodeTemplate(context, mainTemplate);
        }
        public override void ExitProgram(NewDecafParser.ProgramContext context)
        {
            Template selfTemplate = getNodeTemplate(context);

            foreach (NewDecafParser.StructDeclarationContext decCtx in context.structDeclaration())
            {
                Template childTemplate = getNodeTemplate(decCtx);
                selfTemplate.Add("structDec", childTemplate.Render());
            }

            foreach (NewDecafParser.VarDeclarationContext decCtx in context.varDeclaration())
            {
                Template childTemplate = getNodeTemplate(decCtx);
                selfTemplate.Add("field", childTemplate.Render());

                if (getNodeRequiresInit(decCtx)) selfTemplate.Add("arrayFieldInit", getNodeInitTemplate(decCtx).Render());
            }

            foreach (NewDecafParser.MethodDeclarationContext decCtx in context.methodDeclaration())
            {
                Template childTemplate = getNodeTemplate(decCtx);
                selfTemplate.Add("method", childTemplate.Render());
            }
            foreach (NewDecafParser.MainMethodDeclarationContext decCtx in context.mainMethodDeclaration())
            {
                Template childTemplate = getNodeTemplate(decCtx);
                selfTemplate.Add("mainMethod", childTemplate.Render());
            }
        }

        public override void ExitSingle_varDeclaration(NewDecafParser.Single_varDeclarationContext context)
        {
            Template selfTemplate = getNodeTemplate(context);
            selfTemplate.Add("typeName", getNodeTemplate(context.varType()).Render());
        }

        public override void EnterSingle_varDeclaration(NewDecafParser.Single_varDeclarationContext context)
        {
            Template selfTemplate = stg.GetInstanceOf("Field");
            selfTemplate.Add("id", context.Id().GetText());
            setNodeTemplate(context, selfTemplate);
            setNodeRequiresInit(context, false);
        }

        public override void ExitArray_varDeclaration(NewDecafParser.Array_varDeclarationContext context)
        {
            Template selfTemplate = getNodeTemplate(context);
            selfTemplate.Add("typeName", getNodeTemplate(context.varType()).Render());

            IScope currentContext = getNodeScope(context);
            VariableSymbol currentSymbol = currentContext.resolve(context.Id().GetText()) as VariableSymbol;

            Template helperTemplate = stg.GetInstanceOf("ArrayFieldInit");
            helperTemplate.Add("size", currentSymbol.Size);
            helperTemplate.Add("type", getNodeTemplate(context.varType()).Render());
            helperTemplate.Add("name", context.Id().GetText());

            setNodeInitTemplate(context, helperTemplate);

            //mainTemplate.Add("arrayFieldInit", helperTemplate.Render());
        }

        public override void EnterArray_varDeclaration(NewDecafParser.Array_varDeclarationContext context)
        {
            Template selfTemplate = stg.GetInstanceOf("Field");

            selfTemplate.Add("isArray", true);
            selfTemplate.Add("id", context.Id().GetText());
            setNodeRequiresInit(context, true);

            setNodeTemplate(context, selfTemplate);
        }

        public override void EnterSingle_structVarDeclaration(NewDecafParser.Single_structVarDeclarationContext context)
        {
            Template selfTemplate = stg.GetInstanceOf("Field");
            selfTemplate.Add("id", context.Id().GetText());
            setNodeRequiresInit(context, false);
            setNodeTemplate(context, selfTemplate);
        }
        public override void ExitSingle_structVarDeclaration(NewDecafParser.Single_structVarDeclarationContext context)
        {
            Template selfTemplate = getNodeTemplate(context);
            selfTemplate.Add("typeName", getNodeTemplate(context.varType()).Render());
            setNodeTemplate(context, selfTemplate);
        }

        public override void ExitArray_structVarDeclaration(NewDecafParser.Array_structVarDeclarationContext context)
        {
            Template selfTemplate = getNodeTemplate(context);
            selfTemplate.Add("typeName", getNodeTemplate(context.varType()).Render());

            IScope currentContext = getNodeScope(context);
            VariableSymbol currentSymbol = currentContext.resolve(context.Id().GetText()) as VariableSymbol;

            Template helperTemplate = stg.GetInstanceOf("ArrayFieldInit");
            helperTemplate.Add("size", currentSymbol.Size);
            helperTemplate.Add("type", getNodeTemplate(context.varType()).Render());
            helperTemplate.Add("name", context.Id().GetText());

            setNodeInitTemplate(context, helperTemplate);
        }

        public override void EnterArray_structVarDeclaration(NewDecafParser.Array_structVarDeclarationContext context)
        {
            Template selfTemplate = stg.GetInstanceOf("Field");

            selfTemplate.Add("isArray", true);
            selfTemplate.Add("id", context.Id().GetText());
            setNodeRequiresInit(context, true);

            setNodeTemplate(context, selfTemplate);
        }

        public override void EnterStructDeclaration(NewDecafParser.StructDeclarationContext context)
        {
            Template selfTemplate = stg.GetInstanceOf("StructDec");
            selfTemplate.Add("name", context.Id().GetText());

            setNodeTemplate(context, selfTemplate);
        }

        public override void ExitStructDeclaration(NewDecafParser.StructDeclarationContext context)
        {
            Template selfTemplate = getNodeTemplate(context);
            foreach (NewDecafParser.StructVarDeclarationContext sCtx in context.structVarDeclaration())
            {
                Template childTemplate = getNodeTemplate(sCtx);
                selfTemplate.Add("field", childTemplate.Render());
            }
        }

        public override void EnterSingle_blockVarDeclaration(NewDecafParser.Single_blockVarDeclarationContext context)
        {
            Template selfTemplate = stg.GetInstanceOf("LocalVar");
            selfTemplate.Add("id", context.Id().GetText());
            setNodeRequiresInit(context, false);
            setNodeTemplate(context, selfTemplate);
        }

        public override void ExitSingle_blockVarDeclaration(NewDecafParser.Single_blockVarDeclarationContext context)
        {
            Template selfTemplate = getNodeTemplate(context);
            selfTemplate.Add("typeName", getNodeTemplate(context.varType()).Render());
            setNodeTemplate(context, selfTemplate);
        }

        public override void EnterArray_blockVarDeclaration(NewDecafParser.Array_blockVarDeclarationContext context)
        {
            Template selfTemplate = stg.GetInstanceOf("LocalVar");

            selfTemplate.Add("isArray", true);
            selfTemplate.Add("id", context.Id().GetText());
            setNodeRequiresInit(context, true);

            setNodeTemplate(context, selfTemplate);
        }

        public override void ExitArray_blockVarDeclaration(NewDecafParser.Array_blockVarDeclarationContext context)
        {
            Template selfTemplate = getNodeTemplate(context);
            selfTemplate.Add("typeName", getNodeTemplate(context.varType()).Render());

            IScope currentContext = getNodeScope(context);
            VariableSymbol currentSymbol = currentContext.resolve(context.Id().GetText()) as VariableSymbol;

            Template helperTemplate = stg.GetInstanceOf("ArrayFieldInit");
            helperTemplate.Add("size", currentSymbol.Size);
            helperTemplate.Add("type", getNodeTemplate(context.varType()).Render());
            helperTemplate.Add("name", context.Id().GetText());

            setNodeInitTemplate(context, helperTemplate);
        }

        public override void EnterInt_varType(NewDecafParser.Int_varTypeContext context)
        {
            Template selfTemplate = stg.GetInstanceOf("TypeName");
            selfTemplate.Add("typeName", "int32");

            setNodeTemplate(context, selfTemplate);
        }

        public override void EnterChar_varType(NewDecafParser.Char_varTypeContext context)
        {
            Template selfTemplate = stg.GetInstanceOf("TypeName");
            selfTemplate.Add("typeName", "char");

            setNodeTemplate(context, selfTemplate);
        }

        public override void EnterBoolean_varType(NewDecafParser.Boolean_varTypeContext context)
        {
            Template selfTemplate = stg.GetInstanceOf("TypeName");
            selfTemplate.Add("typeName", "bool");

            setNodeTemplate(context, selfTemplate);
        }

        public override void EnterStructImpl_varType(NewDecafParser.StructImpl_varTypeContext context)
        {
            Template selfTemplate = stg.GetInstanceOf("TypeName");
            selfTemplate.Add("typeName", "Program/" + context.Id().GetText());
            selfTemplate.Add("isStruct", true);

            setNodeTemplate(context, selfTemplate);
        }

        public override void ExitStructDecl_varType(NewDecafParser.StructDecl_varTypeContext context)
        {
            Template selfTemplate = stg.GetInstanceOf("TypeName");
            selfTemplate.Add("typeName", "Program/" + context.structDeclaration().Id().GetText());
            selfTemplate.Add("isStruct", true);

            mainTemplate.Add("structDec", getNodeTemplate(context.structDeclaration()).Render());

            setNodeTemplate(context, selfTemplate);
        }

        public override void EnterVoid_varType(NewDecafParser.Void_varTypeContext context)
        {
            Template selfTemplate = stg.GetInstanceOf("TypeName");
            selfTemplate.Add("typeName", "void");

            setNodeTemplate(context, selfTemplate);
        }

        public override void EnterMainMethodDeclaration(NewDecafParser.MainMethodDeclarationContext context)
        {
            Template selfTemplate = stg.GetInstanceOf("MainMethod");

            setNodeTemplate(context, selfTemplate);
        }

        public override void ExitMainMethodDeclaration(NewDecafParser.MainMethodDeclarationContext context)
        {
            Template selfTemplate = getNodeTemplate(context);
            foreach (NewDecafParser.BlockVarDeclarationContext decCtx in context.block().blockVarDeclaration())
            {
                Template childTemplate = getNodeTemplate(decCtx);
                selfTemplate.Add("localVar", childTemplate.Render());

                if (getNodeRequiresInit(decCtx)) selfTemplate.Add("arrayInit", getNodeInitTemplate(decCtx).Render());
            }
            foreach (NewDecafParser.StatementContext decCtx in context.block().statement())
            {
                Template childTemplate = getNodeTemplate(decCtx);
                selfTemplate.Add("statements", childTemplate.Render());
            }
        }

        public override void EnterMethodDeclaration(NewDecafParser.MethodDeclarationContext context)
        {
            Template selfTemplate = stg.GetInstanceOf("Method");
            setNodeTemplate(context, selfTemplate);
        }

        public override void ExitMethodDeclaration(NewDecafParser.MethodDeclarationContext context)
        {
            Template selfTemplate = getNodeTemplate(context);
            selfTemplate.Add("methodSignature", getNodeTemplate(context.methodSignature()).Render());

            foreach (NewDecafParser.BlockVarDeclarationContext decCtx in context.block().blockVarDeclaration())
            {
                Template childTemplate = getNodeTemplate(decCtx);
                selfTemplate.Add("localVar", childTemplate.Render());

                if (getNodeRequiresInit(decCtx)) selfTemplate.Add("arrayInit", getNodeInitTemplate(decCtx).Render());
            }

            foreach (NewDecafParser.StatementContext decCtx in context.block().statement())
            {
                Template childTemplate = getNodeTemplate(decCtx);
                selfTemplate.Add("statements", childTemplate.Render());
            }
        }

        public override void EnterMethodSignature(NewDecafParser.MethodSignatureContext context)
        {
            Template selfTemplate = stg.GetInstanceOf("MethodSignature");
            setNodeTemplate(context, selfTemplate);
        }

        public override void ExitMethodSignature(NewDecafParser.MethodSignatureContext context)
        {
            Template selfTemplate = getNodeTemplate(context);
            selfTemplate.Add("typeName", TypeMapper.getMappedType(context.methodType().GetText()));
            selfTemplate.Add("id", context.Id().GetText());

            foreach (NewDecafParser.ParameterContext decCtx in context.parameter())
            {
                Template childTemplate = getNodeTemplate(decCtx);
                selfTemplate.Add("parameters", childTemplate.Render());
            }
        }

        public override void ExitSingle_parameterDeclaration(NewDecafParser.Single_parameterDeclarationContext context)
        {
            Template selfTemplate = stg.GetInstanceOf("Parameter");
            selfTemplate.Add("typeName", TypeMapper.getMappedType(context.parameterType().GetText()));
            selfTemplate.Add("id", context.Id().GetText());
            setNodeTemplate(context, selfTemplate);
        }

        public override void EnterIf_else_statement(NewDecafParser.If_else_statementContext context)
        {
            Template selfTemplate = stg.GetInstanceOf("IfElse");
            setNodeTemplate(context, selfTemplate);
        }

        public override void ExitIfBlock(NewDecafParser.IfBlockContext context)
        {
            Template selfTemplate = stg.GetInstanceOf("BaseBlock");
            foreach (NewDecafParser.StatementContext decCtx in context.statement())
            {
                Template childTemplate = getNodeTemplate(decCtx);
                selfTemplate.Add("statements", childTemplate.Render());
            }
            setNodeTemplate(context, selfTemplate);
        }

        public override void ExitElseBlock(NewDecafParser.ElseBlockContext context)
        {

            Template selfTemplate = stg.GetInstanceOf("BaseBlock");
            foreach (NewDecafParser.StatementContext decCtx in context.statement())
            {
                Template childTemplate = getNodeTemplate(decCtx);
                selfTemplate.Add("statements", childTemplate.Render());
            }
            setNodeTemplate(context, selfTemplate);
        }
        public override void EnterWhileBlock(NewDecafParser.WhileBlockContext context)
        {
            Template selfTemplate = stg.GetInstanceOf("BaseBlock");
            foreach (NewDecafParser.StatementContext decCtx in context.statement())
            {
                Template childTemplate = getNodeTemplate(decCtx);
                selfTemplate.Add("statements", childTemplate.Render());
            }
            setNodeTemplate(context, selfTemplate);
        }

        public override void ExitStatementBlock(NewDecafParser.StatementBlockContext context)
        {
            Template selfTemplate = stg.GetInstanceOf("BaseBlock");
            foreach (NewDecafParser.StatementContext decCtx in context.statement())
            {
                Template childTemplate = getNodeTemplate(decCtx);
                selfTemplate.Add("statements", childTemplate.Render());
            }
            setNodeTemplate(context, selfTemplate);
        }

        public override void EnterIf_statement(NewDecafParser.If_statementContext context)
        {
            Template selfTemplate = stg.GetInstanceOf("IfCmd");
            setNodeTemplate(context, selfTemplate);
        }

        public override void ExitIf_else_statement(NewDecafParser.If_else_statementContext context)
        {
            Template selfTemplate = getNodeTemplate(context);
            selfTemplate.Add("condition", getNodeTemplate(context.expression()).Render());
            selfTemplate.Add("LZero", LabelNameGenerator.getLabelName());
            selfTemplate.Add("LOne", LabelNameGenerator.getLabelName());
            selfTemplate.Add("ifcode", getNodeTemplate(context.ifBlock()).Render());
            selfTemplate.Add("elseCode", getNodeTemplate(context.elseBlock()).Render());
        }

        public override void ExitIf_statement(NewDecafParser.If_statementContext context)
        {
            Template selfTemplate = getNodeTemplate(context);
            selfTemplate.Add("condition", getNodeTemplate(context.expression()).Render());
            selfTemplate.Add("LZero", LabelNameGenerator.getLabelName());
            selfTemplate.Add("ifcode", getNodeTemplate(context.ifBlock()).Render());
        }

        public override void EnterWhile_statement(NewDecafParser.While_statementContext context)
        {
            Template selfTemplate = stg.GetInstanceOf("While");
            setNodeTemplate(context, selfTemplate);
        }

        public override void ExitWhile_statement(NewDecafParser.While_statementContext context)
        {
            Template selfTemplate = getNodeTemplate(context);
            selfTemplate.Add("condition", getNodeTemplate(context.expression()).Render());
            selfTemplate.Add("LZero", LabelNameGenerator.getLabelName());
            selfTemplate.Add("LOne", LabelNameGenerator.getLabelName());
            selfTemplate.Add("statements", getNodeTemplate(context.whileBlock()).Render());
        }

        public override void EnterReturn_statement(NewDecafParser.Return_statementContext context)
        {
            Template selfTemplate = stg.GetInstanceOf("Return");
            setNodeTemplate(context, selfTemplate);
        }

        public override void ExitReturn_statement(NewDecafParser.Return_statementContext context)
        {
            Template selfTemplate = getNodeTemplate(context);
            if (!context.expression().IsEmpty())
                selfTemplate.Add("expression", getNodeTemplate(context.expression()).Render());
        }

        public override void EnterMethod_statement(NewDecafParser.Method_statementContext context)
        {
            Template selfTemplate = stg.GetInstanceOf("MethodCall");
            setNodeTemplate(context, selfTemplate);
        }

        public override void ExitMethod_statement(NewDecafParser.Method_statementContext context)
        {
            Template selfTemplate = getNodeTemplate(context);
            foreach (var item in context.methodCall().arg())
            {
                selfTemplate.Add("expression", item.expression().GetText());
                selfTemplate.Add("parameterType", "int32");
            }

            IScope currentContext = getNodeScope(context);
            MethodSymbol currentSymbol = currentContext.resolve(context.methodCall().Id().GetText()) as MethodSymbol;


            selfTemplate.Add("methodType", TypeMapper.getMappedType(currentSymbol.type.getName()));
            selfTemplate.Add("methodName", context.methodCall().Id().GetText());


            setNodeTemplate(context, selfTemplate);
        }

        public override void ExitInt_literal_expression(NewDecafParser.Int_literal_expressionContext context)
        {
            Template selfTemplate = stg.GetInstanceOf("LoadIntConstant");
            selfTemplate.Add("constant", context.Num().GetText());
            setNodeTemplate(context, selfTemplate);
        }

        public override void EnterAssign_statement(NewDecafParser.Assign_statementContext context)
        {
            Template selfTemplate = stg.GetInstanceOf("Assignment");
            setNodeTemplate(context, selfTemplate);
        }

        public override void ExitAssign_statement(NewDecafParser.Assign_statementContext context)
        {
            Template selfTemplate = getNodeTemplate(context);
            selfTemplate.Add("expression", getNodeTemplate(context.expression()).Render());
            selfTemplate.Add("local", context.Id());
        }

        public override void EnterSingle_location(NewDecafParser.Single_locationContext context)
        {
            Template selfTemplate = stg.GetInstanceOf("LoadParameter");
            setNodeTemplate(context, selfTemplate);
        }

        public override void ExitSingle_location(NewDecafParser.Single_locationContext context)
        {
            Template selfTemplate = getNodeTemplate(context);
            selfTemplate.Add("parameter", context.Id());
        }


        public override void ExitAdd(NewDecafParser.AddContext context)
        {
            Template selfTemplate = stg.GetInstanceOf("Operation");
            selfTemplate.Add("op", "add");
            setNodeTemplate(context, selfTemplate);
        }

        public override void ExitDiv(NewDecafParser.DivContext context)
        {
            Template selfTemplate = stg.GetInstanceOf("Operation");
            selfTemplate.Add("op", "div");
            setNodeTemplate(context, selfTemplate);
        }

        public override void ExitMul(NewDecafParser.MulContext context)
        {
            Template selfTemplate = stg.GetInstanceOf("Operation");
            selfTemplate.Add("op", "mul");
            setNodeTemplate(context, selfTemplate);
        }


        public override void ExitRem(NewDecafParser.RemContext context)
        {
            Template selfTemplate = stg.GetInstanceOf("Operation");
            selfTemplate.Add("op", "rem");
            setNodeTemplate(context, selfTemplate);
        }

        public override void ExitSub(NewDecafParser.SubContext context)
        {

            Template selfTemplate = stg.GetInstanceOf("Operation");
            selfTemplate.Add("op", "sub");
            setNodeTemplate(context, selfTemplate);
        }

        public override void ExitArith_expression(NewDecafParser.Arith_expressionContext context)
        {
            Template selfTemplate = stg.GetInstanceOf("BinaryOp");
            selfTemplate.Add("left", getNodeTemplate(context.left).Render());
            selfTemplate.Add("right", getNodeTemplate(context.right).Render());
            selfTemplate.Add("op", getNodeTemplate(context.arith_op()).Render());
            setNodeTemplate(context, selfTemplate);
        }

        public override void ExitEqual(NewDecafParser.EqualContext context)
        {
            Template selfTemplate = stg.GetInstanceOf("Operation");
            selfTemplate.Add("op", "ceq");
            setNodeTemplate(context, selfTemplate);
        }

        public override void ExitNotEqual(NewDecafParser.NotEqualContext context)
        {
            Template selfTemplate = stg.GetInstanceOf("Operation");
            selfTemplate.Add("op", "ceq");
            selfTemplate.Add("requireNot", true);
            setNodeTemplate(context, selfTemplate);
        }

        public override void ExitEq_op_expression(NewDecafParser.Eq_op_expressionContext context)
        {
            Template selfTemplate = stg.GetInstanceOf("BinaryOp");
            selfTemplate.Add("left", getNodeTemplate(context.left).Render());
            selfTemplate.Add("right", getNodeTemplate(context.right).Render());
            selfTemplate.Add("op", getNodeTemplate(context.eq_op()).Render());
            setNodeTemplate(context, selfTemplate);
        }

        public override void VisitErrorNode(IErrorNode node)
        {

        }

    }
}
