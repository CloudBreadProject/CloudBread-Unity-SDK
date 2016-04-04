using UnityEngine;
using System.IO;
using System.Text;


namespace CloudBread
{
    public static class CBTool
    {
        static public string GetClassTextFile(string fileName_)
        {
            return Encoding.UTF8.GetString(Resources.Load<TextAsset>(fileName_).bytes);
        }

        static public string GetElementValueFromJson(string element_, string body_)
        {
            int startIndex = body_.IndexOf(element_) + element_.Length;
            startIndex = body_.IndexOf(":", startIndex);
            startIndex = body_.IndexOf('"', startIndex) + 1;
            int endIndex = body_.IndexOf('"', startIndex);

            try
            {
                return body_.Substring(startIndex, endIndex - startIndex);
            }
            catch
            {
                Debug.Log(string.Format("Error.{0}/{1}", startIndex, endIndex));
                return null;
            }
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

        static public bool SaveTextFileInProject(string path_, string text_)
        {
            return SaveTextFile(Application.dataPath + path_, text_);
        }
        static public bool SaveTextFile(string fullPath_, string text_)
        {
            try
            {
                FileInfo fileInfo = new System.IO.FileInfo(fullPath_);
                if(!fileInfo.Directory.Exists)
                {
                    fileInfo.Directory.Create();
                }
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