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
			dynamic document;
			dynamic result;
			Interpreter interpreter = new();
			List<String> strings = new();

			if (argStruct.Interactive) {
				Program.InteractiveMode();
				return;
			}
			if(argStruct.Input != null) {
				string input = File.ReadAllText(argStruct.Input);
				Console.WriteLine("got input: {0}", input);
				Console.WriteLine(input);
				tokens = new Tokenizer(input).Tokens;
				document = new Parser(tokens).Parse();
				interpreter.Interpret(document);
				result = interpreter.Results;
				Console.WriteLine("[Main]: Got result {0}", JsonSerializer.Serialize<Object>(result, Options));
			} else {
				tokens = new Tokenizer("2*2\n\n3*3").Tokens;
				document = new Parser(tokens).Parse();
				interpreter.Interpret(document);
				result = interpreter.Results;
			}
			for (int i = 0; i < result.Count; i++) {
				strings.Add(result[i]?.ToString() ?? "");
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
			Interpreter interpreter = new();

			while (true) {
				Console.Write(" > ");
				inputbuffer = Console.ReadLine();
				List<Token> tokens = new Tokenizer(inputbuffer).Tokens;
				dynamic document = new Parser(tokens).Parse();
				interpreter.Interpret(document);
				dynamic result = interpreter.getRecentResult();
				Console.WriteLine(result.ToString());
			}
		}
	}
}
