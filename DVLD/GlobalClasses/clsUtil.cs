using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public class clsUtil
    {
        public static string GenerateGUID()
        {
            Guid NewGuid = Guid.NewGuid();

            return NewGuid.ToString();
        }

        public static bool CreateFolderIfDoesNotExist(string Folderpath)
        {
            if (!Directory.Exists(Folderpath))
            {
                try
                {
                    Directory.Exists(Folderpath);
                    return true;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(" Error Creating Folder !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

        public static string ReplaceFileNameWithGuid(string SourceFile)
        {
            string FileName = SourceFile;
            FileInfo fileInfo = new FileInfo(FileName);
            string Extn = fileInfo.Extension;
            return GenerateGUID() + Extn;

        }

        public static bool CopyImageToProjectImagesFolder(ref string SourceFile)
        {
            string DestinationFolder = @"C:\DVLD_Person_Image\";
            if (!CreateFolderIfDoesNotExist(DestinationFolder))
            {
                return false;
            }

            string DestinationFile = DestinationFolder + ReplaceFileNameWithGuid(SourceFile);
            try
            {
                File.Copy(SourceFile, DestinationFile,true);
            }
           catch (IOException IOE)
            {
                MessageBox.Show(IOE.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            SourceFile = DestinationFile;
            return true;
        }
    }
}
