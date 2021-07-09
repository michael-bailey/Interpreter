using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
	class RPNInterpreter : IInterpreter
	{
		private ASTNode Tree { get; set; }

		public RPNInterpreter(ASTNode treeRoot)
		{
			this.Tree = treeRoot;
		}

		public void exec()
		{
			this.Tree.accept(this);
		}

		public void Visit(NumberNode node)
		{
			Console.Write("{0} ", node.value);
		}

		public void Visit(OperatorNode node)
		{
			node.left.accept(this);
			node.right.accept(this);
			Console.Write(node.literal + ' ');
		}

		public void Visit(FunctionNode node)
		{
			foreach (var param in node.parameters) {
				param.accept(this);
			}
			Console.Write(node.name + ' ');
		}

		{
			node.expression.accept(this);
		}

		{
			Console.WriteLine("document C:");
		}
	}
}
