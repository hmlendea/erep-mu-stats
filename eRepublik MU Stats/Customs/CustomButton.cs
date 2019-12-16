using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace eRepublik_MU_Stats
{
    class CustomButton : Button
    {
        public Color ShadowColor { get; set; }
        public string ButtonName;
        public bool Selected;
        public bool Clicked;

        public CustomButton()
        {
            InitializeEvents();

            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.Selectable, false);

            Font = new Font("Arial", (int)(Height * 0.35), Font.Style);
            ForeColor = Color.White;
            BackColor = Color.Cyan;
            ShadowColor = Color.Black;
        }

        private void InitializeEvents()
        {
            base.MouseEnter += new System.EventHandler(this.base_MouseEnter);
            base.MouseLeave += new System.EventHandler(this.base_MouseLeave);
            base.MouseDown += new System.Windows.Forms.MouseEventHandler(this.base_MouseDown);
            base.MouseUp += new System.Windows.Forms.MouseEventHandler(this.base_MouseUp);
        }
        protected override void OnPaint(PaintEventArgs p)
        {
            Image img;
            Font f = new Font(Font.FontFamily, Height / 3, Font.Style);
            Brush fb = new SolidBrush(ForeColor);
            Graphics g = p.Graphics;
            Rectangle r = new Rectangle(0, 0, Width, Height);
            string pathButtons = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Data\\GFX\\Panels\\Buttons\\";

            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            if (Selected == false)
                //if (Clicked == false)
                img = Properties.Resources.Button_Idle;
            //else
            //img = Properties.Resources.Button_Clicked;
            else
            {
                img = Properties.Resources.Button_Selected;
                f = new Font(f.FontFamily, f.Size + 1, f.Style | FontStyle.Underline);
            }
            if (Enabled == false)
                fb = Brushes.DimGray;

                g.FillRectangle(new SolidBrush(BackColor), r);
                g.DrawImage(img, r);

                // Base
                g.DrawImage(img, new Rectangle(0, 0, Width, Height), new Rectangle(img.Width / 2, img.Height / 2, img.Width / 4, 1), GraphicsUnit.Pixel);
                g.DrawImage(img, new Rectangle(0, 0, Width, img.Height / 2), new Rectangle(img.Width / 2, 0, 1, img.Height / 2), GraphicsUnit.Pixel);
                g.DrawImage(img, new Rectangle(0, Height - img.Height / 2, Width, img.Height / 2), new Rectangle(img.Width / 2, img.Height / 2, 1, img.Height / 2), GraphicsUnit.Pixel);

                // Left Side
                g.DrawImage(img, new Rectangle(0, 0, img.Width / 2, img.Height / 2), new Rectangle(0, 0, img.Width / 2, img.Height / 2), GraphicsUnit.Pixel);
                g.DrawImage(img, new Rectangle(0, img.Height / 2, img.Width / 2, Height - img.Height), new Rectangle(0, img.Height / 2, img.Width / 2, 1), GraphicsUnit.Pixel);
                g.DrawImage(img, new Rectangle(0, Height - img.Height / 2, img.Width / 2, img.Height / 2), new Rectangle(0, img.Height / 2, img.Width / 2, img.Height / 2), GraphicsUnit.Pixel);

                // Right Side
                g.DrawImage(img, new Rectangle(Width - img.Width / 2, 0, img.Width / 2, img.Height / 2), new Rectangle(img.Width / 2, 0, img.Width / 2, img.Height / 2), GraphicsUnit.Pixel);
                g.DrawImage(img, new Rectangle(Width - img.Width / 2, img.Height / 2, img.Width / 2, Height - img.Height), new Rectangle(img.Width / 2, img.Height / 2, img.Width / 2, 1), GraphicsUnit.Pixel);
                g.DrawImage(img, new Rectangle(Width - img.Width / 2, Height - img.Height / 2, img.Width / 2, img.Height / 2), new Rectangle(img.Width / 2, img.Height / 2, img.Width / 2, img.Height / 2), GraphicsUnit.Pixel);

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            if (ShadowColor != Color.Transparent)
                g.DrawString(Text, f, new SolidBrush(Color.Black),
                    new Rectangle(1, 1, Width, Height), sf);
            g.DrawString(Text, f, fb, r, new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            });
        }

        #region Events
        private void base_MouseEnter(object sender, EventArgs e)
        {
            Selected = true;
        }
        private void base_MouseLeave(object sender, EventArgs e)
        {
            Selected = false;
        }
        private void base_MouseDown(object sender, MouseEventArgs e)
        {
            Clicked = true;
        }
        private void base_MouseUp(object sender, MouseEventArgs e)
        {
            Clicked = false;
        }
        #endregion
    }

    #region Buttons
    class MenuButton : CustomButton
    {
        public MenuButton()
        {
            ButtonName = "Menu";
            Size = new Size(256, 48);
        }
    }
    class MenuButtonSmall : CustomButton
    {
        public MenuButtonSmall()
        {
            ButtonName = "MenuSmall";
            Size = new Size(80, 24);
        }
    }
    class Square36Button : CustomButton
    {
        public Square36Button()
        {
            ButtonName = "Square36";
            Size = new Size(36, 36);
        }
    }
    #endregion
}
