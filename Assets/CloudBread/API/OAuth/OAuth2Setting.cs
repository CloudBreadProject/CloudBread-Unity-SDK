using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

namespace CloudBread.OAuth
{
	public class OAuth2Setting : ScriptableObject
	{
		private const string SettingAssetName = "CBOAuth2Setting";
		private const string SettingsPath = "CloudBread/Resources";
		private const string SettingsAssetExtension = ".asset";

		// Facebook
		private bool _useFacebook = false;
		static public bool UseFacebook 
		{
			get { return Instance._useFacebook; } 
			set { Instance._useFacebook = value; } 
		}

		private string _facebookRedirectAddress = ".auth/login/facebook";
		static public string FacebookRedirectAddress 
		{
			get { return Instance._facebookRedirectAddress; } 
			set { Instance._facebookRedirectAddress = value; }
		}



		// GoogePlay
		public bool _useGooglePlay = false;
		static public bool UseGooglePlay 
		{
			get { return instance._useGooglePlay; }
			set { Instance._useGooglePlay = value; }
		}
			
//		public string _googleRedirectAddress = ".auth/login/googleplay";
		public string _googleRedirectAddress = ".auth/login/google";
		static public string GoogleRedirectAddress 
		{
			get { return instance._googleRedirectAddress; }
			set { Instance._googleRedirectAddress = value; }
		}


		// KaKao
		private bool _useKaKao = false;
		public bool UseKaKao
		{
			get { return Instance._useKaKao; }
			set { Instance._useKaKao = value; }
		}
		public static string KakaoRedirectAddress;



		private static OAuth2Setting instance = null;
		public static OAuth2Setting Instance
		{
			get
			{
				if (instance == null)
				{
					instance = Resources.Load(SettingAssetName) as OAuth2Setting;
					if (instance == null)
					{
						// If not found, autocreate the asset object.
						instance = ScriptableObject.CreateInstance<OAuth2Setting>();

						#if UNITY_EDITOR
						string properPath = Path.Combine(Application.dataPath, SettingsPath);
						if (!Directory.Exists(properPath))
						{
							Directory.CreateDirectory(properPath);
						}

						string fullPath = Path.Combine(
							Path.Combine("Assets", SettingsPath),
							SettingAssetName + SettingsAssetExtension);
						AssetDatabase.CreateAsset(instance, fullPath);
						#endif
					}
				}
				return instance;
			}
		}


	}
}

