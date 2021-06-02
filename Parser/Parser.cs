using System;
using System.Collections.Generic;

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

		public ASTNode Parse()
		{
			return this.Expression();
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
			if (current?.type == TokenType.NUMBER) {
				this.Advance();
				return new NumberNode(current, current.Literal);
			}
			throw new Exception("token was not a number");
		}

		// indecie
		private ASTNode Index()
		{
			return this.Operation(this.Factor, new List<TokenType> {TokenType.IDECIE});
		}

		// multiply or divide
		private ASTNode Term()
		{
			return this.Operation(this.Index, new List<TokenType> { TokenType.MULTIPLY, TokenType.DIVIDE });
		}

		// add or subtract
		private ASTNode Expression()
		{
			return this.Operation(this.Term, new List<TokenType> { TokenType.ADD, TokenType.SUBTRACT });
		}


		private ASTNode Operation(Func<ASTNode> func, List<TokenType> operators)
		{
			ASTNode left = func();
			try {
				while (operators.Contains((TokenType)(this.CurrentToken?.type))) {
					Token operation = (Token)this.CurrentToken;
					this.Advance();
					ASTNode right = func();
					left = new OperatorNode(operation, left, right);
				}
			} catch (Exception e) {
				Console.WriteLine("error in parsing");
				Console.WriteLine(e.Message);
			}
			return left;
		}
	}
}