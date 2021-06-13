using System;

namespace Interpreter
{
	[Serializable]
	public class OperatorNode : ASTNode
	{
		public ASTNode left { get; private set; }
		public ASTNode right { get; private set; }
		public string literal { get; private set; }

		public OperatorNode(Token token, long start, long end, ASTNode left, ASTNode right) : base(token.type, start, end)
		{
			this.left = left;
			this.right = right;
			this.literal = token.Literal;
		}

		public override void accept(IInterpreter interpreter)
		{
			interpreter.Visit(this);
		}
	}
}