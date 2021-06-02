using System;
using System.Collections.Generic;

namespace Interpreter
{
	[Serializable]
	public abstract class ASTNode
	{
		public Token token { get; private set; }

		public TokenType type { get; private set; }
		public Int64 start { get; private set; }
		public Int64 end { get; private set; }

		public Dictionary<String, ASTNode> children { get; private set; }



		public ASTNode(Token token)
		{
			this.token = token;
		}

		public abstract void accept(IInterpreter interpreter);
	}
}