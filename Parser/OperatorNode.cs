using System;

namespace Interpreter
{
	[Serializable]
	public class OperatorNode : ASTNode
	{
		public ASTNode left { get; private set; }
		public ASTNode right { get; private set; }

		public OperatorNode(Token token, ASTNode left, ASTNode right) : base(token)
		{
			this.left = left;
			this.right = right;
		}

		public override void accept(IInterpreter interpreter)
		{
			interpreter.Visit(this);
		}
	}
}