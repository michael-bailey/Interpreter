using System;
using System.Collections.Generic;
using System.Data;

namespace Interpreter
{
	class Parser
	{
		List<Token> tokens;
		Int32 start;
		Int32 current;

		Token CurrentToken {
			get {
				try {
					return this.tokens[this.current];
				} catch (ArgumentOutOfRangeException) {
					return null;
				}
			}
		}

		private void Advance() => this.current++;

		private Boolean IsEnd() => this.tokens.Count <= current;

		public Parser(List<Token> tokens)
		{
			this.tokens = tokens;
			this.current = 0;
		}

		public DocumentNode Parse()
		{
			return this.Document();
		}

		private DocumentNode Document()
		{
			List<ASTNode> expressions = new();
			var start = this.start;

			ASTNode expression = this.Expression();
			expressions.Add(expression);

			while (this.CurrentToken?.type == TokenType.NEW_LINE) 
			{
				this.Advance();
				expression = this.Expression();
				expressions.Add(expression);
			}
			var end = this.start;
			return new DocumentNode(TokenType.Document, start, end, expressions);
		}

		//any number or sub expression
		private ASTNode Factor()
		{
			Token current = this.CurrentToken;
			if (current?.type == TokenType.OPEN_BRACKET) {
				this.Advance();
				ASTNode result = this.Expression();
				if (this.CurrentToken.type != TokenType.CLOSED_BRACKET) {
					throw new Exception("No Closing bracket");
				}
				this.Advance();
				return result;
			}

			if (current?.type == TokenType.NAME) {
				String FunctionName = current.Literal;
				this.start = current.start;

				this.Advance();
				if (this.CurrentToken?.type != TokenType.OPEN_BRACKET) { throw new Exception("[syntaxError]: Expected open bracket"); }

				List<ASTNode> parameters = new();

				this.Advance();
				while (this.CurrentToken?.type != TokenType.CLOSED_BRACKET) {
					ASTNode param = this.Expression();
					parameters.Add(param);
					if (this.CurrentToken?.type == TokenType.COMMA) this.Advance();
				}

				int end = (int)(this.CurrentToken?.end);

				this.Advance();
				return new FunctionNode(current, start, end, FunctionName, parameters);
			}

			if (current?.type == TokenType.NUMBER) {
				this.Advance();
				return new NumberNode(current, current.start, current.end, current.Literal);
			}
			throw new Exception("Syntax Error: token was not a number or sub expression");
		}

		// indecie
		private ASTNode Index()
		{
			return this.Operation(this.Factor, new List<TokenType> { TokenType.IDECIE });
		}

		// multiply or divide
		private ASTNode Term()
		{
			return this.Operation(this.Index, new List<TokenType> { TokenType.MULTIPLY, TokenType.DIVIDE, TokenType.MODULO });
		}

		// add or subtract
		private ASTNode Expression()
		{
			return this.Operation(this.Term, new List<TokenType> { TokenType.ADD, TokenType.SUBTRACT });
		}

		private ASTNode Operation(Func<ASTNode> func, List<TokenType> operators)
		{
			ASTNode left = func();
			while (this.CurrentToken != null) {
				if (operators.Contains(this.CurrentToken.type)) {
					Token operation = (Token)this.CurrentToken;
					this.Advance();
					ASTNode right = func();
					left = new OperatorNode(operation, operation.start, operation.end, left, right);
				} else {
					return left;
				}
			}
			return left;
		}
	}
}