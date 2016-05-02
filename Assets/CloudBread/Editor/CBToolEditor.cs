using UnityEngine;
using System.IO;
using System.Text;


namespace CloudBread
{
    public static partial class CBToolEditor
    {
        const string _editorDefaultResourcesPath = "CloudBread/{0}.txt";
        static public string GetClassTextFile(string fileName_)
        {
            //Editor Default Resources
            return Encoding.UTF8.GetString((UnityEditor.EditorGUIUtility.Load(string.Format(_editorDefaultResourcesPath, fileName_)) as TextAsset).bytes);
            //return Encoding.UTF8.GetString(Resources.Load<TextAsset>(fileName_).bytes);
        }

        static public string ReadFile(string filePath_)
        {
            string content = "";
            if (File.Exists(filePath_))
            {
                StreamReader streamReader = File.OpenText(filePath_);
                content = streamReader.ReadToEnd();
                streamReader.Close();

                return content;
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
                if (!fileInfo.Directory.Exists)
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