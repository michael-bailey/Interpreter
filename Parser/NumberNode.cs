﻿using System;

namespace Interpreter
{
	[Serializable]
	public class NumberNode : ASTNode
	{

		public float value { get; private set; }

		public NumberNode(Token token, long start, long end, string value) : base(token.type, start, end)
		{
			this.value = float.Parse(value);
		}

		public override void accept(IInterpreter interpreter)
		{
			interpreter.Visit(this);
		}
	}
}