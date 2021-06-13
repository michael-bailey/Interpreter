// FunctionNode.cs
// Copyright:  
// Created On: 08/06/2021
using System;
using System.Collections.Generic;
using System.Threading;

namespace Interpreter
{
	[Serializable]
	public class FunctionNode : ASTNode
	{
		public readonly List<ASTNode> parameters;
		public string name;

		public FunctionNode(Token token, long start, long end, string name, List<ASTNode> parameters)
			: base(token.type, start, end)
		{
			this.parameters = parameters;
			this.name = name;
		}

		public override void accept(IInterpreter interpreter)
		{
			interpreter.Visit(this);
		}
	}
}
