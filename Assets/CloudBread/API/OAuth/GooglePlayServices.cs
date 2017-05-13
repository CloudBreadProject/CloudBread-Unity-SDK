using System;
using UnityEngine;

namespace CloudBread.OAuth
{
	public class GooglePlayServices : BaseOAuth2Services
	{
		public void RequestToken(string id_token, string access_token, Action<AzureZumoToken.Receive> callback, Action<string> errorCallback = null)
		{
			string url = CloudBread.Address + ".auth/login/google";
//			string url = CloudBread.Address + OAuth2Setting.GoogleRedirectAddress;
			Debug.Log (url);

			string postData = "{ \"id_token\": \"" + id_token + "\",  \"access_token\" : \"" + access_token + " \"}";
			Debug.Log (postData);

			CloudBread.Request (url, postData, callback, errorCallback);
		}


	}
}

