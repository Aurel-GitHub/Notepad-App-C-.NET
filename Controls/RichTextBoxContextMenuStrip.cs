using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepad.Controls
{
    public class RichTextBoxContextMenuStrip : ContextMenuStrip
    {
        private const string NAME = "RichTextBoxContextMenuStrip";
        private RichTextBox _richtextBox;

        public RichTextBoxContextMenuStrip(RichTextBox richTextBox)
        {
            _richtextBox = richTextBox;
            
            var cut = new ToolStripMenuItem("Couper", null, null, Keys.Control | Keys.X);
            var copy = new ToolStripMenuItem("Copier", null, null, Keys.Control | Keys.C);
            var paste = new ToolStripMenuItem("Coller", null, null, Keys.Control | Keys.V); 
            var selectAll= new ToolStripMenuItem("Selectionner tout ", null, null, Keys.Control | Keys.A);

            cut.Click += (s, e) => _richtextBox.Cut();
            copy.Click += (s, e) => _richtextBox.Copy();
            paste.Click += (s, e) => _richtextBox.Paste();
            selectAll.Click += (s, e) => _richtextBox.SelectAll();



            Items.AddRange(new ToolStripItem[] { cut, copy, paste, selectAll });
        }
    }
}
