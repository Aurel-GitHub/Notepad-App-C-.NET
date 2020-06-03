using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepad.Controls
{
    class MainMenuStrip : MenuStrip
    {
        public MainMenuStrip()
        {
            Name = "MainMenuStrip";
            Dock = DockStyle.Top;

            FileDropDownMenu();
            EditDropDownMenu();
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

        public void EditDropDownMenu()
        {
            var editDropDownMenu = new ToolStripMenuItem("Edition");

            var cancelMenu = new ToolStripMenuItem("Annuler", null, null, Keys.Control | Keys.Z);
            var restoreMenu = new ToolStripMenuItem("Restaurer", null, null, Keys.Control | Keys.Y);
            //ici le copier coller sont rajoutés en plus
            var copyMenu = new ToolStripMenuItem("Copier", null, null, Keys.Control | Keys.C);
            var pasteMenu = new ToolStripMenuItem("Restaurer", null, null, Keys.Control | Keys.V);


            editDropDownMenu.DropDownItems.AddRange(new ToolStripItem[] { cancelMenu, restoreMenu, copyMenu, pasteMenu });

            Items.Add(editDropDownMenu);
        }

        public void FormatDropDownMenu()
        {
            var formatDropDownMenu = new ToolStripMenuItem("Affichage");

            var fontMenu = new ToolStripMenuItem("Police...");


            formatDropDownMenu.DropDownItems.AddRange(new ToolStripItem[] { fontMenu});

            Items.Add(formatDropDownMenu);

        }

        public void ViewDropDownMenu()
        {
            var viewDropDownMenu = new ToolStripMenuItem("Affichage");

            var alwaysOnTopMenu = new ToolStripMenuItem("Toujours Devant");


            var zoomDropDownMenu = new ToolStripMenuItem("Zoom");

            var zoomInMenu = new ToolStripMenuItem("Zoom avant", null, null, Keys.Control | Keys.Add);
            var zoomOutMenu = new ToolStripMenuItem("Zoom arrière", null, null, Keys.Control | Keys.Subtract);
            var zoomResetMenu = new ToolStripMenuItem("Restaurer le Zoom par défaut", null, null, Keys.Control | Keys.Divide);

            zoomInMenu.ShortcutKeyDisplayString = "Ctrl+Num +";
            zoomOutMenu.ShortcutKeyDisplayString = "Ctrl+Num -";
            zoomResetMenu.ShortcutKeyDisplayString = "Ctrl+Num /";

            zoomDropDownMenu.DropDownItems.AddRange(new ToolStripItem[] { zoomInMenu, zoomOutMenu, zoomResetMenu });

            viewDropDownMenu.DropDownItems.AddRange(new ToolStripItem[] { alwaysOnTopMenu, zoomDropDownMenu  });

            Items.Add(viewDropDownMenu);
        }
       
    }
}
