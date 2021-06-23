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

			Tokenizer tokenizer1 = new("2*2\n3*3");
			List<Token> tokens = tokenizer1.Tokens;

			Parser parser = new(tokens);
			DocumentNode document = parser.Parse();

			Interpreter interpreter1 = new(document);
			List<Double?> result = interpreter1.Exec();

			Console.WriteLine("[Main]: Got result {0}", JsonSerializer.Serialize<Object>(result));

			// RPNInterpreter interpreter2 = new RPNInterpreter(astTree);
			// interpreter2.exec();
		}


		static void InteractiveMode()
		{
			String inputbuffer;

			while (true) {
				Console.Write(":>");
				inputbuffer = Console.ReadLine();
				List<Token> tokens = new Tokenizer(inputbuffer).Tokens;
				DocumentNode document = new Parser(tokens).Parse();
				List<Double?> result = new Interpreter(document).Exec();
				Console.WriteLine(result);
			}
		}
	}
}
