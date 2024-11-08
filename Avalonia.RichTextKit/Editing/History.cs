namespace Avalonia.RichTextKit.Editing;

public class History
{
	private readonly Stack<IEditCommand> _redoStack = [];
	private readonly Stack<IEditCommand> _undoStack = [];
	
	public void Execute(IEditCommand cmd)
	{
		cmd.Execute();
		_undoStack.Push(cmd);
		_redoStack.Clear();
	}

	public void Undo()
	{
		if (!_undoStack.TryPop(out var cmd))
			return;
		cmd.Undo();
		_redoStack.Push(cmd);
	}

	public void Redo()
	{
		if (!_redoStack.TryPop(out var cmd))
			return;
		
		cmd.Execute();
		_undoStack.Push(cmd);
	}
}