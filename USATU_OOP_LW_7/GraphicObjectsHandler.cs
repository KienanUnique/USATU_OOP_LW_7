using System.Drawing;
using CustomDoublyLinkedListLibrary;

namespace USATU_OOP_LW_7;

public class GraphicObjectsHandler
{
    private readonly CustomDoublyLinkedList<GraphicObject> _graphicObjects = new();
    private bool _isMultipleSelectionEnabled;
    private readonly Size _backgroundSize;

    public GraphicObjectsHandler(Size backgroundSize) =>
        _backgroundSize = backgroundSize;

    public void JoinSelectedGraphicObject()
    {
        if (IsOnlySingleGeometricObjectSelected())
        {
            return;
        }

        var newGraphicObjectGroup = new GraphicObjectGroup();
        for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
        {
            if (i.Current.IsSelected())
            {
                newGraphicObjectGroup.AddGraphicObject(i.Current);
                _graphicObjects.RemovePointerElement(i);
            }
        }

        _graphicObjects.Add(newGraphicObjectGroup);
        UnselectAll();
    }

    public void SeparateSelectedGraphicObjects()
    {
        for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
        {
            if (i.Current.IsSelected() && i.Current.IsGroup())
            {
                var currentGroupList = ((GraphicObjectGroup) i.Current).GetAllGraphicObjects();
                _graphicObjects.InsertListBeforePointer(currentGroupList, i);
                _graphicObjects.RemovePointerElement(i);
            }
        }

        UnselectAll();
    }

    public void DrawOnGraphics(Graphics graphics)
    {
        for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
        {
            i.Current.DrawOnGraphics(graphics);
        }
    }

    public void EnableMultipleSelection()
    {
        _isMultipleSelectionEnabled = true;
    }

    public void DisableMultipleSelection()
    {
        _isMultipleSelectionEnabled = false;
    }

    public bool TryProcessSelectionClick(Point clickPoint)
    {
        bool wasOnObject = false;
        for (var i = _graphicObjects.GetPointerOnEnd(); !i.IsBorderReached(); i.MovePrevious())
        {
            if (i.Current.IsPointInside(clickPoint))
            {
                wasOnObject = true;
                if (!i.Current.IsSelected() && !_isMultipleSelectionEnabled)
                {
                    UnselectAll();
                }

                i.Current.ProcessClick();
                break;
            }
        }

        return wasOnObject;
    }

    public void AddFigure(Figures figureType, Color color, Point location)
    {
        Figure newFigure = null;
        switch (figureType)
        {
            case Figures.Circle:
                newFigure = new Circle(color, location);
                break;
            case Figures.Square:
                newFigure = new Square(color, location);
                break;
            case Figures.Triangle:
                newFigure = new Triangle(color, location);
                break;
            case Figures.Pentagon:
                newFigure = new Pentagon(color, location);
                break;
        }

        if (!newFigure.IsFigureOutside(_backgroundSize))
        {
            _graphicObjects.Add(newFigure);
        }

        UnselectAll();
    }

    public void ProcessColorClick(Point clickLocation, Color color)
    {
        bool wasColored = false;
        for (var i = _graphicObjects.GetPointerOnEnd(); !i.IsBorderReached(); i.MovePrevious())
        {
            if (i.Current.IsPointInside(clickLocation))
            {
                if (i.Current.IsSelected())
                {
                    for (var k = _graphicObjects.GetPointerOnBeginning(); !k.IsBorderReached(); k.MoveNext())
                    {
                        if (k.Current.IsSelected())
                        {
                            k.Current.Color(color);
                        }
                    }
                }
                else
                {
                    i.Current.Color(color);
                    UnselectAll();
                    i.Current.Select();
                }

                wasColored = true;
                break;
            }
        }

        if (!wasColored)
        {
            UnselectAll();
        }
    }

    public void ResizeSelectedFigures(int changeSizeK, ResizeAction resizeAction)
    {
        for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
        {
            if (i.Current.IsSelected() && i.Current.IsResizePossible(changeSizeK, resizeAction, _backgroundSize))
            {
                i.Current.Resize(changeSizeK, resizeAction);
            }
        }
    }

    public void MoveSelectedFigures(Point moveVector)
    {
        for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
        {
            if (i.Current.IsSelected() && i.Current.IsMovePossible(moveVector, _backgroundSize))
            {
                i.Current.Move(moveVector);
            }
        }
    }

    public void DeleteAllSelected()
    {
        for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
        {
            if (i.Current.IsSelected())
            {
                _graphicObjects.RemovePointerElement(i);
            }
        }
    }

    private void UnselectAll()
    {
        for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
        {
            if (i.Current.IsSelected())
            {
                i.Current.Unselect();
            }
        }
    }

    private bool IsOnlySingleGeometricObjectSelected()
    {
        bool wasOneSelectedObjectPassed = false;
        for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
        {
            if (!i.Current.IsSelected()) continue;
            if (wasOneSelectedObjectPassed)
            {
                return false;
            }
            else
            {
                wasOneSelectedObjectPassed = true;
            }
        }

        return true;
    }
}