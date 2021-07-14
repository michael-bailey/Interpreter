namespace Interpreter
{
	public interface IInterpreter
	{
		void Visit(NumberNode node);
		void Visit(OperatorNode node);
		void Visit(FunctionNode node);
		void Visit(DocumentNode node);
		void Visit(EmptyNode node);
		void Visit(AssignmentNode node);
		void Visit(NameNode node);
	}
}