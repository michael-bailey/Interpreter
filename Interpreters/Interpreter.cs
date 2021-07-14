using System;
using System.Collections.Generic;

namespace Interpreter
{
	public class Interpreter: IInterpreter
	{
		private ASTNode Tree { get; set; }
		private Stack<Object> Stack { get; set; }
		private List<Object?> Results { get; set; }
		private Dictionary<String, Double> Environment { get; set; }

		public Interpreter(DocumentNode treeRoot)
		{
			this.Tree = treeRoot;
			this.Stack = new();
			this.Results = new();
			this.Environment = new();
		}

		public List<Object?> Exec()
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


			// FIX: - Change this to check for variables and cast appropriately
			var right = (Double)this.Stack.Pop();
			var left = (Double)this.Stack.Pop();

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
			// FIX: - Change cast to check for variables and cast appropriately
			switch (node.name)
			{
				case "sin":
					this.Stack.Push(Math.Sin((Double)this.Stack.Pop()));
					break;

				case "cos":
					this.Stack.Push(Math.Cos((Double)this.Stack.Pop()));
					break;

				case "tan":
					this.Stack.Push(Math.Tan((Double)this.Stack.Pop()));
					break;
				case "sum":
					double start = 0;
					for (int i = 0; i < node.parameters.Count; i++)
						start += (Double)this.Stack.Pop();
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
					case AssignmentNode n:
						this.Visit(n);
						break;
				}
				if (this.Stack.Count > 0)
				{
					// FIX: - Change this to check for variables and cast appropriately
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

		public void Visit(AssignmentNode node)
		{
			node.Name.accept(this);
			node.Expression.accept(this);

			var res = (Double)this.Stack.Pop();
			var name = (String)this.Stack.Pop();

			this.Environment.Add(name, res);
			this.Stack.Push(name + ": " + res.ToString());
		}

		public void Visit(NameNode node)
		{
			this.Stack.Push(node.Name);
		}
	}
}
