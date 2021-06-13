// ParameterNode.cs
// Copyright:  
// Created On: 08/06/2021
using System;
namespace Interpreter
{
	public class ParameterNode : ASTNode
	{
		public ASTNode expression;

		public ParameterNode(Token token, long start, long end, ASTNode child) : base(token.type, start, end)
		{
			this.expression = child;
		}

		public override void accept(IInterpreter interpreter)
		{
			interpreter.Visit(this);
		}
	}
}
