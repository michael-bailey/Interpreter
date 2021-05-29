using System;
using System.Collections.Generic;
using System.Linq;
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
			Console.Write(node.token.Literal + ' ');
		}

		public void Visit(OperatorNode node)
		{
			node.left.accept(this);
			node.right.accept(this);
			Console.Write(node.token.Literal + ' ');
		}
	}
}
