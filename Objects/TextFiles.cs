using System;
using System.IO;
using System.Xml.Serialization;

namespace Notepad.Objects
{
    public class TextFile
    {
        /// <summary>
        /// Chemin d'accès et nom du fichier.
        /// </summary>
        [XmlAttribute(AttributeName = "FileName")]
        public string FileName { get; set; }

        /// <summary>
        /// Chemin d'accès et nom du fichier backup.   
        /// </summary>
        [XmlAttribute(AttributeName = "BackupFileName")]
        public string BackupFileName { get; set; } = string.Empty;

        /// <summary>
        /// Nom et extension du fichier. Le nom du fichier n'inclut pas le chemin d'accès.      
        /// </summary>
        [XmlIgnore()]
        public string SafeFileName { get; set; }

        /// <summary>
        /// Nom et extension du fichier Backup. Le nom du fichier n'inclut pas le chemin d'accès.      
        /// </summary>
        [XmlIgnore()]
        public string SafeBackupFileName { get; set; }

        /// <summary>
        /// Contenu du fichier.    
        /// </summary>
        [XmlIgnore()]
        public string Contents { get; set; } = string.Empty;

        /// <summary>
        /// Constructeur vide pour la désérialisation de l'objet
        /// </summary>
        public TextFile()
        {
            
        }

        /// <summary>
        /// Constructeur de la classe TexteFile.
        /// </summary>
        /// <param name="FileName">Chemin d'accès et non du fichier.</param>
        public TextFile(string fileName)
        {
            FileName = fileName;
            SafeFileName = Path.GetFileName(FileName);

            if(FileName.StartsWith("Sans titre"))
            {
                SafeBackupFileName = $"{FileName}@{DateTime.Now: dd-MM-yyyy-hh-MM-ss}";
                BackupFileName = Path.Combine(Session.BackupPath, SafeBackupFileName);
            }


        }
    }
}
