using UnityEngine;
using UnityEditor;
using System.Collections;

namespace CloudBread
{
	public class CBAuthEditor : EditorWindow 
	{
		private const string LoginSettingsAssetName = "FacebookLoginSettings";
		private const string LoginSettingsPath = "CloudBread/Resources";
		private const string LoginSettingsAssetExtension = ".asset";

		static CBAuthEditor _instance = null;

		[MenuItem("CloudBread/CB-Authentication")]
		public static void InitWindow()
		{
			if (null == _instance)
			{
				_instance = GetWindow<CBAuthEditor>();
			}
			else
			{
				_instance.Close();
			}
		}

		bool _useFacebookAuth = true;
		bool _useGoogleAuth = false;

		void OnGUI()
		{
			GUILayout.BeginVertical ();
			{
//				GUILayout.Box("CloudBread Login Service - OAuth 2.0", "IN BigTitle");
//				GUILayout.Label("");
				GUILayout.Label ("CloudBread Login Service - OAuth 2.0");
				GUILayout.Box ("To configure CloudBread Login Service in this Project,\n" +
					"you can get more information in our Project Sites", "IN Title");
				GUILayout.Label ("CloudBread 로그인 서비스를 설정하려면, 아래 프로젝트 사이트를 참조하세요.");

				if (GUILayout.Button("OpenSource Project - CloudBread.", GUI.skin.box))
				{
					Application.OpenURL("https://github.com/CloudBreadProject/");
				}

				if (GUILayout.Toggle (_useFacebookAuth, "Facebook Authentication")) {
					GUILayout.Box ("difjdijsojf");
					GUILayout.BeginHorizontal ();
					{
						GUILayout.Label ("Redirection URL : ");
						GUILayout.TextField ("", GUILayout.ExpandWidth(true));

					}
					GUILayout.EndHorizontal ();
				}

				GUILayout.Label ("CloudBread 로그인 서비스");
				GUILayout.Label ("");
			}
			GUILayout.EndVertical ();

		}


			
	}

}
