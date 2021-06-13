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
		private Stack<double> stack { get; set; }

		public Interpreter(ASTNode treeRoot)
		{
			this.Tree = treeRoot;
			this.stack = new();
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

			switch (node.type) {
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
				case TokenType.MODULO:
					this.stack.Push(left % right);
					break;
			}
		}

		public double exec()
		{
			this.Tree.accept(this);
			return this.stack.Peek();
		}

		public void Visit(FunctionNode node)
		{
			List<int> newParams = new();
			for (int i = 0; i < node.parameters.Count; i++) {
				node.parameters[i].accept(this);
			}

			switch (node.name) {
				case "sin":
					this.stack.Push(Math.Sin(this.stack.Pop()));
					break;

				case "cos":
					this.stack.Push(Math.Cos(this.stack.Pop()));
					break;

				case "tan":
					this.stack.Push(Math.Tan(this.stack.Pop()));
					break;
				default:
					throw new Exception("function not implemented");
			}
		}

		public void Visit(ParameterNode node)
		{
			throw new NotImplementedException();
		}
	}
}
