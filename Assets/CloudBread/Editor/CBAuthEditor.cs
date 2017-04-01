using UnityEngine;
using UnityEditor;
using System.Collections;
using CloudBread.OAuth;

namespace CloudBread
{
	public class CBAuthEditor : EditorWindow 
	{

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

		const float _leftBodySize = 0.25f;

		private Texture2D logoTextur;


		private void GeneralSetting()
		{
			GUILayout.BeginVertical ();
			{
				GUILayout.Label ("CloudBread Login Service - OAuth 2.0", CBGUIStyleFactory.GetTitleGUIStyle());

//				GUILayout.Label ("Facebook Settings", EditorStyles.boldLabel);

				GUILayout.Label ("");

				GUILayout.BeginVertical ();
				{

					GUILayout.Box ("Before you configure CloudBread Login Service in this Project,\n" +
						"go our Project sites and see that documents", CBGUIStyleFactory.GetTitle2GUIStyle());
					GUILayout.Label ("CloudBread 로그인 서비스를 시작하기 전, 아래 문서를 참조하세요.", CBGUIStyleFactory.GetTitle2GUIStyle());
						

					if (GUILayout.Button ("Get Start with Documents (OpenSource Project - CloudBread.)", CBGUIStyleFactory.GetLinkButtonGUIStyle())) {
						Application.OpenURL ("https://github.com/CloudBreadProject/");
					}
						
					GUILayout.Label ("");
					GUILayout.Label ("CloudBread 로그인 서비스");

				}
				GUILayout.EndVertical ();
			}
			GUILayout.EndVertical ();

		}

		private void FaceBookSetting()
		{
			EditorGUILayout.BeginVertical ();
			{
				
				OAuth.OAuth2Setting.UseFacebook = EditorGUILayout.BeginToggleGroup ("Use Facebook OAuth Services", OAuth.OAuth2Setting.UseFacebook);
				{
					

					EditorGUILayout.LabelField ("Facebook oAuth 2.0 Login Service", CBGUIStyleFactory.GetContentGUIStlye());

					EditorGUILayout.BeginHorizontal ();
					{
						EditorGUILayout.LabelField ("CloudBread Url : " , CBGUIStyleFactory.GetContentGUIStlye());
						EditorGUILayout.TextArea (CBSetting.serverAddress);
					}
					EditorGUILayout.EndHorizontal ();

					EditorGUILayout.BeginHorizontal ();
					{
						EditorGUILayout.LabelField ("Redirect Url : ", CBGUIStyleFactory.GetContentGUIStlye());
						EditorGUILayout.TextArea (OAuth.OAuth2Setting.FacebookRedirectAddress);
					}
					EditorGUILayout.EndHorizontal ();

					EditorGUILayout.LabelField ("Full Redirection Url : " + CBSetting.serverAddress + OAuth2Setting.FacebookRedirectAddress);

					EditorGUILayout.LabelField ("");

				
					EditorGUILayout.BeginFadeGroup (0.0f);
					{
						GUILayout.Box ("You can get more information about Facebook Login Service here.!", CBGUIStyleFactory.GetContentGUIStlye());


						if (GUILayout.Button ("Facebook Developer Pages", CBGUIStyleFactory.GetLinkButtonGUIStyle())) {
							Application.OpenURL ("https://github.com/CloudBreadProject/");
						}

							
					}
					EditorGUILayout.EndFadeGroup ();
				}
				EditorGUILayout.EndToggleGroup ();


			}
			EditorGUILayout.EndVertical ();
		}

		private void GoogleGameSetting()
		{
			OAuth2Setting.UseGooglePlay = EditorGUILayout.BeginToggleGroup ("Use Google Play OAuth Services", OAuth2Setting.UseGooglePlay);
			{
				GUILayout.Label ("서 비 스 준 비 중 . . .", CBGUIStyleFactory.GetContentGUIStlye());
				GUILayout.Label ("빠 른 시 간 내 에   업 데 이 트 하 겠 습 니 다 .", CBGUIStyleFactory.GetContentGUIStlye());
			}
		}

		private void KaKaoGameSetting()
		{
			
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
			
			if (_selectedMenu == MENU_CATEGORIES.GENERAL)
				GeneralSetting ();
			else if (_selectedMenu == MENU_CATEGORIES.FACEBOOK)
				FaceBookSetting ();
			else if (_selectedMenu == MENU_CATEGORIES.GOOGLE)
				GoogleGameSetting ();
			else if (_selectedMenu == MENU_CATEGORIES.KAKAO)
				KaKaoGameSetting ();

		}

		void OnGUI()
		{
			GUILayout.BeginHorizontal ();
			{

		

			}
			GUILayout.EndHorizontal ();

			GUILayout.BeginVertical ();
			{
				GeneralSetting ();

				FaceBookSetting ();

				GoogleGameSetting ();
			}
			GUILayout.EndVertical ();

		}

		class CBGUIStyleFactory{

			public static GUIStyle GetTitleGUIStyle()
			{
				GUIStyle style = new GUIStyle(GUI.skin.GetStyle("label"));
				style.fontSize = 22;
				style.normal.textColor = Color.black ;
				style.alignment = TextAnchor.MiddleCenter;
				style.margin = new RectOffset (5, 5, 15, 5);
				return style;
			}

			public static GUIStyle GetTitle2GUIStyle()
			{
				GUIStyle style = new GUIStyle(GUI.skin.GetStyle("label"));
				style.fontSize = 15;
				style.normal.textColor = Color.black ;
				style.alignment = TextAnchor.LowerLeft;
//				style.margin = new RectOffset (5, 5, 5, 5);
				return style;
			}

			public static GUIStyle GetContentGUIStlye()
			{
				GUIStyle style = new GUIStyle(GUI.skin.GetStyle("label"));
				style.fontSize = 13;
				style.normal.textColor = Color.black ;
				style.alignment = TextAnchor.LowerLeft;
				style.margin = new RectOffset (5, 5, 5, 10);
				return style;
			}

			public static GUIStyle GetLinkButtonGUIStyle()
			{
				GUIStyle style = new GUIStyle(GUI.skin.GetStyle("button"));
				style.fontSize = 13;
				style.normal.textColor = Color.gray ;
				style.alignment = TextAnchor.MiddleCenter;
								style.margin = new RectOffset (2, 2, 5, 10);
				return style;
			}


		}
	}

}
