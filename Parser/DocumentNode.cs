using System;
using System.Collections.Generic;

namespace Interpreter
{
	public class DocumentNode : ASTNode 
	{
		public readonly List<ASTNode> expressions;

		public DocumentNode(TokenType type, long start, long end, List<ASTNode> parameters) : base(type, start, end)
		{
			this.expressions = parameters;
		}

		public override void accept(IInterpreter interpreter)
		{
			interpreter.Visit(this);
		}

		public Int32 ChildCount()
		{
			return this.expressions.Count;
		}
	}
}
