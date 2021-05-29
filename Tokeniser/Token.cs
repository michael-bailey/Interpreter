using System;

namespace Interpreter
{
  [Serializable]
  public class Token {
    public TokenType type {get; set;}
		public String? Literal { get; set; }
    public Int32 start {get; set;}
    public Int32 end {get; set;}


    public Token(TokenType type, String literal, Int32 start, Int32 end) {
      this.type = type;
      this.Literal = literal;
      this.start = start;
      this.end = end;
    }
  }
}