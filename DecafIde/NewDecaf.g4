grammar NewDecaf;

/*
 * Parser Rules
 */

program
	:	'class' Id '{' (structDeclaration
	|	varDeclaration
	|	methodDeclaration
	|	mainMethodDeclaration)* '}'								//ya
	;

varDeclaration
	:	varType Id ';'											#single_varDeclaration //ya
	|	varType Id '[' Num ']' ';'								#array_varDeclaration //ya
	;

structVarDeclaration
	:	varType Id ';'											#single_structVarDeclaration //ya
	|	varType Id '[' Num ']' ';'								#array_structVarDeclaration //ya
	;

structDeclaration
	:	'struct' Id '{' (structVarDeclaration)* '}'				//ya
	;

blockVarDeclaration
	:	varType Id ';'											#single_blockVarDeclaration //ya
	|	varType Id '[' Num ']' ';'								#array_blockVarDeclaration //ya
	;

varType
	:	'int'													#int_varType
	|	'char'													#char_varType
	|	'boolean'												#boolean_varType
	|	'struct' Id												#structImpl_varType
	|	structDeclaration										#structDecl_varType
	|	'void'													#void_varType
	;

mainMethodDeclaration
	:	mainMethodSignature block								//no implementado
	;

mainMethodSignature
	:	methodType 'main' '(' (parameter (',' parameter)*)? ')'	//pending
	;

methodDeclaration
	:	methodSignature block									//no implementado
	;

methodSignature
	: methodType Id '(' (parameter (',' parameter)*)? ')'		//pending
	;

methodType
	:	'int'
	|	'char'
	|	'boolean'
	|	'void'
	;

parameter
	:	parameterType Id		 #single_parameterDeclaration	//pending
	;

parameterType
	:	'int'
	|	'char'
	|	'boolean'
	;

	// recordar las variables con arrayinit
block
	:	'{' (blockVarDeclaration)* (statement)* '}'				//pending
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

	//**************************************** FALTA *************
statement
	:	'if' '(' expression ')' ifBlock 'else' elseBlock		#if_else_statement
	|	'if' '(' expression ')' ifBlock							#if_statement
	|	'while' '(' expression ')' whileBlock					#while_statement
	|	'return' (expression)? ';'								#return_statement
	|	methodCall ';'											#method_statement
	|	statementBlock											#block_statement
	|	Id '=' expression ';'								#assign_statement
	|	Id '=' '(char)' expression ';'					#char_assign_statement
	|	(expression)? ';'										#expression_statement
	;

locationList
	:	location ('.' location)*
	;

location
	:	Id (locationList)? 										#single_location
	|	Id '[' expression ']' (locationList)?					#array_location
	;
	// ******************** FALTA ************************/

expression  //pending
	:	'(' expression ')'										#parens_expression
	|	Num														#int_literal_expression
	|	CharacterLiteral										#char_literal_expression
	|	bool_literal											#bool_literal_expression
	|	location												#var_location_expression
	|	methodCall												#methodCall_expression
	|	left=expression cond_op right=expression							#cond_op_expression
	|	left=expression arith_op right=expression							#arith_expression
	|	left=expression rel_op right=expression							#rel_op_expression
	|	left=expression eq_op right=expression								#eq_op_expression
	|	'-' expression											#negative_expression
	|	'!' expression											#not_expression
	;

methodCall
	:	Id '(' (arg (',' arg)*)? ')'
	;

arg
	: expression
	;

cond_op
	:	'&&'
	|	'||'
	;

arith_op
	:	'+'														#add
	|	'-'														#sub
	|	'*'														#mul
	|	'/'														#div
	|	'%'														#rem
	;

rel_op
	:	'<'
	|	'>'
	|	'<='
	|	'>='
	;

eq_op
	:	'=='													#equal
	|	'!='													#notEqual
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