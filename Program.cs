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
			ArgStruct argStruct = ArgParser.Parse(args);
			List<Token> tokens;
			DocumentNode document;
			List<Double?> result;
			List<String> strings = new();
			if (argStruct.Interactive) {
				Program.InteractiveMode();
			}
			if(argStruct.Input != null) {
				string input = File.ReadAllText(argStruct.Input);
				Console.WriteLine("got input:");
				Console.WriteLine(input);
				tokens = new Tokenizer(input).Tokens;
				document = new Parser(tokens).Parse();
				result = new Interpreter(document).Exec();
				Console.WriteLine("[Main]: Got result {0}", JsonSerializer.Serialize<Object>(result, Options));
			} else {
				tokens = new Tokenizer("2*2\n\n3*3").Tokens;
				document = new Parser(tokens).Parse();
				result = new Interpreter(document).Exec();
			}
			for (int i = 0; i < result.Count; i++) {
				strings.Add(result[i].ToString() ?? "\n");
			}
			if (argStruct.Output != null) {
				File.WriteAllLines(argStruct.Output, strings);
			} else {
				foreach (var item in strings)
				{
					Console.WriteLine("{0}", item);
				}
			}
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
