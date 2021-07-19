using System;

namespace Interpreter
{
	public interface IInterpreter
	{
		// interpretation definitons
		void Interpret(dynamic node);
		object? getRecentResult();

		// visitor pattern definitions
		void Visit(NumberNode node);
		void Visit(OperatorNode node);
		void Visit(FunctionNode node);
		void Visit(DocumentNode node);
		void Visit(EmptyNode node);
		void Visit(AssignmentNode node);
		void Visit(NameNode node);
	}
}