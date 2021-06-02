using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
	public class Interpreter : IInterpreter
	{

		private ASTNode Tree { get; set; }
		private Stack<float> stack { get; set; }

		public Interpreter(ASTNode treeRoot)
		{
			this.Tree = treeRoot;
			this.stack = new Stack<float>();
		}

		public void Visit(NumberNode node)
		{
			this.stack.Push(node.value);
		}

		public void Visit(OperatorNode node)
		{
			node.left.accept(this);
			node.right.accept(this);

			var right = this.stack.Pop();
			var left = this.stack.Pop();

			switch (node.token.type)
			{
				case TokenType.ADD:
					this.stack.Push(left + right);
					break;
				case TokenType.SUBTRACT:
					this.stack.Push(left - right);
					break;
				case TokenType.MULTIPLY:
					this.stack.Push(left * right);
					break;
				case TokenType.DIVIDE:
					this.stack.Push(left / right);
					break;
				case TokenType.IDECIE:
					this.stack.Push((float)Math.Pow(left, right));
					break;
			}
		}

		public float exec()
		{
			this.Tree.accept(this);
			return this.stack.Peek();
		}
	}
}
