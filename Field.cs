using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace FormWithButton
{
    public class Field : FlowLayoutPanel
    {
        public Label label;
        public TextBox text_box;

        public Field(string label_text)
            : base()
        {
            AutoSize = true;

            label = new Label();
            label.Text = label_text.PadRight(40,' ');
            label.Anchor = AnchorStyles.Right;
            label.TextAlign = ContentAlignment.MiddleLeft;

            Controls.Add(label);

            text_box = new TextBox();

            Controls.Add(text_box);
        }
    }

}