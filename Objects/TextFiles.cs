using System.IO;
using System.Xml.Serialization;

namespace Notepad.Objects
{
    public class TextFile
    {
        [XmlAttribute(AttributeName = "FileName")]
        /// <summary>
        /// Chemin d'accès et nom du fichier.
        /// </summary>
        public string FileName { get; set; }

        [XmlAttribute(AttributeName = "BackupFileName")]
        /// <summary>
        /// Chemin d'accès et nom du fichier backup.   
        /// </summary>
        public string BackupFileName { get; set; } = string.Empty;

        [XmlIgnore()]   
        /// <summary>
        /// Nom et extension du fichier. Le nom du fichier n'inclut pas le chemin d'accès.      
        /// </summary>
        public string SafeFileName { get; set; }

        [XmlIgnore()]
        /// <summary>
        /// Nom et extension du fichier Backup. Le nom du fichier n'inclut pas le chemin d'accès.      
        /// </summary>
        public string SafeBackupFileName { get; set; }

        [XmlIgnore()]
        /// <summary>
        /// Contenu du fichier.    
        /// </summary>
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
        }

         

    }
}
