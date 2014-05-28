grammar NewDecaf;

/*
 * Parser Rules
 */

program
	:	'class' Id '{' (structDeclaration
	|	varDeclaration
	|	methodDeclaration
	|	mainMethodDeclaration)* '}'									//ya
	;

varDeclaration
	:	varType Id ';'														#single_varDeclaration //ya
	|	varType Id '[' Num ']' ';'											#array_varDeclaration //ya
	;

structVarDeclaration
	:	varType Id ';'														#single_structVarDeclaration //pending
	|	varType Id '[' Num ']' ';'											#array_structVarDeclaration //pending
	;

structDeclaration
	:	'struct' Id '{' (structVarDeclaration)* '}'							//pending
	;

blockVarDeclaration
	:	varType Id ';'														#single_blockVarDeclaration //pending
	|	varType Id '[' Num ']' ';'											#array_blockVarDeclaration //pending
	;

varType
	:	'int'																#int_varType
	|	'char'																#char_varType
	|	'boolean'															#boolean_varType
	|	'struct' Id															#structImpl_varType
	|	structDeclaration													#structDecl_varType
	|	'void'																#void_varType
	;

mainMethodDeclaration
	:	mainMethodSignature block											//no implementado
	;

mainMethodSignature
	:	methodType 'main' '(' (parameter (',' parameter)*)? ')'				//pending
	;

methodDeclaration
	:	methodSignature block												//no implementado
	;

methodSignature
	: methodType Id '(' (parameter (',' parameter)*)? ')'					//pending
	;

methodType
	:	'int'
	|	'char'
	|	'boolean'
	|	'void'
	;

parameter
	:	parameterType Id		 #single_parameterDeclaration				//pending
	;

parameterType
	:	'int'
	|	'char'
	|	'boolean'
	;

block
	:	'{' (blockVarDeclaration)* (statement)* '}'							//pending
	;

ifBlock
	:	'{' (blockVarDeclaration)* (statement)* '}'
	;

elseBlock
	:	'{' (blockVarDeclaration)* (statement)* '}'
	;

whileBlock
	:	'{' (blockVarDeclaration)* (statement)* '}'
	;

statementBlock
	:	'{' (blockVarDeclaration)* (statement)* '}'
	;

	//**************************************** FALTA *******************/
statement
	:	'if' '(' expression ')' ifBlock 'else' elseBlock					#if_else_statement
	|	'if' '(' expression ')' ifBlock										#if_statement
	|	'while' '(' expression ')' whileBlock								#while_statement
	|	'return' (expression)? ';'											#return_statement
	|	methodCall ';'														#method_statement
	|	statementBlock														#block_statement
	|	location '=' expression ';'											#assign_statement
	|	location '=' '(char)' expression ';'								#char_assign_statement
	|	(expression)? ';'													#expression_statement
	;

locationList
	:	location ('.' location)*
	;

location
	:	Id (locationList)? 													#single_location
	|	Id '[' expression ']' (locationList)?								#array_location
	;
	// ******************** FALTA ************************/

expression  //pending
	:	location															#var_location_expression
	|	methodCall															#methodCall_expression
	|	Num																	#int_literal_expression
	|	CharacterLiteral													#char_literal_expression
	|	bool_literal														#bool_literal_expression
	|	expression arith_op expression										#arith_expression
	|	expression rel_op expression										#rel_op_expression
	|	expression eq_op expression											#eq_op_expression
	|	expression cond_op expression										#cond_op_expression
	|	'-' expression														#negative_expression
	|	'!' expression														#not_expression
	|	'(' expression ')'													#parens_expression
	;

methodCall
	:	Id '(' (arg (',' arg)*)? ')'
	;

arg
	: expression
	;

arith_op
	:	'+'
	|	'-'
	|	'*'
	|	'/'
	|	'%'
	;

rel_op
	:	'<'
	|	'>'
	|	'<='
	|	'>='
	;

eq_op
	:	'=='
	|	'!='
	;

cond_op
	:	'&&'
	|	'||'
	;

bool_literal
	:	'true'
	|	'false'
	;

Id
	:	Letter (Letter|Digit)*
	;

Num
	:	Digit+
	;

CharacterLiteral
	:	'\'' SingleCharacter '\''
	;

fragment
SingleCharacter
	:	~['\\]
	;

Digit
	:	[0-9]
	;

Letter
	:	[a-zA-Z]
	;

WS
	:	[  \t\r\n\u000C]+ -> skip ;

COMMENT
	:	'/*' .*? '*/' -> skip
	;

LINE_COMMENT
	:	'//' ~[\r\n]* -> skip
	;