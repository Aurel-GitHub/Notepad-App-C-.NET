using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepad.Controls
{
    public class MainMenuStrip : MenuStrip
    {
        //Création des onglets, raccourcis

        private const string NAME = "MainMenuStrip";
        
        private FontDialog _fontDialog;

        public MainMenuStrip()
        {
            Name = NAME;    
            Dock = DockStyle.Top;

            _fontDialog = new FontDialog();
            FileDropDownMenu();
            EditDropDown();
            FormatDropDownMenu();
            ViewDropDownMenu();
        }

        public void FileDropDownMenu()
        {
            var fileDropDownMenu = new ToolStripMenuItem("Fichier");

            var newMenu = new ToolStripMenuItem("Nouveau", null, null, Keys.Control | Keys.N);
            var openMenu = new ToolStripMenuItem("Ouvrir...", null, null, Keys.Control | Keys.O);
            var saveMenu = new ToolStripMenuItem("Enrengistrer", null, null, Keys.Control | Keys.S);
            var saveAsMenu = new ToolStripMenuItem("Enrengistrer sous...", null, null, Keys.Control | Keys.Shift | Keys.S);
            var quitMenu = new ToolStripMenuItem("Quitter", null, null, Keys.Alt | Keys.F4);

            fileDropDownMenu.DropDownItems.AddRange(new ToolStripItem[] { newMenu, openMenu, saveMenu, saveAsMenu, quitMenu });

            Items.Add(fileDropDownMenu);
        }

        public void EditDropDown()
        {
            var editDropDown = new ToolStripMenuItem("Edition");

            var undo = new ToolStripMenuItem("Annuler", null, null, Keys.Control | Keys.Z);
            var redo = new ToolStripMenuItem("Restaurer", null, null, Keys.Control | Keys.Y);
            //ici le copier coller sont rajoutés en plus
            var copy = new ToolStripMenuItem("Copier", null, null, Keys.Control | Keys.C);
            var paste = new ToolStripMenuItem("Restaurer", null, null, Keys.Control | Keys.V);

            undo.Click += (s, e) =>
            {
                if (MainForm.RichTextBox.CanUndo)
                {
                    MainForm.RichTextBox.Undo();
                };    
            };

            redo.Click += (s, e) =>
            {
                if (MainForm.RichTextBox.CanRedo)
                {
                    MainForm.RichTextBox.Redo();
                };
            };

            editDropDown.DropDownItems.AddRange(new ToolStripItem[] { undo, redo, copy, paste });

            Items.Add(editDropDown);
        }

        public void FormatDropDownMenu()
        {
            var formatDropDown = new ToolStripMenuItem("Affichage");

            var font = new ToolStripMenuItem("Police...");

            font.Click += (s, e) =>
            {
                _fontDialog.Font = MainForm.RichTextBox.Font;
                _fontDialog.ShowDialog();

                MainForm.RichTextBox.Font = _fontDialog.Font;
            };


            formatDropDown.DropDownItems.AddRange(new ToolStripItem[] { font});

            Items.Add(formatDropDown);

        }

        public void ViewDropDownMenu()
        {
            var viewDropDown = new ToolStripMenuItem("Affichage");
            var alwaysOnTop = new ToolStripMenuItem("Toujours Devant");
                
            var zoomDropDown = new ToolStripMenuItem("Zoom");
            var zoomIn = new ToolStripMenuItem("Zoom avant", null, null, Keys.Control | Keys.Add);
            var zoomOut = new ToolStripMenuItem("Zoom arrière", null, null, Keys.Control | Keys.Subtract);
            var restoreZoom = new ToolStripMenuItem("Restaurer le Zoom par défaut", null, null, Keys.Control | Keys.Divide);

            zoomIn.ShortcutKeyDisplayString = "Ctrl+Num +";
            zoomOut.ShortcutKeyDisplayString = "Ctrl+Num -";
            restoreZoom.ShortcutKeyDisplayString = "Ctrl+Num /";

            alwaysOnTop.Click += (s, e) =>
            {
                if (alwaysOnTop.Checked)
                {
                    alwaysOnTop.Checked = false;
                    Program.MainForm.TopMost = false;
                }
                else
                {
                    alwaysOnTop.Checked = true;
                    Program.MainForm.TopMost = true;

                }
            };

            zoomIn.Click += (s, e) =>
            {
                if(MainForm.RichTextBox.ZoomFactor < 3)
                {
                    MainForm.RichTextBox.ZoomFactor += 0.3F;
                }
            };

            zoomOut.Click += (s, e) =>
            {
                if (MainForm.RichTextBox.ZoomFactor > 0.3)
                {
                    MainForm.RichTextBox.ZoomFactor -= 0.3F;
                }
            };

            restoreZoom.Click += (s, e) =>
            {
                MainForm.RichTextBox.ZoomFactor = 1F;
            };  

            zoomDropDown.DropDownItems.AddRange(new ToolStripItem[] { zoomIn, zoomOut, restoreZoom });
       
            viewDropDown.DropDownItems.AddRange(new ToolStripItem[] { alwaysOnTop, zoomDropDown  });

            Items.Add(viewDropDown);
        }

        private void AlwaysOnTop_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
