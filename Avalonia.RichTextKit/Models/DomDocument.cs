namespace Avalonia.RichTextKit.Models;

public class DomDocument
{
	private readonly List<TextStyle> _styles = [];
	private readonly List<DomBlock> _blocks = [];

	internal event DocumentChangedEvent? DocumentChanged;
	public string Version { get; set; }
	public DateTime LastModified { get; set; }
	public double Width { get; set; }
	public double Height { get; private set; }
	public double LineSpacing { get; set; } = 1.5;
	public double WordSpacing { get; set; } = 1.3;
	public double FontSize { get; set; } = 16;

	internal IReadOnlyList<DomBlock> Blocks => _blocks;
	internal IReadOnlyList<TextStyle> Styles => _styles;


	#region 获取样式信息

	internal TextStyle GetStyle(int styleIndex)
	{
		if (_styles.Count <= 0)
		{
			_styles.Add(new TextStyle
			{
				BackgroundColor = "#F1F1F1",
				Bold = false,
				ForegroundColor = "#313131",
				Italic = false,
				Underline = false,
			});
		}

		if (_styles.Count < styleIndex)
			return _styles[0];

		return _styles[styleIndex];
	}

	internal int AddStyle(TextStyle style)
	{
		var index = _styles.IndexOf(style);
		if (index != -1)
		{
			return index;
		}

		_styles.Add(style);
		return _styles.Count - 1;
	}

	#endregion

	private void Reset()
	{
		_styles.Clear();
		_styles.Add(new TextStyle
		{
			BackgroundColor = "#F1F1F1",
			Bold = false,
			ForegroundColor = "#313131",
			Italic = false,
			FontSize = 16,
			Underline = false,
		});
		_blocks.Clear();
		_blocks.Add(new DomBlock(this));
	}

	public void InsertBlock(int index, DomBlock newBlock)
	{
		this._blocks.Insert(this._blocks.Count, newBlock);
	}

	public void Clear()
	{
		this._blocks.Clear();
	}

	internal void RaiseDocumentChanged()
	{
		this.DocumentChanged?.Invoke(this);
	}
	
}

internal delegate void DocumentChangedEvent(DomDocument document);

