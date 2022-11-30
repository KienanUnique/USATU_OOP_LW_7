﻿using System.Drawing;
using System.IO;
using System.Text;
using CustomDoublyLinkedListLibrary;

namespace USATU_OOP_LW_7
{
    public class GraphicObjectGroup : GraphicObject
    {
        private readonly CustomDoublyLinkedList<GraphicObject> _graphicObjects;
        private readonly GraphicObjectsListAbstractFactory _graphicObjectsListAbstractFactory;

        public GraphicObjectGroup(GraphicObjectsListAbstractFactory graphicObjectsListAbstractFactory)
        {
            _graphicObjects = new CustomDoublyLinkedList<GraphicObject>();
            _graphicObjectsListAbstractFactory = graphicObjectsListAbstractFactory;
            IsSelected = false;
        }

        public GraphicObjectGroup(StringReader stringReader,
            GraphicObjectsListAbstractFactory graphicObjectsListAbstractFactory)
        {
            _graphicObjects = graphicObjectsListAbstractFactory.ParseGraphicObjects(stringReader);
            IsSelected = false;
        }

        public GraphicObjectGroup(CustomDoublyLinkedList<GraphicObject> graphicObjects,
            GraphicObjectsListAbstractFactory graphicObjectsListAbstractFactory) =>
            (_graphicObjects, _graphicObjectsListAbstractFactory) = (graphicObjects, graphicObjectsListAbstractFactory);

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

        public override bool IsObjectSelected()
        {
            return IsSelected;
        }

        public override void Select()
        {
            IsSelected = true;
            ChangeAllSelection(IsSelected);
        }

        public override void Unselect()
        {
            IsSelected = false;
            ChangeAllSelection(IsSelected);
        }

        public override void ProcessClick()
        {
            IsSelected = !IsSelected;
            ChangeAllSelection(IsSelected);
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

        public override string PrepareDataToStore()
        {
            var dataStringBuilder = new StringBuilder();
            dataStringBuilder.AppendLine(PrefixGraphicObjectsType + GraphicObjectsTypes.Group);
            dataStringBuilder.Append(_graphicObjectsListAbstractFactory.PrepareDataToStore(_graphicObjects));
            return dataStringBuilder.ToString();
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
                if (!newIsSelected && i.Current.IsObjectSelected())
                {
                    i.Current.Unselect();
                }
                else if (newIsSelected && !i.Current.IsObjectSelected())
                {
                    i.Current.Select();
                }
            }
        }
    }
}