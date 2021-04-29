using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Interpreter
{
  class Program
  {
    readonly static JsonSerializerOptions Options = new JsonSerializerOptions()
    {
      WriteIndented = true
    };

		static void Main(string[] args)
    {
      Tokenizer tokenizer1 = new Tokenizer("12.34+345*12.3-234");
      List<Token> tokens = tokenizer1.Tokens;
			string tokenString = JsonSerializer.Serialize(tokens, Options);
			Console.WriteLine("[Main]: Got tokens {0}", tokenString);

      Parser parser = new Parser(tokens);
      ASTNode astTree = parser.Parse();
      string ASTString = JsonSerializer.Serialize(astTree, Options);
      Console.WriteLine("[Main]: Got AST {0}", ASTString);
    }
  }
}
