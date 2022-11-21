using System.Drawing;

namespace USATU_OOP_LW_7;

public enum ResizeAction
{
    Increase,
    Decrease
}

public abstract class GraphicObject
{
    protected bool _isSelected; // TODO: SetSelected or just set
    public abstract bool IsFigureOutside(Size backgroundSize);
    public abstract void Color(Color newColor);
    public abstract bool IsResizePossible(int sizeK, ResizeAction resizeAction, Size backgroundSize);
    public abstract void Resize(int sizeK, ResizeAction resizeAction);
    public abstract bool IsMovePossible(Point moveVector, Size backgroundSize);
    public abstract void Move(Point moveVector);
    public abstract void DrawOnGraphics(Graphics graphics);
    public abstract bool IsSelected();
    public abstract void Select();
    public abstract void Unselect();
    public abstract void ProcessClick();
    public abstract bool IsPointInside(Point pointToCheck);
    public abstract bool IsGroup();
}