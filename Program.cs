using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
			/* Passing arguments
			 * This section is designated to passing arguments for the programs execution type
			 */

			ArgStruct argStruct = ArgParser.Parse(args);

			if (argStruct.Interactive) {
				Program.InteractiveMode();
			}

			Tokenizer tokenizer1 = new Tokenizer("sum(1 2 3 4 5 6 7 8 9)");
			List<Token> tokens = tokenizer1.Tokens;

			Parser parser = new Parser(tokens);
			ASTNode astTree = parser.Parse();

			Interpreter interpreter1 = new Interpreter(astTree);
			double result = interpreter1.exec();

			Console.WriteLine("[Main]: Got result {0}", result);

			RPNInterpreter interpreter2 = new RPNInterpreter(astTree);
			interpreter2.exec();
		}


		static void InteractiveMode()
		{
			String inputbuffer;

			while (true) {
				Console.Write(":>");
				inputbuffer = Console.ReadLine();
				List<Token> tokens = new Tokenizer(inputbuffer).Tokens;
				ASTNode astTree = new Parser(tokens).Parse();
				double result = new Interpreter(astTree).exec();
				Console.WriteLine(result);
			}

		}
	}
}
