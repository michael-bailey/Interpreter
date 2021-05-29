namespace Interpreter
{
	public interface IInterpreter
	{
		void Visit(NumberNode node);
		void Visit(OperatorNode node);
	}
}