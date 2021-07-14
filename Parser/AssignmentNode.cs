using System;

namespace Interpreter
{
	public class AssignmentNode : ASTNode
	{

		public ASTNode Name { get; set; }
		public ASTNode Expression { get; set; }

		public AssignmentNode(TokenType type, Int64 start, Int64 end, ASTNode name, ASTNode expression) : base(type, start, end)
		{
			this.Name = name;
			this.Expression = expression;

		}

		public override void accept(IInterpreter interpreter)
		{
			interpreter.Visit(this);
		}
	}
}
