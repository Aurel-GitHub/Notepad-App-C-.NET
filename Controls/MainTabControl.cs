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
        private MainForm _form;
        public MainTabControl()
        {
            _contextMenuStrip = new TabControlContextMenuStrip();    
            Name = NAME;
            ContextMenuStrip = _contextMenuStrip;
            Dock = DockStyle.Fill; //positionnement de la fenetre Onglet sur l'appli

            HandleCreated += (s, e) =>
            {
                _form = FindForm() as MainForm;
            };

            SelectedIndexChanged += (s, e) =>
            {
                _form.CurrentFile = _form.Session.Files[SelectedIndex];
                _form.CurrentRtb = (CustomRichTextBox)_form.MainTabControl.TabPages[SelectedIndex].Controls.Find("RtbTextFileContents", true).First();
                _form.Text = $"{_form.CurrentFile.FileName} - Notepad.NET";
            };

            MouseUp += (s, e) =>
            {
                if(e.Button == MouseButtons.Right)
                {
                    for (int i = 0; i < TabCount; i++)
                    {
                        var rect = GetTabRect(i);
                        
                        if(rect.Contains(e.Location))
                        {
                            SelectedIndex = i;
                            break;
                        }
                    }
                }
            };
        }   
    }
}
    