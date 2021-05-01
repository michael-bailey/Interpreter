using System;

namespace Interpreter
{
	[Serializable]
	class ASTNode
	{
		public readonly Token token;

		public ASTNode(Token token)
		{
			this.token = token;
		}
	}
}