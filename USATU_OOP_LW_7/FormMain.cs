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
        private readonly GraphicObjectsHandler _graphicObjectGroup;
        private bool _wasControlAlreadyPressed;

        public FormMain()
        {
            InitializeComponent();
            _graphicObjectGroup = new GraphicObjectsHandler(panelForDrawing.DisplayRectangle.Size);
            this.KeyPreview = true;

            colorDialog.Color = _startColor;
            controlCurrentColor.BackColor = _startColor;
        }

        private void panelForDrawing_Paint(object sender, PaintEventArgs e)
        {
            _graphicObjectGroup.DrawOnGraphics(e.Graphics);
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
                if (!_graphicObjectGroup.TryProcessSelectionClick(e.Location))
                {
                    _graphicObjectGroup.AddFigure(GetSelectedFigureEnum(), colorDialog.Color, e.Location);
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                _graphicObjectGroup.ProcessColorClick(e.Location, colorDialog.Color);
            }

            panelForDrawing_Update();
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.ControlKey when !_wasControlAlreadyPressed:
                    _graphicObjectGroup.EnableMultipleSelection();
                    _wasControlAlreadyPressed = true;
                    break;
                case Keys.W:
                    _graphicObjectGroup.MoveSelectedFigures(new Point(0, -1 * MoveLength));
                    panelForDrawing_Update();
                    break;
                case Keys.S:
                    _graphicObjectGroup.MoveSelectedFigures(new Point(0, MoveLength));
                    panelForDrawing_Update();
                    break;
                case Keys.A:
                    _graphicObjectGroup.MoveSelectedFigures(new Point(-1 * MoveLength, 0));
                    panelForDrawing_Update();
                    break;
                case Keys.D:
                    _graphicObjectGroup.MoveSelectedFigures(new Point(MoveLength, 0));
                    panelForDrawing_Update();
                    break;
            }
        }

        private void FormMain_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.ControlKey:
                    _graphicObjectGroup.DisableMultipleSelection();
                    _wasControlAlreadyPressed = false;
                    break;
                case Keys.Delete:
                    _graphicObjectGroup.DeleteAllSelected();
                    panelForDrawing_Update();
                    break;
                case Keys.Oemplus:
                    _graphicObjectGroup.ResizeSelectedFigures(ChangeSizeK, ResizeAction.Increase);
                    panelForDrawing_Update();
                    break;
                case Keys.OemMinus:
                    _graphicObjectGroup.ResizeSelectedFigures(ChangeSizeK, ResizeAction.Decrease);
                    panelForDrawing_Update();
                    break;
                case Keys.J:
                    _graphicObjectGroup.JoinSelectedGraphicObject();
                    panelForDrawing_Update();
                    break;
                case Keys.U:
                    _graphicObjectGroup.SeparateSelectedGraphicObjects();
                    panelForDrawing_Update();
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

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            _graphicObjectGroup.StoreData();
        }
    }
}