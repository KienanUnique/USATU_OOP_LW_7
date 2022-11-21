using System.Drawing;
using CustomDoublyLinkedListLibrary;

namespace USATU_OOP_LW_7
{
    public class GraphicObjectGroup : GraphicObject
    {
        private readonly CustomDoublyLinkedList<GraphicObject> _graphicObjects = new();

        public GraphicObjectGroup()
        {
            _isSelected = false;
        }

        public override bool IsFigureOutside(Size backgroundSize)
        {
            for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
            {
                if (i.Current.IsFigureOutside(backgroundSize))
                {
                    return true;
                }
            }

            return false;
        }

        public override void Color(Color newColor)
        {
            for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
            {
                i.Current.Color(newColor);
            }
        }

        public override bool IsResizePossible(int sizeK, ResizeAction resizeAction, Size backgroundSize)
        {
            for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
            {
                if (!i.Current.IsResizePossible(sizeK, resizeAction, backgroundSize))
                {
                    return false;
                }
            }

            return true;
        }

        public override void Resize(int sizeK, ResizeAction resizeAction)
        {
            for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
            {
                i.Current.Resize(sizeK, resizeAction);
            }
        }

        public override bool IsMovePossible(Point moveVector, Size backgroundSize)
        {
            for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
            {
                if (!i.Current.IsMovePossible(moveVector, backgroundSize))
                {
                    return false;
                }
            }

            return true;
        }

        public override void Move(Point moveVector)
        {
            for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
            {
                i.Current.Move(moveVector);
            }
        }

        public override void DrawOnGraphics(Graphics graphics)
        {
            for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
            {
                i.Current.DrawOnGraphics(graphics);
            }
        }

        public override bool IsSelected()
        {
            return _isSelected;
        }

        public override void Select()
        {
            _isSelected = true;
            ChangeAllSelection(_isSelected);
        }

        public override void Unselect()
        {
            _isSelected = false;
            ChangeAllSelection(_isSelected);
        }

        public override void ProcessClick()
        {
            _isSelected = !_isSelected;
            ChangeAllSelection(_isSelected);
        }

        public override bool IsPointInside(Point pointToCheck)
        {
            for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
            {
                if (i.Current.IsPointInside(pointToCheck))
                {
                    return true;
                }
            }

            return false;
        }

        public override bool IsGroup()
        {
            return true;
        }

        public CustomDoublyLinkedList<GraphicObject> GetAllGraphicObjects()
        {
            return _graphicObjects;
        }

        public void AddGraphicObject(GraphicObject newGraphicObject)
        {
            newGraphicObject.Unselect();
            _graphicObjects.Add(newGraphicObject);
        }

        private void ChangeAllSelection(bool newIsSelected)
        {
            for (var i = _graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
            {
                if (!newIsSelected && i.Current.IsSelected())
                {
                    i.Current.Unselect();
                }
                else if (newIsSelected && !i.Current.IsSelected())
                {
                    i.Current.Select();
                }
            }
        }
    }
}