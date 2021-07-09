using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
	public class Interpreter: IInterpreter
	{
		private ASTNode Tree { get; set; }
		private Stack<Double> Stack { get; set; }
		private List<Double?> Results { get; set; }

		public Interpreter(DocumentNode treeRoot)
		{
			this.Tree = treeRoot;
			this.Stack = new();
			this.Results = new();
		}

		public List<Double?> Exec()
		{
			this.Tree.accept(this);
			return this.Results;
		}

		public void Visit(NumberNode node)
		{
			this.Stack.Push(node.value);
		}

		public void Visit(OperatorNode node)
		{
			node.left.accept(this);
			node.right.accept(this);

			var right = this.Stack.Pop();
			var left = this.Stack.Pop();

			switch (node.type)
			{
				case TokenType.ADD:
					this.Stack.Push(left + right);
					break;
				case TokenType.SUBTRACT:
					this.Stack.Push(left - right);
					break;
				case TokenType.MULTIPLY:
					this.Stack.Push(left * right);
					break;
				case TokenType.DIVIDE:
					this.Stack.Push(left / right);
					break;
				case TokenType.IDECIE:
					this.Stack.Push((float)Math.Pow(left, right));
					break;
				case TokenType.MODULO:
					this.Stack.Push(left % right);
					break;
			}
		}

		public void Visit(FunctionNode node)
		{
			for (int i = 0; i < node.parameters.Count; i++)
			{
				node.parameters[i].accept(this);
			}

			switch (node.name)
			{
				case "sin":
					this.Stack.Push(Math.Sin(this.Stack.Pop()));
					break;

				case "cos":
					this.Stack.Push(Math.Cos(this.Stack.Pop()));
					break;

				case "tan":
					this.Stack.Push(Math.Tan(this.Stack.Pop()));
					break;
				case "sum":
					double start = 0;
					for (int i = 0; i < node.parameters.Count; i++)
						start += this.Stack.Pop();
					this.Stack.Push(start);
					break;

				default:
					throw new Exception("function not implemented");
			}
		}

		public void Visit(DocumentNode node)
		{
			for (int i = 0; i < node.expressions.Count; i++)
			{
				switch (node.expressions[i])
				{
					case NumberNode n:
						this.Visit(n);
						break;
					case OperatorNode n:
						this.Visit(n);
						break;
					case FunctionNode n:
						this.Visit(n);
						break;
				}
				if (this.Stack.Count > 0)
				{
					var a = this.Stack.Pop();
					this.Results.Add(a);
				}
				else
				{
					this.Results.Add(null);
				}
				this.Stack.Clear();
			}
		}

		public void Visit(EmptyNode node)
		{
			this.Results.Add(null);
		}
	}
}
