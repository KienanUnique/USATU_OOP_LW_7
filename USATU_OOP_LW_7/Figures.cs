using System.Drawing;

namespace USATU_OOP_LW_7
{
    public enum Figures
    {
        None,
        Circle,
        Triangle,
        Square,
        Pentagon
    }

    public static class SelectionBorder
    {
        private const int SelectionBorderWidth = 5;
        private static readonly Color SelectionColor = Color.Black;
        private static readonly float[] DashValues = {1, 1};
        private static readonly Pen BorderPen;

        static SelectionBorder()
        {
            BorderPen = new Pen(SelectionColor, SelectionBorderWidth);
            BorderPen.DashPattern = DashValues;
        }

        public static void DrawSelectionBorder(Graphics graphics, Rectangle figureRectangle)
        {
            graphics.DrawRectangle(BorderPen, figureRectangle);
        }
    }

    public abstract class Figure : GraphicObject
    {
        protected Rectangle FigureRectangle;
        protected readonly SolidBrush CurrentBrush;

        private readonly Size _defaultSize = new Size(50, 50);
        private readonly Size _minimumSize = new Size(10, 10);

        protected Figure(Color color, Point centerLocation)
        {
            var leftTopPoint = new Point(centerLocation.X - _defaultSize.Width / 2,
                centerLocation.Y - _defaultSize.Height / 2);
            FigureRectangle = new Rectangle(leftTopPoint, _defaultSize);
            CurrentBrush = new SolidBrush(color);
        }

        public override bool IsFigureOutside(Size backgroundSize)
        {
            return IsFigureOutside(FigureRectangle, backgroundSize);
        }

        public override void Color(Color newColor) => CurrentBrush.Color = newColor;

        private Rectangle GetResizedFigureRectangle(int sizeK, ResizeAction resizeAction)
        {
            var newFigureRectangle = new Rectangle();
            switch (resizeAction)
            {
                case ResizeAction.Increase:
                    newFigureRectangle = new Rectangle(FigureRectangle.Location,
                        new Size(FigureRectangle.Size.Width * sizeK, FigureRectangle.Size.Height * sizeK));
                    break;
                case ResizeAction.Decrease:
                    newFigureRectangle = new Rectangle(FigureRectangle.Location,
                        new Size(FigureRectangle.Size.Width / sizeK, FigureRectangle.Size.Height / sizeK));
                    break;
            }

            return newFigureRectangle;
        }

        public override bool IsResizePossible(int sizeK, ResizeAction resizeAction, Size backgroundSize)
        {
            var newFigureRectangle = GetResizedFigureRectangle(sizeK, resizeAction);
            if (IsFigureOutside(newFigureRectangle, backgroundSize) ||
                newFigureRectangle.Size.Height < _minimumSize.Height ||
                newFigureRectangle.Size.Width < _minimumSize.Width) return false;
            return true;
        }

        public override void Resize(int sizeK, ResizeAction resizeAction)
        {
            FigureRectangle = GetResizedFigureRectangle(sizeK, resizeAction);
        }

        private Rectangle GetMovedFigureRectangle(Point moveVector)
        {
            return new Rectangle(
                new Point(FigureRectangle.Location.X + moveVector.X, FigureRectangle.Location.Y + moveVector.Y),
                FigureRectangle.Size);
        }

        public override bool IsMovePossible(Point moveVector, Size backgroundSize)
        {
            return !(IsFigureOutside(GetMovedFigureRectangle(moveVector), backgroundSize));
        }

        public override void Move(Point moveVector)
        {
            FigureRectangle = GetMovedFigureRectangle(moveVector);
        }

        public override void DrawOnGraphics(Graphics graphics)
        {
            DrawFigureOnGraphics(graphics);
            if (_isSelected)
            {
                DrawSelectionBorders(graphics);
            }
        }

        public override bool IsSelected()
        {
            return _isSelected;
        }

        public override void Select()
        {
            _isSelected = true;
        }

        public override void Unselect()
        {
            _isSelected = false;
        }

        public override void ProcessClick()
        {
            _isSelected = !_isSelected;
        }

        protected abstract void DrawFigureOnGraphics(Graphics graphics);

        protected static bool IsUnderLine(Point firstLinePoint, Point secondLinePoint, Point checkPoint)
        {
            return (checkPoint.X - firstLinePoint.X) * (secondLinePoint.Y - firstLinePoint.Y) -
                (checkPoint.Y - firstLinePoint.Y) * (secondLinePoint.X - firstLinePoint.X) <= 0;
        }

        private void DrawSelectionBorders(Graphics graphics)
        {
            SelectionBorder.DrawSelectionBorder(graphics, FigureRectangle);
        }

        private static bool IsFigureOutside(Rectangle figureRectangle, Size backgroundSize)
        {
            return 0 > figureRectangle.Left || figureRectangle.Right > backgroundSize.Width ||
                   backgroundSize.Height < figureRectangle.Bottom || figureRectangle.Top < 0;
        }

        public override bool IsGroup()
        {
            return false;
        }
    }

    public class Circle : Figure
    {
        public Circle(Color color, Point location) : base(color, location)
        {
        }

        public override bool IsPointInside(Point pointToCheck)
        {
            var circleRadius = FigureRectangle.Width / 2;
            var tmpX = pointToCheck.X - FigureRectangle.Location.X - circleRadius;
            var tmpY = pointToCheck.Y - FigureRectangle.Location.Y - circleRadius;
            return tmpX * tmpX + tmpY * tmpY <= circleRadius * circleRadius;
        }

        protected override void DrawFigureOnGraphics(Graphics graphics)
        {
            graphics.FillEllipse(CurrentBrush, FigureRectangle);
        }
    }

    public class Square : Figure
    {
        public Square(Color color, Point location) : base(color, location)
        {
        }

        public override bool IsPointInside(Point pointToCheck)
        {
            return FigureRectangle.Contains(pointToCheck);
        }

        protected override void DrawFigureOnGraphics(Graphics graphics)
        {
            graphics.FillRectangle(CurrentBrush, FigureRectangle);
        }
    }

    public class Triangle : Figure
    {
        public Triangle(Color color, Point location) : base(color, location)
        {
        }

        public override bool IsPointInside(Point pointToCheck)
        {
            return (FigureRectangle.Contains(pointToCheck) && IsUnderLine(FigureRectangle.Location,
                new Point(FigureRectangle.Location.X + FigureRectangle.Width,
                    FigureRectangle.Location.Y + FigureRectangle.Height), pointToCheck));
        }

        protected override void DrawFigureOnGraphics(Graphics graphics)
        {
            Point[] points =
            {
                FigureRectangle.Location, new Point(FigureRectangle.Left, FigureRectangle.Bottom),
                new Point(FigureRectangle.Right, FigureRectangle.Bottom)
            };
            graphics.FillPolygon(CurrentBrush, points);
        }
    }

    public class Pentagon : Figure
    {
        public Pentagon(Color color, Point location) : base(color, location)
        {
        }

        public override bool IsPointInside(Point pointToCheck)
        {
            var isUnderLeftCornerLine = IsUnderLine(
                new Point(FigureRectangle.Left, FigureRectangle.Bottom - FigureRectangle.Height / 2),
                new Point(FigureRectangle.Right - FigureRectangle.Width / 2, FigureRectangle.Top), pointToCheck);
            var isUnderRightCornerLine = IsUnderLine(
                new Point(FigureRectangle.Right - FigureRectangle.Width / 2, FigureRectangle.Top),
                new Point(FigureRectangle.Right, FigureRectangle.Bottom - FigureRectangle.Height / 2), pointToCheck);
            return FigureRectangle.Contains(pointToCheck) && isUnderLeftCornerLine && isUnderRightCornerLine;
        }

        protected override void DrawFigureOnGraphics(Graphics graphics)
        {
            Point[] points =
            {
                new Point(FigureRectangle.Left, FigureRectangle.Bottom - FigureRectangle.Height / 2),
                new Point(FigureRectangle.Right - FigureRectangle.Width / 2, FigureRectangle.Top),
                new Point(FigureRectangle.Right, FigureRectangle.Bottom - FigureRectangle.Height / 2),
                new Point(FigureRectangle.Right, FigureRectangle.Bottom),
                new Point(FigureRectangle.Left, FigureRectangle.Bottom),
            };
            graphics.FillPolygon(CurrentBrush, points);
        }
    }
}