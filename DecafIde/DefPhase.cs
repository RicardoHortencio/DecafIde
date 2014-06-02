using Antlr4.Runtime.Tree;
using DecafIde.LipBased;
using System;

namespace DecafIde
{
    class DefPhase : NewDecafBaseListener
    {
        public ParseTreeProperty<IScope> scopes = new ParseTreeProperty<IScope>();
        public void setNodeScope(IParseTree node, IScope value)
        {
            scopes.Put(node, value);
        }

        public IScope getNodeScope(IParseTree node)
        {
            return scopes.Get(node);
        }

        GlobalScope globals;
        IScope currentScope;

        public override void EnterProgram(NewDecafParser.ProgramContext context)
        {
            globals = new GlobalScope(null);
            currentScope = globals;
            setNodeScope(context, currentScope);
        }

        public override void ExitProgram(NewDecafParser.ProgramContext context)
        {
            base.ExitProgram(context);
        }

        public override void EnterStructDeclaration(NewDecafParser.StructDeclarationContext context)
        {
            StructSymbol ss = new StructSymbol(context.Id().GetText(), currentScope);
            currentScope.define(ss);
            currentScope = ss;
            setNodeScope(context, currentScope);
        }

        public override void ExitStructDeclaration(NewDecafParser.StructDeclarationContext context)
        {
            currentScope = currentScope.getEnclosingScope();
        }

        public override void EnterBlock(NewDecafParser.BlockContext context)
        {
            currentScope = new LocalScope(currentScope);
            setNodeScope(context, currentScope);
        }

        public override void ExitBlock(NewDecafParser.BlockContext context)
        {
            currentScope = currentScope.getEnclosingScope();
        }

        public override void EnterSingle_blockVarDeclaration(NewDecafParser.Single_blockVarDeclarationContext context)
        {
            IType varType = (IType)currentScope.resolve(context.varType().GetText());
            VariableSymbol vs = new VariableSymbol(context.Id().GetText(), varType, theVariableType: VariableType.vField);
            currentScope.define(vs);
            setNodeScope(context, currentScope);
        }

        public override void EnterArray_blockVarDeclaration(NewDecafParser.Array_blockVarDeclarationContext context)
        {
            IType varType = (IType)currentScope.resolve(context.varType().GetText());
            int arraySize = Convert.ToInt32(context.Num().GetText());
            VariableSymbol vs = new VariableSymbol(context.Id().GetText(), varType, theSize: arraySize, theVariableType: VariableType.vField);
            currentScope.define(vs);
            setNodeScope(context, currentScope);
        }

        public override void EnterMethodSignature(NewDecafParser.MethodSignatureContext context)
        {
            IType returnType = (IType)currentScope.resolve(context.methodType().GetText());
            MethodSymbol ms = new MethodSymbol(context.Id().GetText(), returnType, currentScope);
            currentScope.define(ms);
            currentScope = ms;
            setNodeScope(context, currentScope);
        }

        public override void ExitMethodSignature(NewDecafParser.MethodSignatureContext context)
        {
            //currentScope = currentScope.getEnclosingScope();
        }

        public override void EnterSingle_parameterDeclaration(NewDecafParser.Single_parameterDeclarationContext context)
        {
            IType varType = (IType)currentScope.resolve(context.parameterType().GetText());
            VariableSymbol vs = new VariableSymbol(context.Id().GetText(), varType, theVariableType: VariableType.vParameter);
            currentScope.define(vs);
            setNodeScope(context, currentScope);
        }

        public override void EnterSingle_varDeclaration(NewDecafParser.Single_varDeclarationContext context)
        {
            IType varType = (IType)currentScope.resolve(context.varType().GetText());
            VariableSymbol vs = new VariableSymbol(context.Id().GetText(), varType, theVariableType: VariableType.vField);
            currentScope.define(vs);
            setNodeScope(context, currentScope);
        }

        public override void EnterArray_varDeclaration(NewDecafParser.Array_varDeclarationContext context)
        {
            IType varType = (IType)currentScope.resolve(context.varType().GetText());
            int arraySize = Convert.ToInt32(context.Num().GetText());
            VariableSymbol vs = new VariableSymbol(context.Id().GetText(), varType, theSize: arraySize, theVariableType: VariableType.vField);
            currentScope.define(vs);
            setNodeScope(context, currentScope);
        }

        public override void EnterSingle_structVarDeclaration(NewDecafParser.Single_structVarDeclarationContext context)
        {
            IType varType = (IType)currentScope.resolve(context.varType().GetText());
            VariableSymbol vs = new VariableSymbol(context.Id().GetText(), varType, theVariableType: VariableType.vField);
            currentScope.define(vs);
            setNodeScope(context, currentScope);
        }

        public override void EnterArray_structVarDeclaration(NewDecafParser.Array_structVarDeclarationContext context)
        {
            IType varType = (IType)currentScope.resolve(context.varType().GetText());
            int arraySize = Convert.ToInt32(context.Num().GetText());
            VariableSymbol vs = new VariableSymbol(context.Id().GetText(), varType, theSize: arraySize, theVariableType: VariableType.vField);
            currentScope.define(vs);
            setNodeScope(context, currentScope);
        }

        public override void EnterMainMethodSignature(NewDecafParser.MainMethodSignatureContext context)
        {
            IType returnType = (IType)currentScope.resolve(context.methodType().GetText());
            MethodSymbol ms = new MethodSymbol("main", returnType, currentScope);
            currentScope.define(ms);
            currentScope = ms;
            setNodeScope(context, currentScope);
        }

        //void defineVar(SymbolType getType)
    }
}
