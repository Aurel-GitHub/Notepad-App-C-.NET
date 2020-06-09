﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepad.Controls
{
    public class TabControlContextMenuStrip : ContextMenuStrip
    {
        private const string NAME = "TabControlContextMenuStrip";
        public TabControlContextMenuStrip()
        {
            Name = NAME;

            var closeTab = new ToolStripMenuItem("Fermer");
            var closeAllTabsExceptThis = new ToolStripMenuItem("Fermer tout sauf ce fichier");
            var openFileInExplorer = new ToolStripMenuItem("Ouvrir le répertoire en cours du fichier dans l'explorateur ");

            Items.AddRange(new ToolStripItem[] { closeTab, closeAllTabsExceptThis, openFileInExplorer });
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }
    }
}
