using System;
using System.Drawing;
using System.Windows.Forms;

namespace USATU_OOP_LW_7
{
    public partial class FormMain : Form
    {
        private const int ChangeSizeK = 2;
        private const int MoveLength = 10;
        private readonly Color _startColor = Color.Coral;
        private readonly FiguresHandler _figuresHandler;
        private bool _wasControlAlreadyPressed;

        public FormMain()
        {
            InitializeComponent();
            _figuresHandler = new FiguresHandler(panelForDrawing.DisplayRectangle.Size);
            this.KeyPreview = true;

            colorDialog.Color = _startColor;
            controlCurrentColor.BackColor = _startColor;

            _figuresHandler.NeedUpdate += panelForDrawing_Update;
        }

        private void panelForDrawing_Paint(object sender, PaintEventArgs e)
        {
            _figuresHandler.DrawAllCirclesOnGraphics(e.Graphics);
        }

        private void panelForDrawing_Update()
        {
            panelForDrawing.Invalidate();
        }

        private Figures GetSelectedFigureEnum()
        {
            if (radioButtonCircle.Checked)
            {
                return Figures.Circle;
            }
            else if (radioButtonTriangle.Checked)
            {
                return Figures.Triangle;
            }
            else if (radioButtonSquare.Checked)
            {
                return Figures.Square;
            }
            else if (radioButtonPentagon.Checked)
            {
                return Figures.Pentagon;
            }

            return Figures.None;
        }

        private void panelForDrawing_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!_figuresHandler.TryProcessSelectionClick(e.Location))
                {
                    _figuresHandler.AddFigure(GetSelectedFigureEnum(), colorDialog.Color, e.Location);
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                _figuresHandler.ProcessColorClick(e.Location, colorDialog.Color);
            }
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.ControlKey when !_wasControlAlreadyPressed:
                    _figuresHandler.EnableMultipleSelection();
                    _wasControlAlreadyPressed = true;
                    break;
                case Keys.W:
                    _figuresHandler.MoveSelectedFigures(new Point(0, -1 * MoveLength));
                    break;
                case Keys.S:
                    _figuresHandler.MoveSelectedFigures(new Point(0, MoveLength));
                    break;
                case Keys.A:
                    _figuresHandler.MoveSelectedFigures(new Point(-1 * MoveLength, 0));
                    break;
                case Keys.D:
                    _figuresHandler.MoveSelectedFigures(new Point(MoveLength, 0));
                    break;
            }
        }

        private void FormMain_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.ControlKey:
                    _figuresHandler.DisableMultipleSelection();
                    _wasControlAlreadyPressed = false;
                    break;
                case Keys.Delete:
                    _figuresHandler.DeleteAllSelected();
                    break;
                case Keys.Oemplus:
                    _figuresHandler.ResizeSelectedFigures(ChangeSizeK, ResizeAction.Increase);
                    break;
                case Keys.OemMinus:
                    _figuresHandler.ResizeSelectedFigures(ChangeSizeK, ResizeAction.Decrease);
                    break;
            }
        }

        private void buttonChooseColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                controlCurrentColor.BackColor = colorDialog.Color;
            }
        }
    }
}