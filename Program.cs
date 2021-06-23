using System;
using System.Collections.Generic;
using System.IO;
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

			if(argStruct.Input != null) {
				string input = File.ReadAllText(argStruct.Input);
				Console.WriteLine("got input:");
				Console.WriteLine(input);

				List<Token> tokens = new Tokenizer(input).Tokens;
				DocumentNode document = new Parser(tokens).Parse();
				List<Double?> result = new Interpreter(document).Exec();
				Console.WriteLine("[Main]: Got result {0}", JsonSerializer.Serialize<Object>(result, Options));
			}

			// List<Token> tokens = new Tokenizer("2*2\n3*3").Tokens;
			// DocumentNode document = new Parser(tokens).Parse();
			// List<Double?> result = new Interpreter(document).Exec();
		}


		static void InteractiveMode()
		{
			String inputbuffer;

			while (true) {
				Console.Write(" > ");
				inputbuffer = Console.ReadLine();
				List<Token> tokens = new Tokenizer(inputbuffer).Tokens;
				DocumentNode document = new Parser(tokens).Parse();
				Double? result = new Interpreter(document).Exec()[0];
				Console.WriteLine(result);
			}
		}
	}
}
