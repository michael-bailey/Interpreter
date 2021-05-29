using System;

namespace Interpreter
{
	[Serializable]
	public abstract class ASTNode
	{
		public Token token { get; set; }

		public ASTNode(Token token)
		{
			this.token = token;
		}

		public abstract void accept(IInterpreter interpreter);
	}
}