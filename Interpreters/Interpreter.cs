using System;
using System.Collections.Generic;

namespace Interpreter
{
	public class Interpreter: IInterpreter
	{
		private Stack<Object> Stack { get; set; }
		public List<Object?> Results { get; private set; }
		public Dictionary<String, Double> Environment { get; private set; }

		public Interpreter()
		{
			this.Stack = new();
			this.Results = new();
			this.Environment = new();
		}

		public void Interpret(dynamic node)
		{
			this.Visit(node);
		}

		public object getRecentResult()
		{
			try {
				return this.Results[^1];
			} catch (System.ArgumentOutOfRangeException e) {
				return "\n";
			}
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

			var rightData = (right is String @rString) ? this.Environment[@rString] : (Double)right;
			var leftData = (left is String @lString) ? this.Environment[@lString] : (Double)left;

			switch (node.type)
			{
				case TokenType.ADD:
					this.Stack.Push(leftData + rightData);
					break;
				case TokenType.SUBTRACT:
					this.Stack.Push(leftData - rightData);
					break;
				case TokenType.MULTIPLY:
					this.Stack.Push(leftData * rightData);
					break;
				case TokenType.DIVIDE:
					this.Stack.Push(leftData / rightData);
					break;
				case TokenType.IDECIE:
					this.Stack.Push((float)Math.Pow(leftData, rightData));
					break;
				case TokenType.MODULO:
					this.Stack.Push(leftData % rightData);
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
					case NameNode n:
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
					case EmptyNode n:
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
			if(this.Environment.ContainsKey(node.Name)) {
				this.Stack.Push(this.Environment[node.Name]);
				return;
			}
			this.Stack.Push(node.Name);
		}
	}
}
