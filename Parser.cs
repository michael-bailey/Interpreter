using System;
using System.Collections.Generic;

namespace Interpreter
{
	class Parser
	{
		List<Token> tokens;
		Int32 start;
		Int32 current;

		Token? CurrentToken
		{
			get
			{
				try
				{
					return this.tokens[this.current];
				}
				catch (IndexOutOfRangeException)
				{
					return null;
				}
			}
		}

		Token? NextToken
		{
			get
			{
				try
				{
					return this.tokens[this.current + 1];
				}
				catch (IndexOutOfRangeException)
				{
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

		//any number
		private ASTNode? Number()
		{
			Token current = this.CurrentToken;
			if (current?.type == TokenType.NUMBER)
			{
				this.Advance();
				return new NumberNode(current);
			}
			throw new Exception("token was not a number");
		}

		// multiply or divide
		private ASTNode Term()
		{
			return this.Operation(this.Number, new List<TokenType> { TokenType.MULTIPLY, TokenType.DIVIDE });
		}

		// add or subtract
		private ASTNode Expression()
		{
			return this.Operation(this.Term, new List<TokenType> { TokenType.ADD, TokenType.SUBTRACT });

		}

		private ASTNode Operation( Func<ASTNode> func, List<TokenType> operators)
		{
			ASTNode left = func();

			while (operators.Contains((TokenType)(this.CurrentToken?.type)))
			{
				Token operation = (Token)this.CurrentToken;
				this.Advance();
				ASTNode right = func();
				left = new OperatorNode(this.CurrentToken, left, right);
			}

			return left;
		}
	}

	class ASTNode
	{
		public readonly Token token;

		public ASTNode(Token token)
		{
			this.token = token;
		}
	}

	class NumberNode : ASTNode
	{
		public NumberNode(Token token) : base(token)
		{
		}
	}

	class OperatorNode : ASTNode
	{

		public readonly ASTNode left;
		public readonly ASTNode right;

		public OperatorNode(Token token, ASTNode left, ASTNode right) : base(token)
		{
			this.left = left;
			this.right = right;
		}
	}
}