
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace UserInterface.Forms
{
    public partial class ImagePreviewer : Form
    {
        public Image ItemImage { get; set; }

        public ImagePreviewer()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ImagePreviewer_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = ItemImage;
            CenterImage();
            //Invalidate(DisplayRectangle);
            //Update();

            pictureBox1.Visible = true;
            btnClose.Visible = true;
        }

        private void CenterImage()
        {
            int x = (Size.Width - pictureBox1.Size.Width) / 2;
            int y = (Size.Height - pictureBox1.Size.Height) / 2;
            Point p = new Point(x, y);

            pictureBox1.Location = p;
            //p.Offset(- btnClose.Size.Width, - btnClose.Size.Height);
            p.Offset(pictureBox1.Size.Width - btnClose.Size.Width / 2, -btnClose.Size.Height / 2);
            btnClose.Location = p;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            SolidBrush sb = new SolidBrush(Color.FromArgb(90, 0, 0, 0));
            e.Graphics.FillRectangle(sb, DisplayRectangle);


        }

        private void ImagePreviewer_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}