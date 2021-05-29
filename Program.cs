using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Interpreter
{
  class Program
  {
    readonly static JsonSerializerOptions Options = new JsonSerializerOptions()
    {
      WriteIndented = true,
    };

		static void Main(string[] args)
    {
      Tokenizer tokenizer1 = new Tokenizer("10294843*246238472634-12142/2*2134732+23792584+2345");
      List<Token> tokens = tokenizer1.Tokens;

			Parser parser = new Parser(tokens);
      ASTNode astTree = parser.Parse();

      Interpreter interpreter1 = new Interpreter(astTree);
      float result = interpreter1.exec();

      Console.WriteLine("[Main]: Got result {0}", result);

      RPNInterpreter interpreter2 = new RPNInterpreter(astTree);
      interpreter2.exec();

    }
  }
}
