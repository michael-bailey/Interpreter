// ArgParser.cs
// Copyright:  
// Created On: 13/06/2021
using System;
namespace Interpreter
{
	public static class ArgParser
	{
		public static ArgStruct Parse(String[] args)
		{

			ArgStruct argStruct = new ArgStruct();

			for (int i = 0; i < args.Length; i++) {
				switch (args[i]) {
					case "-f":
						i++;
						argStruct.Input = args[i];
						continue;
					case "-I":
						argStruct.Interactive = true;
						continue;
					case "-o":
						i++;
						argStruct.Output = args[i];
						continue;
				}
			}
			return argStruct;
		}
	}
}
