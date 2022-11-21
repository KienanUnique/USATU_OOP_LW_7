using System.Drawing;
using CustomDoublyLinkedListLibrary;

namespace USATU_OOP_LW_7
{
    public class FiguresHandler
    {
        public delegate void NeedUpdateHandler();

        public event NeedUpdateHandler NeedUpdate;

        private readonly CustomDoublyLinkedList<Figure> _figures = new();
        private bool _isMultipleSelectionEnabled;
        private readonly Size _backgroundSize;

        public FiguresHandler(Size backgroundSize) => _backgroundSize = backgroundSize;

        public void DrawAllCirclesOnGraphics(Graphics graphics)
        {
            for (var i = _figures.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
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
            bool wasOnCircleClick = false;
            for (var i = _figures.GetPointerOnEnd(); !i.IsBorderReached(); i.MovePrevious())
            {
                if (i.Current.IsPointInside(clickPoint))
                {
                    wasOnCircleClick = true;
                    if (!i.Current.IsSelected() && !_isMultipleSelectionEnabled)
                    {
                        TryUnselectAll();
                    }

                    i.Current.ProcessClick();
                    break;
                }
            }

            if (wasOnCircleClick || TryUnselectAll())
            {
                NeedUpdate?.Invoke();
            }

            return wasOnCircleClick;
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
                _figures.Add(newFigure);
                NeedUpdate?.Invoke();
            }

            TryUnselectAll();
        }

        public void ProcessColorClick(Point clickLocation, Color color)
        {
            bool wasFigureClicked = false;
            for (var i = _figures.GetPointerOnEnd(); !i.IsBorderReached(); i.MovePrevious())
            {
                if (i.Current.IsPointInside(clickLocation))
                {
                    wasFigureClicked = true;
                    if (i.Current.IsSelected())
                    {
                        for (var k = _figures.GetPointerOnBeginning(); !k.IsBorderReached(); k.MoveNext())
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
                        TryUnselectAll();
                        i.Current.Select();
                    }

                    break;
                }
            }

            if (wasFigureClicked || TryUnselectAll())
            {
                NeedUpdate?.Invoke();
            }
        }

        public void ResizeSelectedFigures(int changeSizeK, ResizeAction resizeAction)
        {
            bool wasSomethingResized = false;
            for (var i = _figures.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
            {
                if (i.Current.IsSelected() && i.Current.TryResize(changeSizeK, resizeAction, _backgroundSize))
                {
                    wasSomethingResized = true;
                }
            }

            if (wasSomethingResized)
            {
                NeedUpdate?.Invoke();
            }
        }

        public void MoveSelectedFigures(Point moveVector)
        {
            bool wasSomethingMoved = false;
            for (var i = _figures.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
            {
                if (i.Current.IsSelected() && i.Current.TryMove(moveVector, _backgroundSize))
                {
                    wasSomethingMoved = true;
                }
            }

            if (wasSomethingMoved)
            {
                NeedUpdate?.Invoke();
            }
        }

        public void DeleteAllSelected()
        {
            bool wasFigureDeleted = false;
            for (var i = _figures.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
            {
                if (i.Current.IsSelected())
                {
                    wasFigureDeleted = true;
                    _figures.RemovePointerElement(i);
                }
            }

            if (wasFigureDeleted)
            {
                NeedUpdate?.Invoke();
            }
        }

        private bool TryUnselectAll()
        {
            bool wasSomethingUnselected = false;
            for (var i = _figures.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
            {
                if (i.Current.IsSelected())
                {
                    wasSomethingUnselected = true;
                    i.Current.Unselect();
                }
            }

            return wasSomethingUnselected;
        }
    }
}