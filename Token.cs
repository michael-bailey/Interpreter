using System;



namespace Interpreter
{
  [Serializable]
  class Token {
    public TokenType type {get; set;}
    public String literal {get; set;}
    public Int32 start {get; set;}
    public Int32 end {get; set;}


    public Token(TokenType type, String literal, Int32 start, Int32 end) {
      this.type = type;
      this.literal = literal;
      this.start = start;
      this.end = end;
    }
  }
}