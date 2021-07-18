using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;

namespace MultiColumnCombcs
{
    public partial class MultiColumnCombocs : ComboBox
    {
        // Hide some properties
        [Browsable(false)]
        public new bool IntegralHeight { get; set; }

        [Browsable(false)]
        public new DrawMode DrawMode { get; set; }

        [Browsable(false)]
        public new int DropDownHeight { get; set; }

        [Browsable(false)]
        public new ComboBoxStyle DropDownStyle { get; set; }

        [Browsable(false)]
        public new bool DoubleBuffered { get; set; }

        public Boolean paintHandled = false;
        public const int WM_PAINT = 0xF;
        public int intScreenMagnification = 125; // Screen Magnification = 125%
                                                 // Dropdown
        private string _columnWidths;
        private string[] columnWidthsArray;

        // 'Combo Box
        private Color _buttonColor = Color.Gainsboro;
        private Color _borderColor = Color.Gainsboro;
        private readonly Color bgSelectedColor = Color.PaleGreen;
        private readonly Color textselectedcolor = Color.Red;
        private readonly Color bgColor = Color.White;
        private readonly Color lineColor = Color.White;

        private Brush backgroundBrush = new SolidBrush(SystemColors.ControlText);
        private Brush arrowBrush = new SolidBrush(Color.Black);
        private Brush ButtonBrush = new SolidBrush(Color.Gainsboro);

        //Properties
        [Browsable(true)]
        public Color ButtonColor
        {
            get
            {
                return _buttonColor;
            }
            set
            {
                _buttonColor = value;
                ButtonBrush = new SolidBrush(this.ButtonColor);
                this.Invalidate();
            }
        }

        [Browsable(true)]
        public Color BorderColor
        {
            get
            {
                return _borderColor;
            }
            set
            {
                _borderColor = value;
                this.Invalidate();
            }
        }
        //Column Widths to be set in Properties Window as string containing width of each column in Pixels, 
        // delimited by ';' eg 15;45;40;100,50;40 for six columns

        [Browsable(true)]
        public string ColumnWidths
        {
            get
            {
                if (string.IsNullOrEmpty(_columnWidths))
                {
                    _columnWidths = "15";  //default value
                }

                return _columnWidths;
            }
            set
            {
                _columnWidths = value;
                // split Column Widths string into Array of substrings delimited by ';' character
                columnWidthsArray = _columnWidths.Split(System.Convert.ToChar(";"));
                int w = 0;
                foreach (string str in columnWidthsArray)
                    w += System.Convert.ToInt32(System.Convert.ToInt32(str) * intScreenMagnification / 100);// ******
                DropDownWidth = (w + 20);
            }
        }

        // Constructor stuff
        public MultiColumnCombocs() : base()
        {
            base.IntegralHeight = false;
            base.DrawMode = DrawMode.OwnerDrawFixed;
            base.DropDownStyle = ComboBoxStyle.DropDown;
            MaxDropDownItems = 12;
            // Minimise flicker in painted control
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void WndProc(ref Message m)  // Listen for operating system messages
        {
            base.WndProc(ref m);  /*Inheriting controls should call the base class's WndProc(Message) method
                                to process any messages that they do not handle.*/
            switch (m.Msg)
            {
                case WM_PAINT:
                    // Draw Combobox and dropdown arrow
                    Graphics g = this.CreateGraphics();
                    // Background - Only the borders will show up because the edit box will be overlayed
                    try
                    {
                        backgroundBrush = new SolidBrush(Color.White);
                        g.FillRectangle(backgroundBrush, 0, 0, Size.Width, Size.Height);
                        // Border
                        Rectangle rectangle = new Rectangle();
                        Pen pen = new Pen(BorderColor, 2);
                        rectangle.Size = new Size(Width - 2, Height);
                        g.DrawRectangle(pen, rectangle);

                        // Background of the dropdown button
                        ButtonBrush = new SolidBrush(ButtonColor);
                        Rectangle rect = new Rectangle(Width - 15, 0, 15, Height);
                        g.FillRectangle(ButtonBrush, rect);

                        // Create the path for the arrow
                        g.SmoothingMode = SmoothingMode.AntiAlias;

                        GraphicsPath pth = new GraphicsPath();
                        PointF TopLeft = new PointF(Width - 12, System.Convert.ToSingle((Height - 5) / 2));
                        PointF TopRight = new PointF(Width - 5, System.Convert.ToSingle((Height - 5) / 2));
                        PointF Bottom = new PointF(Width - 8, System.Convert.ToSingle((Height + 4) / 2));
                        pth.AddLine(TopLeft, TopRight);
                        pth.AddLine(TopRight, Bottom);

                        // Determine the arrow and button's color.
                        arrowBrush = new SolidBrush(Color.Black);

                        if (this.DroppedDown)
                        {
                            arrowBrush = new SolidBrush(Color.Red);
                            ButtonBrush = new SolidBrush(Color.PaleGreen);
                        }
                        // Draw the arrow
                        g.FillRectangle(ButtonBrush, rect);
                        g.FillPath(arrowBrush, pth);

                        pen.Dispose();
                        pth.Dispose();

                    }
                    finally
                    {
                        // Cleanup
                        g.Dispose();
                        arrowBrush.Dispose();
                        backgroundBrush.Dispose();
                    }
                    break;

                default:
                    {
                        break;
                    }
            }
        }

        protected override void OnDrawItem(System.Windows.Forms.DrawItemEventArgs e)
        {
            // Draw Dropdown with Multicolumns
            Cursor = Cursors.Arrow;
            DataRowView row = (DataRowView)base.Items[e.Index];

            int newpos = e.Bounds.X;
            int endpos = e.Bounds.X;
            int intColumnIndex = 0;

            // Draw the current item text based on the current Font and the custom brush settings
            foreach (string str in columnWidthsArray)
            {
                // paint each column, "intColumnIndex" is local integer
                string strColWidth = columnWidthsArray[intColumnIndex];
                int ColLength = System.Convert.ToInt32(strColWidth);
                // Adjust ColLength
                ColLength = System.Convert.ToInt32(ColLength * intScreenMagnification / 100); // ******
                endpos += ColLength;

                string strColumnText = row[intColumnIndex].ToString();

                if (IsDate(strColumnText))  //Format Date as 'dd-MM-yy' (not avail as 'ToString("Format")'
                {
                    strColumnText = strColumnText.Replace("/", "-");
                    string strSaveColumn = strColumnText;
                    strColumnText = strSaveColumn.Substring(0, 6) + strSaveColumn.Substring(8, 2);
                    ColLength = 40;
                }

                // Paint Text
                if (ColLength > 0)
                {
                    RectangleF r = new RectangleF(newpos + 1, e.Bounds.Y, endpos - 1, e.Bounds.Height);
                    // Colours of normal row and text

                    //   Colours of normal row and text
                    SolidBrush textBrush = new SolidBrush(Color.Black);
                    SolidBrush backbrush = new SolidBrush(Color.White);
                    StringFormat strFormat = new StringFormat();
                    try
                    {
                        // Colours of selected row and text
                        if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                        {
                            textBrush.Color = textselectedcolor; // Red
                            backbrush.Color = bgSelectedColor; // Pale Green
                        }
                        e.Graphics.FillRectangle(backbrush, r);

                        strFormat.Trimming = StringTrimming.Character;
                        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                        e.Graphics.DrawString(strColumnText, e.Font, textBrush, r, strFormat);
                        e.Graphics.SmoothingMode = SmoothingMode.None;
                    }
                    finally
                    {
                        backbrush.Dispose();
                        strFormat.Dispose();
                        textBrush.Dispose();
                    }

                    //Separate columns with white border
                    if (intColumnIndex > 0 && intColumnIndex <= (columnWidthsArray.Length))
                    {
                        e.Graphics.DrawLine(new Pen(Color.White), endpos, e.Bounds.Y, endpos, ItemHeight * MaxDropDownItems);
                    }
                    newpos = endpos;
                    intColumnIndex++;
                } // end if
            }// end for

            // load ValueMember value into combobox when using mouse on dropped down list
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                string selectedItem = SelectedValue.ToString();
                base.Text = selectedItem.ToString();
            }
        } //end sub

        private bool IsDate(string strColumnText)
        {
            DateTime dateValue;

            if (DateTime.TryParse(strColumnText, out dateValue))
            {
                return true;
            }
            else
            {
                return false;
            }
        } // end sub

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            // Overrides Sub of same name in parent class
            HandledMouseEventArgs MWheel = (HandledMouseEventArgs)e;
            // HandledMouseEventArgs prevents event being sent to parent container
            if (!this.DroppedDown)
            {
                MWheel.Handled = true;
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (!this.DroppedDown & e.KeyCode == Keys.Down)
                this.DroppedDown = true;
            else if (e.KeyCode == Keys.Escape)
            {
                this.DroppedDown = false;
                this.SelectedIndex = -1;
                this.Text = null;
            }
        }


    }
}