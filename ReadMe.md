# Maths interpreter langueage definnitions

```
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
```