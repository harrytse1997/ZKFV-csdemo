using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace NewButton
{
    class FlatButton : Button
    {
        public FlatButton()
        {
            BackColor = Color.RoyalBlue;
            ForeColor = Color.White;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            pevent.Graphics.FillRectangle(new SolidBrush(BackColor), 0, 0, this.Width, this.Height);
            TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
            TextRenderer.DrawText(pevent.Graphics, Text, Font, new Point(Width + 3, this.Height / 2), ForeColor, flags);
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            BackColor = Color.LightSkyBlue;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            BackColor = Color.DodgerBlue;
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            BackColor = Color.DodgerBlue;
        }
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            BackColor = Color.LightSkyBlue;
        }

        private Color onMouseHoverBackcolor = Color.LightSkyBlue;
        public Color OnMouseHoverBackcolor
        {
            get { return onMouseHoverBackcolor; }
            set { onMouseHoverBackcolor = value; }
        }
    }
}
