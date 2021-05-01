using System;

namespace Interpreter
{
	[Serializable]
	class OperatorNode : ASTNode
	{
		public readonly ASTNode left;
		public readonly ASTNode right;

		public OperatorNode(Token token, ASTNode left, ASTNode right) : base(token)
		{
			this.left = left;
			this.right = right;
		}
	}
}