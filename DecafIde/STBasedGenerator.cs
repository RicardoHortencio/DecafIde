
using Antlr4.Runtime.Tree;
using Antlr4.StringTemplate;
using DecafIde.LipBased;
namespace DecafIde
{
    class STBasedGenerator : NewDecafBaseListener
    {
        static TemplateGroup stg = new TemplateGroupFile(@"C:\Users\Paulo\SkyDrive\Visual Studio 2013\Projects\DecafIde\DecafIde\TAC.stg");
        public Template mainTemplate = stg.GetInstanceOf("Main");

        ParseTreeProperty<Template> nodeTemplate = new ParseTreeProperty<Template>();
        ParseTreeProperty<IScope> scopes = new ParseTreeProperty<IScope>();

        public void setNodeTemplate(IParseTree node, Template value) { nodeTemplate.Put(node, value); }
        public Template getNodeTemplate(IParseTree node) { return nodeTemplate.Get(node); }

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
        }

        public override void EnterArray_varDeclaration(NewDecafParser.Array_varDeclarationContext context)
        {
            Template selfTemplate = stg.GetInstanceOf("Field");

            selfTemplate.Add("isArray", true);
            selfTemplate.Add("id", context.Id().GetText());

            setNodeTemplate(context, selfTemplate);
        }


        public override void VisitErrorNode(IErrorNode node)
        {
            base.VisitErrorNode(node);
        }

    }
}
