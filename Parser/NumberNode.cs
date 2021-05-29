using System;

namespace Interpreter
{
	[Serializable]
	public class NumberNode : ASTNode
	{

		public float value { get; private set; }

		public NumberNode(Token token, string value) : base(token)
		{
			this.value = float.Parse(value);
		}

		public override void accept(IInterpreter interpreter)
		{
			interpreter.Visit(this);
		}
	}
}