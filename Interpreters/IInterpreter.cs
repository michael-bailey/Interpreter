namespace Interpreter
{
	public interface IInterpreter
	{
		void Visit(NumberNode node);
		void Visit(OperatorNode node);
		void Visit(FunctionNode node);
		void Visit(ParameterNode node);
	}
}