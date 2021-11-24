using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
namespace EC_TH2012_J.Models
{
    public class CommonFunctions
    {
        //Ham xoa tat ca file trong thu muc
        public void DeleteAll(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
        }
        //Ham xoa 1 file trong thu muc
        public string DeleteFileInDir(string path)
        {
            string result = "";
           // string completePath = Server.MapPath("~/PDF/Document/" + Session["ID"].ToString()) + "SimpleTable" + index + ".pdf");
            string completePath = HttpContext.Current.Server.MapPath(path);
            if (System.IO.File.Exists(completePath))
            {
                System.IO.File.Delete(completePath);
                result = "ok";
            }
            return result;
        }
        //Ham backup
        /**
         *  string sourceDir = @"c:\current";
            string backupDir = @"c:\archives\2008";

         **/ 
        public void backup(string sourceDir, string backupDir)
        {
           
            try
            {
                string[] picList = Directory.GetFiles(sourceDir, "*.jpg");
                string[] txtList = Directory.GetFiles(sourceDir, "*.txt");

                // Copy picture files.
                foreach (string f in picList)
                {
                    // Remove path from the file name.
                    string fName = f.Substring(sourceDir.Length + 1);

                    // Use the Path.Combine method to safely append the file name to the path.
                    // Will overwrite if the destination file already exists.
                    File.Copy(Path.Combine(sourceDir, fName), Path.Combine(backupDir, fName), true);
                }

                // Copy text files.
                foreach (string f in txtList)
                {

                    // Remove path from the file name.
                    string fName = f.Substring(sourceDir.Length + 1);

                    try
                    {
                        // Will not overwrite if the destination file already exists.
                        File.Copy(Path.Combine(sourceDir, fName), Path.Combine(backupDir, fName));
                    }

                    // Catch exception if the file was already copied.
                    catch (IOException copyError)
                    {
                        Console.WriteLine(copyError.Message);
                    }
                }

                // Delete source files that were copied.
                foreach (string f in txtList)
                {
                    File.Delete(f);
                }
                foreach (string f in picList)
                {
                    File.Delete(f);
                }
            }

            catch (DirectoryNotFoundException dirNotFound)
            {
                Console.WriteLine(dirNotFound.Message);
            }
        }
    }
}