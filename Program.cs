using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Interpreter
{
	class Program
	{
		readonly static JsonSerializerOptions Options = new JsonSerializerOptions() {
			WriteIndented = true,
		};

		static void Main(string[] args)
		{
			Tokenizer tokenizer1 = new Tokenizer("sin((40+50))*10");
			List<Token> tokens = tokenizer1.Tokens;

			Parser parser = new Parser(tokens);
			ASTNode astTree = parser.Parse();

			Interpreter interpreter1 = new Interpreter(astTree);
			double result = interpreter1.exec();

			Console.WriteLine("[Main]: Got result {0}", result);

			RPNInterpreter interpreter2 = new RPNInterpreter(astTree);
			interpreter2.exec();
		}
	}
}
