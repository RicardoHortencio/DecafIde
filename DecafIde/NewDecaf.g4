grammar NewDecaf;

/*
 * Parser Rules
 */

program 
	:   'class' Id '{' (declaration)* '}'						//ya
	;

declaration 
	:   structDeclaration
	|   varDeclaration
	|   methodDeclaration
	|	mainMethodDeclaration
	;
	 
varDeclaration 
	:   varType Id ';'				#single_varDeclaration		
	|   varType Id '[' Num ']' ';'	#array_varDeclaration
	;

structVarDeclaration 
	:   varType Id ';'				#single_structVarDeclaration		
	|   varType Id '[' Num ']' ';'	#array_structVarDeclaration
	;


blockVarDeclaration 
	:   varType Id ';'				#single_blockVarDeclaration
	|   varType Id '[' Num ']' ';'	#array_blockVarDeclaration
	;

structDeclaration
	:   'struct' Id '{' (structVarDeclaration)* '}'					//ya
	;

varType
	:   'int'					#int_varType
	|   'char'					#char_varType
	|   'boolean'				#boolean_varType
	|   'struct' Id				#structImpl_varType
	|   structDeclaration       #structDecl_varType
	|   'void'					#void_varType
	;

mainMethodDeclaration
	:   methodType 'main' '(' (parameter (',' parameter)*)? ')'  block    //ya
	;

methodDeclaration
	:   methodSignature block
	;

methodSignature
	:   methodType Id '(' (parameter (',' parameter)*)? ')'     //ya
	;

methodType 														//ya
	:   'int'					
	|   'char'					
	|   'boolean'	
	|   'void'					
	;

parameter														
	:   parameterType Id		 #single_parameterDeclaration	//ya
	;

parameterType													//ya
	:   'int'		
	|   'char'		
	|   'boolean'	
	;

block
	:   '{' (blockVarDeclaration)* (statement)* '}' 
	;

ifBlock
	:   '{' (blockVarDeclaration)* (statement)* '}' 
	;

elseBlock
	:   '{' (blockVarDeclaration)* (statement)* '}' 
	;

whileBlock
	:   '{' (blockVarDeclaration)* (statement)* '}' 
	;

statementBlock
	:   '{' (blockVarDeclaration)* (statement)* '}' 
	;

	//**************************************** FALTA *******************/
statement
	:   'if' '(' expression ')' ifBlock 'else' elseBlock	#if_else_statement // ya
	|   'if' '(' expression ')' ifBlock                   #if_statement // ya
	|   'while' '(' expression ')' whileBlock		#while_statement //ya
	|   'return' (expression)? ';'			#return_statement  //ya
	|   methodCall ';'					#method_statement  // no se implementa
	|   statementBlock						#block_statement // no se implementa
	|   location '=' expression ';'			#assign_statement // no se implementa
	|   location '=' '(char)' expression ';'		#char_assign_statement //ya
	|   (expression)? ';'				#expression_statement //ya
	;

locationList
	:   location ('.' location)*
	;

location  
	:   Id (locationList)? 				#single_location
	|   Id '[' expression ']' (locationList)?           #array_location
	;
	// ******************** FALTA ************************/

expression  //ya
	:   location					#var_location_expression
	|   methodCall					#methodCall_expression
	|	Num                                             #int_literal_expression
	|   CharacterLiteral				#char_literal_expression
	|   bool_literal					#bool_literal_expression
	|   expression arith_op expression                  #arith_expression
	|   expression rel_op expression                    #rel_op_expression
	|   expression eq_op expression                     #eq_op_expression
	|   expression cond_op expression                   #cond_op_expression
	|   '-' expression					#negative_expression
	|   '!' expression					#not_expression
	|   '(' expression ')'				#parens_expression
	;

methodCall
	:	Id '(' (arg (',' arg)*)? ')' //ya
	;

arg
	:   expression //ya
	;
	
arith_op
	:   '+'
	|   '-'
	|   '*'
	|   '/'
	|   '%'
	;

rel_op
	:   '<'
	|   '>'
	|   '<='
	|   '>='
	;

eq_op
	:   '=='
	|   '!='
	;

cond_op
	:   '&&'
	|   '||'
	;
	
bool_literal //ya
	:   'true'
	|   'false'
	;

Id
	:   Letter (Letter|Digit)*
	;

Num
	:   Digit+
	;

CharacterLiteral
	:   '\'' SingleCharacter '\''
	;

fragment
SingleCharacter
	:   ~['\\]
	;

Digit 
	:   [0-9]
	;

Letter
	:   [a-zA-Z]
	;

WS  :   [  \t\r\n\u000C]+ -> skip ;
 
COMMENT
	:   '/*' .*? '*/' -> skip
	;

LINE_COMMENT
	:   '//' ~[\r\n]* -> skip
	;