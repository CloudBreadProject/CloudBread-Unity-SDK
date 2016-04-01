using UnityEngine;
using System.IO;
using System.Text;


namespace CloudBread
{
    public static class CBTool
    {
        static public string GetClassTextFile()
        {
            return Encoding.UTF8.GetString(Resources.Load<TextAsset>("Template.Class").bytes);
        }

        static public string ReadFile(string filePath_)
        {
            string xmlContent = "";
            if (File.Exists(filePath_))
            {
                StreamReader XMLReaderStream = File.OpenText(filePath_);
                xmlContent = XMLReaderStream.ReadToEnd();
                XMLReaderStream.Close();

                return xmlContent;
            }
            else
            {
                Debug.LogWarning("Files does not exist: " + filePath_);
                return null;
            }
        }

        static public bool SaveTextFile(string fullPath_, string text_)
        {
            try
            {
                FileInfo fileInfo = new System.IO.FileInfo(fullPath_);
                using (FileStream fs = fileInfo.Create())
                {
                    using (TextWriter tw = new StreamWriter(fs, System.Text.Encoding.UTF8))
                    {
                        tw.Write(text_);
                        tw.Close();
                    }
                    fs.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}