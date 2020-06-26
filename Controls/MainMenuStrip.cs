using System;
using Notepad.Objects;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using Notepad.Services;

namespace Notepad.Controls
{
    public class MainMenuStrip : MenuStrip
    {

        private const string NAME = "MainMenuStrip";

        private MainForm _form;
        private FontDialog _fontDialog;
        private OpenFileDialog _openFileDialog;
        private SaveFileDialog _saveFileDialog;

        public MainMenuStrip()
        {
            Name = NAME;    
            Dock = DockStyle.Top;

            _fontDialog = new FontDialog();
            _openFileDialog = new OpenFileDialog();
            _saveFileDialog = new SaveFileDialog();

            FileDropDownMenu();
            EditDropDown();
            FormatDropDownMenu();
            ViewDropDownMenu();

            HandleCreated += (s, e) =>
             {
                 _form = FindForm() as MainForm;
             };
        }

        public void FileDropDownMenu()
        { 
            var fileDropDownMenu = new ToolStripMenuItem("Fichier");

            var newFile = new ToolStripMenuItem("Nouveau", null, null, Keys.Control | Keys.N);
            var open = new ToolStripMenuItem("Ouvrir...", null, null, Keys.Control | Keys.O);
            var save = new ToolStripMenuItem("Enrengistrer", null, null, Keys.Control | Keys.S);
            var saveAs = new ToolStripMenuItem("Enrengistrer sous...", null, null, Keys.Control | Keys.Shift | Keys.S);
            var quit = new ToolStripMenuItem("Quitter", null, null, Keys.Alt | Keys.F4);

            newFile.Click += (s, e) =>
            {
                var tabControl = _form.MainTabControl;
                var tabCount = tabControl.TabCount;

                var fileName = $"Sans titre {tabCount +1}";
                var file = new TextFile(fileName);
                var rtb = new CustomRichTextBox();
                            
                tabControl.TabPages.Add(file.SafeFileName);
                    
                var newTabPage = tabControl.TabPages[tabCount];

                newTabPage.Controls.Add(rtb);
                _form.Session.Files.Add(file);
                tabControl.SelectedTab = newTabPage;

                newTabPage.Controls.Add(rtb);   
                _form.CurrentFile = file;
                _form.CurrentRtb = rtb;
            };

            open.Click +=  async (s, e) =>
            {
                if(_openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var tabControl = _form.MainTabControl;
                    var tabCount = tabControl.TabCount;

                    var file = new TextFile(_openFileDialog.FileName);

                    var rtb = new CustomRichTextBox();

                    _form.Text = $"{file.FileName} - Notepad.NET";

                    using (StreamReader reader = new StreamReader(file.FileName))
                    {
                        file.Contents = await reader.ReadToEndAsync();
                    }

                    rtb.Text = file.Contents;

                    tabControl.TabPages.Add(file.SafeFileName);
                    tabControl.TabPages[tabCount].Controls.Add(rtb);

                    _form.Session.Files.Add(file);
                    _form.CurrentRtb = rtb;
                    _form.CurrentFile = file;
                    tabControl.SelectedTab = tabControl.TabPages[tabCount];
                }
            };

            save.Click += async (s, e) =>
            {
                var currentFile = _form.CurrentFile;
                var currentRtbText = _form.CurrentRtb.Text;

                if (currentFile.Contents != currentRtbText)
                {
                    if (File.Exists(currentFile.FileName))
                    {
                        using (StreamWriter writer = File.CreateText(currentFile.FileName))
                        {
                            await writer.WriteAsync(currentFile.Contents);
                        }
                    }
                    currentFile.Contents = currentRtbText;
                    _form.Text = currentFile.FileName;
                    _form.MainTabControl.SelectedTab.Text = currentFile.SafeFileName;
                }
                else
                {
                    saveAs.PerformClick();
                }
            };

            saveAs.Click +=  async (s, e) =>
            {
                if (_saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var newFileName = _saveFileDialog.FileName;
                    var alreadyExists = false;

                    foreach (var file in _form.Session.Files)
                    {
                        if(file.FileName == newFileName)
                        {
                            MessageBox.Show("Ce fichier est déjà ouvert dans Notepad.NET", "ERREUR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            alreadyExists = true;
                            break;
                        }
                    }
                    
                    // si le fichier à enrengistrer n'existe pas.
                    if(!alreadyExists)
                    {
                        var file = new TextFile(newFileName) { Contents = _form.CurrentRtb.Text };

                        var oldFile = _form.Session.Files.Where(x => x.FileName == _form.CurrentFile.FileName).First();

                        _form.Session.Files.Replace(oldFile, file);

                        using (StreamWriter writer = File.CreateText(file.FileName))
                        {
                            await writer.WriteAsync(file.Contents);
                        }

                        _form.MainTabControl.SelectedTab.Text = file.SafeFileName;
                        _form.Text = file.FileName;
                        _form.CurrentFile = file;

                    }

                   
                }
            };

            quit.Click += (s, e) =>
            {
                Application.Exit();
            };


            fileDropDownMenu.DropDownItems.AddRange(new ToolStripItem[] { newFile, open, save, saveAs, quit });

            Items.Add(fileDropDownMenu);
        }

        public void EditDropDown()
        {
            var editDropDown = new ToolStripMenuItem("Edition");

            var undo = new ToolStripMenuItem("Annuler", null, null, Keys.Control | Keys.Z);
            var redo = new ToolStripMenuItem("Restaurer", null, null, Keys.Control | Keys.Y);
            var copy = new ToolStripMenuItem("Copier", null, null, Keys.Control | Keys.C);
            var paste = new ToolStripMenuItem("Restaurer", null, null, Keys.Control | Keys.V);

            undo.Click += (s, e) =>
            {
                if (_form.CurrentRtb.CanUndo)
                {
                    _form.CurrentRtb.Undo();
                };    
            };

            redo.Click += (s, e) =>
            {
                if (_form.CurrentRtb.CanRedo)
                {
                    _form.CurrentRtb.Redo();
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
                _fontDialog.Font = _form.CurrentRtb.Font;
                _fontDialog.ShowDialog();

                _form.CurrentRtb.Font = _fontDialog.Font;
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
                if(_form.CurrentRtb.ZoomFactor < 3)
                {
                    _form.CurrentRtb.ZoomFactor += 0.3F;
                }
            };

            zoomOut.Click += (s, e) =>
            {
                if (_form.CurrentRtb.ZoomFactor > 0.3)
                {
                    _form.CurrentRtb.ZoomFactor -= 0.3F;
                }
            };

            restoreZoom.Click += (s, e) =>
            {
                _form.CurrentRtb.ZoomFactor = 1F;
            };  

            zoomDropDown.DropDownItems.AddRange(new ToolStripItem[] { zoomIn, zoomOut, restoreZoom });
       
            viewDropDown.DropDownItems.AddRange(new ToolStripItem[] { alwaysOnTop, zoomDropDown  });

            Items.Add(viewDropDown);
        }
    }
}
