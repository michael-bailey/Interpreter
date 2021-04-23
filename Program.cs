using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Interpreter
{
  class Program
  {
    static JsonSerializerOptions options = new JsonSerializerOptions()
    {
      WriteIndented = true
    };

    static void Main(string[] args)
    {
      Tokenizer tokenizer1 = new Tokenizer("12.34+345*(12.3-234)");
      List<Token> tokens = tokenizer1.Tokens;
      String tokenString = JsonSerializer.Serialize(tokens, options);
      Console.WriteLine("[Main]: Got tokens {0}", tokenString);
    }
  }
}
