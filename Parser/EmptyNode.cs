
namespace Interpreter
{
	public class EmptyNode : ASTNode
	{

		public EmptyNode(TokenType type, long start, long end) : base(type,  start,  end) {}

		public override void accept(IInterpreter interpreter)
		{
			interpreter.Visit(this);
		}
	}
}
