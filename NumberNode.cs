using System;

namespace Interpreter
{
	[Serializable]
	class NumberNode : ASTNode
	{
		public NumberNode(Token token) : base(token)
		{
		}
	}
}