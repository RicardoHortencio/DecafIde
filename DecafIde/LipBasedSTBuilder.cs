using Antlr4.Runtime.Tree;
using DecafIde.LipBased;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecafIde
{
    class LipBasedSTBuilder : DecafBaseListener
    {
        #region annotatedTreeProperties
        ParseTreeProperty<IScope> nodeScopes = new ParseTreeProperty<IScope>();
        ParseTreeProperty<Symbol> nodeSymbols = new ParseTreeProperty<Symbol>();

        GlobalScope globals;
        IScope currentScope;

        public void setNodeScope(IParseTree node, IScope value)
        {
            nodeScopes.Put(node, value);
        }

        public IScope getNodeScope(IParseTree node)
        {
            return nodeScopes.Get(node);
        }

        public void setNodeSymbol(IParseTree node, Symbol value)
        {
            nodeSymbols.Put(node, value);
        }

        public Symbol getNodeSymbol(IParseTree node)
        {
            return nodeSymbols.Get(node);
        }
        #endregion

        #region MainProgram rules
        public override void EnterProgram(DecafParser.ProgramContext context)
        {
            globals = new GlobalScope();
            currentScope = globals;
            setNodeScope(context, currentScope);
        }
        #endregion

        #region Declaration rules
        public override void EnterStructDeclaration(DecafParser.StructDeclarationContext context)
        {
            StructSymbol ss = new StructSymbol(context.Id().GetText(), currentScope);
            currentScope.define(ss);
            setNodeScope(context, ss);
            setNodeSymbol(context, ss);
            currentScope = ss;
        }

        public override void ExitStructDeclaration(DecafParser.StructDeclarationContext context)
        {
            currentScope = currentScope.getEnclosingScope();
        }
        #endregion
    }
}
