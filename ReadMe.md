# Maths interpreter language
 
This is a simple interpreted programming language for completing mathematical calculations

## Current capabilities
*	Addition and subtraction of factors
*	Muliplication and division of factors
*	Exponentiation of factors
*	Basic trigonometric functions
*	Variable assignment operation
*	File input and output
*	REPL mode

## To be implemented
* Function definitions
* Array definitons
* Functional programming funtions (E.G. map, fold, filter, etc...)

## far in the future
* Set definitions

## language Definitions

```
<Document> ::= \n |
	<Expression> |
	<Expression> \n <Document> |
	\n <Document>


<Expression> ::= <Term> |
	<Expression> '+' <Expression> |
	<Expression> '-' <Expression> |

<Term> ::= <Indecie> |
	<Term> '*' <Term> |
	<Term> '/' <Term> |
	<Term> '%' <Term> |

<Indecie> ::= <Factor> |
	<Indecie> '^' <Indecie>

<Factor> ::= <Number> |
	'(' <Expression> ')' |
	<Function>

<Function> ::= <MathsOperator> '(' <Arguments> ')'
<Arguments> ::= <Number> |
	<Number> ',' <Arguments>

<MathsOperator> ::= SINE | COSINE | TANGENT

// Variable Definitions
<Letter> ::= a b c d e f g h i j k l m n o p q r s t u v w x y z

<VariableDefinition> ::= <Letter> = <Indecie>

<ParameterDefinition> ::= <Letter> |
	<Letter> ' ' <ParameterDefinition>

<FuncitonDefinition> ::= <letter> '(' <letter> ')' |


```
