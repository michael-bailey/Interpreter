using System;
using System.Collections.Generic;
using System.Threading;

namespace Interpreter
{
	[Serializable]
	public abstract class ASTNode
	{
		public TokenType type { get; private set; }
		public Int64 Start { get; private set; }
		public Int64 End { get; private set; }

		public ASTNode(TokenType type, Int64 start, Int64 end)
		{
			this.type = type;
			this.Start = start;
			this.End = end;
		}

		public abstract void accept(IInterpreter interpreter);
	}
}