using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DecafIde.Semantic_Analysis;
using DecafIde.Semantic_Analysis.ExtendedSymbols;
using DecafIde.HelperClasses;
using Antlr4.Runtime;

namespace DecafIde
{
    public class SymbolTableConstructor : DecafBaseListener
    {
        ScopeManager theScopeManager = new ScopeManager();

        List<SymbolType> parameterList = new List<SymbolType>();

        public int beginningLine;
        public int endingLine;

        #region Annotated properties
        ParseTreeProperty<SymbolType> nodeTypes = new ParseTreeProperty<SymbolType>();
        ParseTreeProperty<Symbol> nodeSymbols = new ParseTreeProperty<Symbol>();
        ParseTreeProperty<KeyValuePair<String, Symbol>> nodeSymbolTableEntries = new ParseTreeProperty<KeyValuePair<String, Symbol>>();
        ParseTreeProperty<int> nodeValue = new ParseTreeProperty<int>();

        public void setNodeValue(IParseTree node, int value) { nodeValue.Put(node, value); }
        public int getNodeValue(IParseTree node) { return nodeValue.Get(node); }


        public void setNodeType(IParseTree node, SymbolType value) { nodeTypes.Put(node, value); }
        public SymbolType getNodeType(IParseTree node) { return nodeTypes.Get(node); }

        public void setNodeSymbolTableEntry(IParseTree node, KeyValuePair<String, Symbol> value) { nodeSymbolTableEntries.Put(node, value); }
        public KeyValuePair<String, Symbol> getNodeSymbolTableEntry(IParseTree node) { return nodeSymbolTableEntries.Get(node); }

        public void setNodeSymbol(IParseTree node, Symbol value) { nodeSymbols.Put(node, value); }
        public Symbol getNodeSymbol(IParseTree node) { return nodeSymbols.Get(node); }
        #endregion

        #region MainProgram rules
        public override void EnterProgram(DecafParser.ProgramContext context)
        {
            theScopeManager.EnterScope();
        }

        public override void ExitProgram(DecafParser.ProgramContext context)
        {
            theScopeManager.ExitScope();
        }
        #endregion

        #region Declaration rules
        public override void EnterStructDeclaration(DecafParser.StructDeclarationContext context)
        {
            theScopeManager.EnterScope();
        }

        public override void ExitStructDeclaration(DecafParser.StructDeclarationContext context)
        {
            SymbolTable theScope = theScopeManager.ExitScope();
            StructDeclSymbol theSymbolRepresentation = new StructDeclSymbol(theScope);

            theScopeManager.AddSymbol(context.Id().GetText(), theSymbolRepresentation);
        }

        public override void ExitSingle_varDeclaration(DecafParser.Single_varDeclarationContext context)
        {
            SymbolType theType = getNodeType(context.varType());
            Symbol theSymbolRepresentation;

            if (theType != SymbolType.TstructImpl)
                theSymbolRepresentation = new Symbol(theType);
            else
                theSymbolRepresentation = getNodeSymbol(context.varType());

            theScopeManager.AddSymbol(context.Id().GetText(), theSymbolRepresentation);
        }

        public override void ExitArray_varDeclaration(DecafParser.Array_varDeclarationContext context)
        {
            SymbolType theType = getNodeType(context.varType());
            string num = context.Num().GetText();
            ArraySymbol theSymbolRepresentation;

            if (theType != SymbolType.TstructImpl)
                theSymbolRepresentation = new ArraySymbol(theType, Convert.ToInt32(num));
            else
            {
                StructImpl theSymbol = getNodeSymbol(context.varType()) as StructImpl;
                theSymbolRepresentation = new StructArraySymbol(theType, Convert.ToInt32(num), theSymbol.ParentStructName);
            }
            theScopeManager.AddSymbol(context.Id().GetText(), theSymbolRepresentation);
        }

        public override void EnterInt_methodDeclaration(DecafParser.Int_methodDeclarationContext context)
        {
            parameterList = new List<SymbolType>();
            theScopeManager.AddSymbol(context.Id().GetText(), new MethodSymbol(SymbolType.Tint));
            theScopeManager.EnterScope();
        }

        public override void EnterChar_methodDeclaration(DecafParser.Char_methodDeclarationContext context)
        {
            parameterList = new List<SymbolType>();
            theScopeManager.AddSymbol(context.Id().GetText(), new MethodSymbol(SymbolType.Tchar));
            theScopeManager.EnterScope();
        }

        public override void EnterBoolean_methodDeclaration(DecafParser.Boolean_methodDeclarationContext context)
        {
            parameterList = new List<SymbolType>();
            theScopeManager.AddSymbol(context.Id().GetText(), new MethodSymbol(SymbolType.Tboolean));
            theScopeManager.EnterScope();
        }

        public override void EnterVoid_methodDeclaration(DecafParser.Void_methodDeclarationContext context)
        {

            parameterList = null;
            parameterList = new List<SymbolType>();
            theScopeManager.AddSymbol(context.Id().GetText(), new MethodSymbol(SymbolType.Tvoid));
            theScopeManager.EnterScope();

        }

        public override void ExitInt_methodDeclaration(DecafParser.Int_methodDeclarationContext context)
        {

            parameterList = null;
            SymbolType theReturnType = SymbolType.Tint;
            List<SymbolType> theParameters = new List<SymbolType>();

            theScopeManager.GetAndExitScope();
            DecafParser.ParameterContext[] parameterGroup = context.parameter();

            foreach (DecafParser.ParameterContext item in parameterGroup)
                theParameters.Add(getNodeType(item));

            MethodSymbol theSymbolRepresentation = new MethodSymbol(theReturnType, theParameters);

            theScopeManager.AddSymbol(context.Id().GetText(), theSymbolRepresentation);

            if (theScopeManager.HasReturn)
            {
                if (!(TypeHelper.isEquivalentType(theScopeManager.ReturnType, SymbolType.Tint)))
                {
                    throw new Exception("Return Type Mismatch");
                }
            }
            else
                throw new Exception("Return Type Mismatch");

            theScopeManager.resetReturn();
        }

        public override void ExitChar_methodDeclaration(DecafParser.Char_methodDeclarationContext context)
        {

            parameterList = null;
            SymbolType theReturnType = SymbolType.Tchar;
            List<SymbolType> theParameters = new List<SymbolType>();

            theScopeManager.GetAndExitScope();
            DecafParser.ParameterContext[] parameterGroup = context.parameter();

            foreach (DecafParser.ParameterContext item in parameterGroup)
                theParameters.Add(getNodeType(item));

            MethodSymbol theSymbolRepresentation = new MethodSymbol(theReturnType, theParameters);

            theScopeManager.AddSymbol(context.Id().GetText(), theSymbolRepresentation);
            if (theScopeManager.HasReturn)
            {
                if (!(TypeHelper.isEquivalentType(theScopeManager.ReturnType, SymbolType.Tchar)))
                {
                    throw new Exception("Return Type Mismatch");
                }
            }
            else
                throw new Exception("Return Type Mismatch");

            theScopeManager.resetReturn();
        }

        public override void ExitBoolean_methodDeclaration(DecafParser.Boolean_methodDeclarationContext context)
        {

            parameterList = null;
            SymbolType theReturnType = SymbolType.Tboolean;
            List<SymbolType> theParameters = new List<SymbolType>();

            theScopeManager.GetAndExitScope();
            DecafParser.ParameterContext[] parameterGroup = context.parameter();

            foreach (DecafParser.ParameterContext item in parameterGroup)
                theParameters.Add(getNodeType(item));

            MethodSymbol theSymbolRepresentation = new MethodSymbol(theReturnType, theParameters);

            theScopeManager.AddSymbol(context.Id().GetText(), theSymbolRepresentation);

            if (theScopeManager.HasReturn)
            {
                if (!(TypeHelper.isEquivalentType(theScopeManager.ReturnType, SymbolType.Tboolean)))
                {
                    throw new Exception("Return Type Mismatch");
                }
            }
            else
                throw new Exception("Return Type Mismatch");

            theScopeManager.resetReturn();
        }

        public override void ExitVoid_methodDeclaration(DecafParser.Void_methodDeclarationContext context)
        {

            parameterList = null;
            SymbolType theReturnType = SymbolType.Tvoid;
            List<SymbolType> theParameters = new List<SymbolType>();

            theScopeManager.GetAndExitScope();
            DecafParser.ParameterContext[] parameterGroup = context.parameter();

            foreach (DecafParser.ParameterContext item in parameterGroup)
                theParameters.Add(getNodeType(item));

            MethodSymbol theSymbolRepresentation = new MethodSymbol(theReturnType, theParameters);

            theScopeManager.AddSymbol(context.Id().GetText(), theSymbolRepresentation);

            if (theScopeManager.HasReturn)
            {
                if (!(TypeHelper.isEquivalentType(theScopeManager.ReturnType, SymbolType.Tvoid)))
                {
                    throw new Exception("Return Type Mismatch");
                }
            }

            theScopeManager.resetReturn();
        }
        #endregion

        #region VarType rules
        public override void ExitInt_varType(DecafParser.Int_varTypeContext context)
        {
            setNodeType(context, SymbolType.Tint);
        }


        public override void ExitChar_varType(DecafParser.Char_varTypeContext context)
        {
            setNodeType(context, SymbolType.Tchar);
        }

        public override void ExitBoolean_varType(DecafParser.Boolean_varTypeContext context)
        {
            setNodeType(context, SymbolType.Tboolean);
        }

        public override void ExitStructImpl_varType(DecafParser.StructImpl_varTypeContext context)
        {
            setNodeType(context, SymbolType.TstructImpl);
            setNodeSymbol(context, new StructImpl(context.Id().GetText()));
        }

        public override void ExitStructDecl_varType(DecafParser.StructDecl_varTypeContext context)
        {
            setNodeType(context, SymbolType.TstructImpl);
            setNodeSymbol(context, new StructDeclSymbol(theScopeManager.ExitScope()));
        }

        public override void ExitVoid_varType(DecafParser.Void_varTypeContext context)
        {
            setNodeType(context, SymbolType.Tvoid);
        }
        #endregion

        #region ParameterDeclaration
        public override void ExitSingle_parameterDeclaration(DecafParser.Single_parameterDeclarationContext context)
        {
            SymbolType theParameterType = getNodeType(context.parameterType());
            Symbol theSymbolRepresentation = new Symbol(theParameterType);
            KeyValuePair<string, Symbol> theKeyValuePairRepresentaion = new KeyValuePair<string, Symbol>(context.Id().GetText(), theSymbolRepresentation);
            theScopeManager.AddSymbol(theKeyValuePairRepresentaion);
            parameterList.Add(theParameterType);
            setNodeSymbolTableEntry(context, theKeyValuePairRepresentaion);
            setNodeType(context, theParameterType);
        }
        #endregion

        #region ParameterType Rules
        public override void ExitInt_parameterType(DecafParser.Int_parameterTypeContext context)
        {
            setNodeType(context, SymbolType.Tint);
        }

        public override void ExitChar_parameterType(DecafParser.Char_parameterTypeContext context)
        {
            setNodeType(context, SymbolType.Tchar);
        }

        public override void ExitBoolean_parameterType(DecafParser.Boolean_parameterTypeContext context)
        {
            setNodeType(context, SymbolType.Tboolean);
        }
        #endregion

        #region Block
        public override void EnterBlock(DecafParser.BlockContext context)
        {
            theScopeManager.EnterScope();
        }

        public override void ExitBlock(DecafParser.BlockContext context)
        {
            theScopeManager.ExitScope();
        }
        #endregion

        #region statements
        public override void ExitIf_statement(DecafParser.If_statementContext context)
        {
            if (!TypeHelper.isBoolean(getNodeType(context.expression())))
                throw new Exception("Boolean expression expected " + getNodeType(context.expression()).ToString() + " found instead.");

        }

        public override void ExitWhile_statement(DecafParser.While_statementContext context)
        {
            if (!TypeHelper.isBoolean(getNodeType(context.expression())))
                throw new Exception("Boolean expression expected " + getNodeType(context.expression()).ToString() + " found instead.");
        }

        public override void ExitReturn_statement(DecafParser.Return_statementContext context)
        {
            if (context.expression() != null)
            {
                setNodeType(context, getNodeType(context.expression()));
                if (!theScopeManager.HasReturn)
                    theScopeManager.ReturnType = getNodeType(context.expression());
                else
                    if (!TypeHelper.isEquivalentType(theScopeManager.ReturnType, getNodeType(context.expression())))
                        throw new Exception("Result type mismatch");
            }
        }

        public override void ExitAssign_statement(DecafParser.Assign_statementContext context)
        {
            SymbolType locationType = getNodeType(context.location());
            SymbolType expressionType = getNodeType(context.expression());

            if (TypeHelper.isEquivalentType(locationType, expressionType))
                setNodeType(context, TypeHelper.getGreatestType(locationType, expressionType));
            else
                throw new Exception("Incompatible types, found location with type "
                    + locationType.ToString()
                    + " and expression with type "
                    + expressionType.ToString());
        }

        public override void ExitChar_assign_statement(DecafParser.Char_assign_statementContext context)
        {
            SymbolType locationType = getNodeType(context.location());
            SymbolType expressionType = getNodeType(context.expression());

            if (TypeHelper.isNumeric(expressionType))
                if (TypeHelper.isNumeric(locationType))
                    setNodeType(context, SymbolType.Tchar);
                else
                    throw new Exception("Incompatible types, found location with type "
                    + locationType.ToString()
                    + " and expression with type "
                    + expressionType.ToString());
        }

        public override void ExitExpression_statement(DecafParser.Expression_statementContext context)
        {
            if (context.expression() != null)
            {
                setNodeType(context, getNodeType(context.expression()));
            }
            else
                setNodeType(context, SymbolType.Tvoid);
        }

        #endregion

        #region location
        public override void EnterSingle_location(DecafParser.Single_locationContext context)
        {
            Symbol tempSymbol = theScopeManager.FindSymbol(context.Id().GetText(), symbolCategory.Cvariable);
            if (context.location() != null)
            {
                StructImpl typedStruct = tempSymbol as StructImpl;
                string structDecName = typedStruct.ParentStructName;
                StructDeclSymbol typedParent = theScopeManager.FindSymbol(structDecName, symbolCategory.CstructDecl) as StructDeclSymbol;
                theScopeManager.PushSymbolTable(typedParent.Members);
            }
        }
        public override void ExitSingle_location(DecafParser.Single_locationContext context)
        {
            Symbol tempSymbol = theScopeManager.currentScopeFindSymbol(context.Id().GetText(), symbolCategory.Cvariable);
            if (context.location() != null)
            {
                setNodeType(context, getNodeType(context.location()));
                theScopeManager.GetAndExitScope();
            }
            else
            {
                setNodeType(context, tempSymbol.Type);
            }
        }

        public override void EnterArray_location(DecafParser.Array_locationContext context)
        {
            Symbol tempSymbol = theScopeManager.FindSymbol(context.Id().GetText(), symbolCategory.Cvariable);
            if (context.location() != null)
            {
                StructArraySymbol typedSymbol = tempSymbol as StructArraySymbol;
                string structDecName = typedSymbol.ParentStructName;
                StructDeclSymbol typedParent = theScopeManager.FindSymbol(structDecName, symbolCategory.CstructDecl) as StructDeclSymbol;
                theScopeManager.PushSymbolTable(typedParent.Members);
            }
        }

        public override void ExitArray_location(DecafParser.Array_locationContext context)
        {
            Symbol tempSymbol = theScopeManager.FindSymbol(context.Id().GetText(), symbolCategory.Cvariable);
            if (context.location() != null)
            {
                StructArraySymbol typedSymbol = tempSymbol as StructArraySymbol;
                string structDecName = typedSymbol.ParentStructName;
                StructDeclSymbol typedParent = theScopeManager.FindSymbol(structDecName, symbolCategory.CstructDecl) as StructDeclSymbol;
                theScopeManager.PushSymbolTable(typedParent.Members);
                setNodeType(context, getNodeType(context.location()));
                if (getNodeValue(context.expression())>0)
                {
                    if (getNodeValue(context.expression()) > typedSymbol.length)
                    {
                        throw new Exception("Out of bounds exception");
                    }
                }
            }
            else
            {
                ArraySymbol typedSymbol = tempSymbol as ArraySymbol;
                setNodeType(context, typedSymbol.InternalType);
            }
        }

        #endregion

        #region expressionDeclarations

        #region var_location_expression
        public override void ExitVar_location_expression(DecafParser.Var_location_expressionContext context)
        {
            setNodeType(context, getNodeType(context.location()));
        }
        #endregion

        #region methodCall handling
        public override void ExitMethodCall_expression(DecafParser.MethodCall_expressionContext context)
        {
            setNodeType(context, getNodeType(context.methodCall()));
        }

        public override void ExitMethodCall(DecafParser.MethodCallContext context)
        {
            string theId = context.Id().GetText();

            if (!theScopeManager.CheckScope(context.Id().GetText(), symbolCategory.Cmethod)) //checks if the method is already declared
            {
                int line = context.start.Line;
                throw new Exception("Undeclared Identifier at line " + line.ToString());
            }
            else
            {
                MethodSymbol theSymbol = theScopeManager.FindSymbol(context.Id().GetText(), symbolCategory.Cmethod) as MethodSymbol;
                List<SymbolType> MyparameterList = new List<SymbolType>();
                #region methodCall parameter handling

                for (int i = 0; i < context.arg().Length; i++)
                {
                    DecafParser.ArgContext argument = context.arg(i);
                    MyparameterList.Add(getNodeType(argument));
                }

                if (!theSymbol.checkParameterList(MyparameterList))

                    if (!TypeHelper.checkLists(MyparameterList, parameterList))
                    {
                        throw new Exception("Argument Mismatch Errror");
                    }

                #endregion
                setNodeType(context, theSymbol.Type);
            }
        }
        #endregion

        #region literal_expressions
        public override void ExitInt_literal_expression(DecafParser.Int_literal_expressionContext context)
        {
            setNodeValue(context, Convert.ToInt32(context.Num().GetText()));
            setNodeType(context, SymbolType.Tint);
        }

        public override void ExitBool_literal_expression(DecafParser.Bool_literal_expressionContext context)
        {
            setNodeType(context, SymbolType.Tboolean);

        }

        public override void ExitChar_literal_expression(DecafParser.Char_literal_expressionContext context)
        {
            setNodeType(context, SymbolType.Tchar);
        }
        #endregion

        #region binary_op
        public override void ExitArith_expression(DecafParser.Arith_expressionContext context)
        {
            if (TypeHelper.isNumeric(getNodeType(context.expression(0)), getNodeType(context.expression(1))))
                setNodeType(context, SymbolType.Tint);
            else
                throw new Exception("Incompatible type. Numeric expected.");
        }

        public override void ExitRel_op_expression(DecafParser.Rel_op_expressionContext context)
        {
            if (TypeHelper.isNumeric(getNodeType(context.expression(0)), getNodeType(context.expression(1))))
                setNodeType(context, SymbolType.Tboolean);
            else
                throw new Exception("The expressions have incompatible types.");
        }

        public override void ExitEq_op_expression(DecafParser.Eq_op_expressionContext context)
        {
            if (TypeHelper.isEquivalentType(getNodeType(context.expression(0)), getNodeType(context.expression(1))))
                setNodeType(context, SymbolType.Tboolean);
            else
                throw new Exception("The expressions have incompatible types.");
        }

        public override void ExitCond_op_expression(DecafParser.Cond_op_expressionContext context)
        {
            if (TypeHelper.isBoolean(getNodeType(context.expression(0)), getNodeType(context.expression(1))))
                setNodeType(context, SymbolType.Tboolean);
            else
                throw new Exception("The expressions have incompatible types.");
        }
        #endregion

        #region unary_operators
        public override void ExitNegative_expression(DecafParser.Negative_expressionContext context)
        {
            if (TypeHelper.isNumeric(getNodeType(context.expression())))
                setNodeType(context, SymbolType.Tint);
            else
                throw new Exception("Incompatible type. Numeric expected. " + getNodeType(context.expression()).ToString() + " found instead.");
        }

        public override void ExitNot_expression(DecafParser.Not_expressionContext context)
        {
            if (TypeHelper.isBoolean(getNodeType(context.expression())))
                setNodeType(context, SymbolType.Tboolean);
            else
                throw new Exception("Incompatible type. Boolean expected. " + getNodeType(context.expression()).ToString() + " found instead.");
        }
        public override void ExitParens_expression(DecafParser.Parens_expressionContext context)
        {
            setNodeType(context, getNodeType(context.expression()));
        }
        #endregion

        #endregion

        #region arg
        public override void ExitArg(DecafParser.ArgContext context)
        {
            setNodeType(context, getNodeType(context.expression()));
        }
        #endregion

        #region Literal Rules
        public override void ExitBool_literal(DecafParser.Bool_literalContext context)
        {
            setNodeType(context, SymbolType.Tboolean);
        }

        #endregion


        public override void EnterEveryRule(ParserRuleContext context)
        {
            beginningLine = context.start.Line;
            endingLine = context.stop.Line;
        }

    }
}
