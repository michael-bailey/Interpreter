using System;
using System.Collections.Generic;



namespace Interpreter
{
	class Tokenizer
	{

		public List<Token> Tokens { get; protected set; }
		readonly String Source;

		Int32 start;
		Int32 current;

		Char CurrentChar {
			get {
				try {
					return this.Source[this.current];
				} catch (IndexOutOfRangeException) {
					return '\0';
				}
			}
		}

		Char NextChar {
			get {
				try {
					return this.Source[this.current + 1];
				} catch (IndexOutOfRangeException) {
					return '\0';
				}
			}
		}

		public Tokenizer(String source)
		{
			this.Source = source;
			this.Tokens = new List<Token>();

			this.start = 0;
			this.current = 0;

			while (!this.IsEnd()) {
				this.start = this.current;
				Console.WriteLine("[Tokenizer]: Next char {0}", CurrentChar);

				switch (CurrentChar) {
					case '(': this.AddToken(new Token(TokenType.OPEN_BRACKET, "(", this.start, this.current)); this.Advance(); break;
					case ')': this.AddToken(new Token(TokenType.CLOSED_BRACKET, ")", this.start, this.current)); this.Advance(); break;
					case '+': this.AddToken(new Token(TokenType.ADD, "+", this.start, this.current)); this.Advance(); break;
					case '-': this.AddToken(new Token(TokenType.SUBTRACT, "-", this.start, this.current)); this.Advance(); break;
					case '*': this.AddToken(new Token(TokenType.MULTIPLY, "*", this.start, this.current)); this.Advance(); break;
					case '/': this.AddToken(new Token(TokenType.DIVIDE, "/", this.start, this.current)); this.Advance(); break;
					case '^': this.AddToken(new Token(TokenType.IDECIE, "^", this.start, this.current)); this.Advance(); break;
					case '%': this.AddToken(new Token(TokenType.MODULO, "%", this.start, this.current)); this.Advance(); break;
					case ' ': this.Advance(); break;
					default:
						if (Char.IsDigit(CurrentChar)) {
							while (Char.IsDigit(CurrentChar)) this.Advance();
							if (CurrentChar == '.' && Char.IsDigit(NextChar)) {
								this.Advance();
								while (Char.IsDigit(CurrentChar)) this.Advance();
							}

							Console.WriteLine("[Tokenizer]: got number {0}", source[this.start..this.current]);
							this.AddToken(
								new Token(TokenType.NUMBER, source[this.start..this.current], this.start, this.current)
							);
						} else if (Char.IsLetter(CurrentChar)) {
							while (Char.IsLetter(CurrentChar)) this.Advance();
							this.AddToken(
								new Token(TokenType.NAME, source[this.start..this.current], this.start, this.current)
							);
						}
						break;
				}
			}
		}

		private Boolean IsEnd() => this.Source.Length <= current;

		private void Advance() => this.current++;

		private void AddToken(Token token)
		{
			Console.WriteLine("[Tokenizer]: added token {0}", token);
			this.Tokens.Add(token);
		}
	}
}