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

		private enum MENU_CATEGORIES { GENERAL, FACEBOOK, GOOGLE, KAKAO};
		private MENU_CATEGORIES _selectedMenu = MENU_CATEGORIES.GENERAL;

		static CBAuthEditor _instance = null;

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

		bool _FacebookAuthEnabled = true;
		bool _GoogleGameAuthEnabled = false;

		bool myBool = true;
		float myFloat = 0.0f;

		const float _leftBodySize = 0.25f;

		private void GeneralSetting()
		{
			GUILayout.BeginVertical ();
			{

				GUILayout.Label ("CloudBread Login Service - OAuth 2.0", EditorStyles.largeLabel);

				//			myString = EditorGUILayout.TextField ("Text Field", myString);
				GUILayout.Label ("Facebook Settings", EditorStyles.boldLabel);


				GUILayout.BeginVertical ();
				{
					//				GUILayout.Box("CloudBread Login Service - OAuth 2.0", "IN BigTitle");
					//				GUILayout.Label("");

					GUILayout.Box ("To configure CloudBread Login Service in this Project,\n" +
						"you can get more information in our Project Sites", "IN Title");
					GUILayout.Label ("CloudBread 로그인 서비스를 설정하려면, 아래 프로젝트 사이트를 참조하세요.");

					if (GUILayout.Button ("OpenSource Project - CloudBread.", GUI.skin.box)) {
						Application.OpenURL ("https://github.com/CloudBreadProject/");
					}

					if (GUILayout.Toggle (_FacebookAuthEnabled, "Facebook Authentication")) {
						GUILayout.Box ("difjdijsojf");
						GUILayout.BeginHorizontal ();
						{
							GUILayout.Label ("Redirection URL : ");
							GUILayout.TextField ("", GUILayout.ExpandWidth (true));

						}
						GUILayout.EndHorizontal ();
					}

					GUILayout.Label ("CloudBread 로그인 서비스");
					GUILayout.Label ("");
				}
				GUILayout.EndVertical ();
			}
			GUILayout.EndVertical ();

		}

		private void FaceBookSetting()
		{
			EditorGUILayout.BeginVertical ();
			{
				
				_FacebookAuthEnabled = EditorGUILayout.BeginToggleGroup ("Optional Settings", _FacebookAuthEnabled);
				{
					myBool = EditorGUILayout.Toggle ("Toggle", myBool);
					myFloat = EditorGUILayout.Slider ("Slider", myFloat, -3, 3);



					EditorGUILayout.LabelField ("Facebook oAuth 2.0 Login Service");

					EditorGUILayout.BeginHorizontal ();
					{
						EditorGUILayout.LabelField ("Redirect Url : ");
						EditorGUILayout.TextArea ("https://cb2-auth-demo.azurewebsites.net/facebook");
					}
					EditorGUILayout.EndHorizontal ();

					if (GUILayout.Button ("Generate")) {
//						CBToolEditor.SaveTextFileInProject ();
					}
				
				}
				EditorGUILayout.EndToggleGroup ();
			}
			EditorGUILayout.EndVertical ();
		}

		private void GoogleGameSetting()
		{
			_GoogleGameAuthEnabled = EditorGUILayout.BeginToggleGroup ("Optional Settings", _GoogleGameAuthEnabled);
			{

			}
		}

		private void DrawLeftMenu()
		{
			GUILayout.BeginVertical (GUILayout.Width(position.width * _leftBodySize));
			{
				if (GUILayout.Button ("General"))
					_selectedMenu = MENU_CATEGORIES.GENERAL;
				
				if (GUILayout.Button ("Facebook"))
					_selectedMenu = MENU_CATEGORIES.FACEBOOK;

				if (GUILayout.Button ("Google"))
					_selectedMenu = MENU_CATEGORIES.GOOGLE;
				
				if (GUILayout.Button ("KaKao"))
					_selectedMenu = MENU_CATEGORIES.KAKAO;	

			}
			GUILayout.EndVertical ();
		}

		private void DrawRightBody()
		{
			
			if (_selectedMenu == MENU_CATEGORIES.GENERAL) {
				GeneralSetting ();
			}
			else if (_selectedMenu == MENU_CATEGORIES.FACEBOOK) {
				FaceBookSetting ();
			}
			else if (_selectedMenu == MENU_CATEGORIES.GOOGLE) {
				GoogleGameSetting ();
			}

		}

		void OnGUI()
		{
			GUILayout.BeginHorizontal ();
			{
				DrawLeftMenu ();

				DrawRightBody ();

			}
			GUILayout.EndHorizontal ();

		}


			
	}

}
