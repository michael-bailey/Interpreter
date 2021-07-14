using System;

namespace Interpreter
{
	public class NameNode : ASTNode
	{

		public String Name { get; private set;}

		public NameNode(TokenType type, Int64 start, Int64 end, String name) : base(type, start, end)
		{
			this.Name = name;
		}

		public override void accept(IInterpreter interpreter)
		{
			interpreter.Visit(this);
		}
	}
}