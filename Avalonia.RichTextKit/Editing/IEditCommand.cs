namespace Avalonia.RichTextKit.Editing;

public interface IEditCommand
{	
	void Execute();
	void Undo();
}