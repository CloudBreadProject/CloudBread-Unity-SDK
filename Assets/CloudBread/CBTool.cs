using UnityEngine;
using System.IO;
using System.Text;


namespace CloudBread
{
    public static partial class CBTool
    {
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
    }
}