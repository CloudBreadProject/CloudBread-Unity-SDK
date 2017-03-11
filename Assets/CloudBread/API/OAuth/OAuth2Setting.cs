using System;
using UnityEngine;

namespace CloudBread.OAuth
{
	public class OAuth2Setting : ScriptableObject
	{

		static bool _useFacebook;

		static public string FaceBookRedirectAddress;



		static bool _useGooglePlay;

		public static string GooglePlayRedirectAddress;



		static bool _useKaKao;

		public static string KakaoRedirectAddress;


		public OAuth2Setting ()
		{
		}
	}
}

