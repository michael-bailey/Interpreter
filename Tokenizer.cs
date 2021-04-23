using System;
using System.Collections.Generic;



namespace Interpreter
{
  enum TokenType {
    OPEN_BRACKET,
    CLOSED_BRACKET,
    NUMBER,
    EOF,
  }

  struct Token {
    TokenType type;
    String literal;
    Int32 start;
    Int32 end;


    public Token(TokenType type, String literal, Int32 start, Int32 end) {
      this.type = type;
      this.literal = literal;
      this.start = start;
      this.end = end;
    }
  }

  class Tokenizer {

    List<Token> Tokens {get; set;}
    String Source;

    Int32 start;
    Int32 current;
    Char? currentChar {
      get {
        try {
          return this.Source[this.current++];
        } catch(IndexOutOfRangeException e) {
          return null;
        }
      }
    }
    
    Char? nextChar {
      get {
        try {
          return this.Source[this.current++];
        } catch(IndexOutOfRangeException e) {
          return null;
        }
      }
    }

    public Tokenizer(String source) {
      this.Source = source;
      this.Tokens = new List<Token>();

      this.start = 0; 
      this.current = 0;

      while (!this.isEnd()) {
        char currentChar = this.Source[current];
        Console.WriteLine("[Tokenizer]: Next char {0}", currentChar);

        switch(currentChar) {
          case '(': this.addToken(new Token(TokenType.OPEN_BRACKET, null, this.start, this.current)); break;
          case ')': this.addToken(new Token(TokenType.CLOSED_BRACKET, null, this.start, this.current)); break;
          default:
            if (Char.IsDigit(currentChar)) {
              while (true) {

              }
            }
            break;
        }
        this.current++;
      }
    }

    private Boolean isEnd() {
      return this.Source.Length <= current;
    }

    private void advance() {

    }

    private void addToken(Token token) {
      Console.WriteLine("[Tokenizer]: added token {0}", token);
      this.Tokens.Add(token);
    }
  }
}