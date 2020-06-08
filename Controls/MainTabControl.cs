﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepad.Controls
{
    public class MainTabControl : TabControl
    {
        //Création de la zone de texte
        private const string NAME = "MainTabControl";
        private ContextMenuStrip _contextMenuStrip;   
        public MainTabControl()
        {
            _contextMenuStrip = new TabControlContextMenuStrip();    
            Name = NAME;
            ContextMenuStrip = _contextMenuStrip;
            Dock = DockStyle.Fill; //positionnement de la fenetre Onglet sur l'appli


        }   
    }
}