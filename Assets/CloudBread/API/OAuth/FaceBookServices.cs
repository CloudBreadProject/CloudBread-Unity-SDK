using System;
using UnityEngine;

namespace CloudBread.OAuth
{
	public class FaceBookServices : BaseOAuth2Services
	{
		public void RequestToken(string access_token, Action<AzureZumoToken.Receive> callback, Action<string> errorCallback = null)
		{
			string url = CloudBread.Address + OAuth2Setting.FacebookRedirectAddress;
			Debug.Log (url);

			string postData = "{ \"access_token\" : \"" + access_token + " \"}";
			Debug.Log (postData);

			CloudBread.Request (url, postData, callback, errorCallback);
		}

		public void RequestUser(string access_token, Action<FacebookUserData>callback, Action<string> errorCallback = null)
		{
			string url = "https://graph.facebook.com/me?access_token=" + access_token;

			CloudBread.Request (url, null, callback, errorCallback);
		}

		[Serializable]
		public class FacebookUserData
		{
//			public FacebookUserData() : base(ExternalAuthServices.Facebook) { }

			[SerializeField]
			public string id { get; set; }

			[SerializeField]
			public string name { get; set; }

			[SerializeField]
			public string success { get; set; }
		}


	}
}

