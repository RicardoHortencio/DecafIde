/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

grammar short;
program 
	:   'class' Id '{' (declaration)* '}'
	;

declaration 
	:   structDeclaration
	|   varDeclaration
	|   methodDeclaration
	;
	 
varDeclaration 
	:   varType Id ';'												#single_varDeclaration
	|   varType Id '[' Num ']' ';'									#array_varDeclaration
	;

structDeclaration
	:   'struct' Id '{' (varDeclaration)* '}'
	;

varType
	:   'int'														#int_varType
	|   'char'														#char_varType
	|   'boolean'													#boolean_varType
	|   'struct' Id													#structImpl_varType
	|   structDeclaration											#structDecl_varType
	|   'void'														#void_varType
	;

methodDeclaration
	:   'int' Id '(' (parameter (',' parameter)*)? ')' block		#int_methodDeclaration
	|   'char' Id '(' (parameter (',' parameter)*)? ')' block		#char_methodDeclaration
	|   'boolean' Id '(' (parameter (',' parameter)*)? ')' block	#boolean_methodDeclaration
	|   'void' Id '(' (parameter (',' parameter)*)? ')' block		#void_methodDeclaration
	;

parameter
	:   parameterType Id		 #single_parameterDeclaration
	;

parameterType
	:   'int'					#int_parameterType
	|   'char'					#char_parameterType
	|   'boolean'				#boolean_parameterType
	;

block
	:   '{' (varDeclaration)* (statement)* '}' 
	;
	//**************************************** FALTA *******************/
statement
	:   'if' '(' expression ')' block ('else' block)?				#if_statement // ya
	|   'while' '(' expression ')' block							#while_statement //ya
	|   'return' (expression)? ';'									#return_statement  //ya
	|   methodCall ';'												#method_statement  // no se implementa
	|   block														#block_statement // no se implementa
	|   location '=' expression ';'									#assign_statement // no se implementa
	|   location '=' '(char)' expression ';'						#char_assign_statement //ya
	|   (expression)? ';'											#expression_statement //ya
	;

location  
	: Id ('.' location)?											#single_location
	|   Id '[' expression ']' ('.' location)?						#array_location
	;
	// ******************** FALTA ************************/

expression  //ya
	:   location													#var_location_expression
	|   methodCall													#methodCall_expression
	|	Num															#int_literal_expression
	|   CharacterLiteral											#char_literal_expression
	|   bool_literal												#bool_literal_expression
	|   expression arith_op expression								#arith_expression
	|   expression rel_op expression								#rel_op_expression
	|   expression eq_op expression									#eq_op_expression
	|   expression cond_op expression								#cond_op_expression
	|   '-' expression												#negative_expression
	|   '!' expression												#not_expression
	|   '(' expression ')'											#parens_expression
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