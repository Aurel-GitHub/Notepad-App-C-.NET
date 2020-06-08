using Notepad.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepad
{
    public partial class MainForm : Form
    {
        public static RichTextBox RichTextBox;
         
        public MainForm()
        {
            InitializeComponent();
            var menuStrip = new MainMenuStrip();
            var maintabControl = new MainTabControl();  
            RichTextBox = new CustomRichTextBox();

            maintabControl.TabPages.Add("Onglet 1");
            maintabControl.TabPages[0].Controls.Add(RichTextBox);

            Controls.AddRange( new Control[] { maintabControl, menuStrip });

        }

      
    }
}
