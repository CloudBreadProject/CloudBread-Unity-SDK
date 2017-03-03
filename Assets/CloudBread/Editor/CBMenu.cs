using UnityEngine;
using UnityEditor;

namespace CloudBread
{
	public class CBMenu {


		#if UNITY_EDITOR

		[MenuItem("CloudBread/Setting", false, 0)]
		public static void Settings()
		{
			Selection.activeObject = CBSetting.instance;
		}

		[MenuItem("CloudBread/Developers Page", false, 1)]
		public static void OpenWeb()
		{
			string url = "https://github.com/CloudBreadProject";
			Application.OpenURL(url);
		}

		[MenuItem("CloudBread/SDK Document", false, 2)]
		public static void OpenDocs()
		{
			string url = "https://github.com/CloudBreadProject/CloudBread/wiki/How-to-use-Unity-SDK-kor";
			Application.OpenURL(url);
		}

		[MenuItem("CloudBread/CB-PostMan")]
		public static void OpenCBPostManWindows()
		{
			CBPostman.InitWindow ();
		}

		/*
		[MenuItem("CloudBread/CB-Authentication")]
		public static void OpenAuthenticationSettingsWindows()
		{
			CBAuthEditor.InitWindow ();
		}
		*/

		#endif
	}
}