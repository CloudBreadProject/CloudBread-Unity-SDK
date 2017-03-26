using System;
using UnityEngine;

namespace CloudBread.OAuth
{
	public class FaceBookServices : BaseOAuth2Services
	{

		public void RequestToken(string access_token, System.Action<FacebookData.Receive> callback_, System.Action<string> errorCallback_ = null)
		{

			string url = CloudBread.Address + OAuth2Setting.FacebookRedirectAddress;
			Debug.Log (url);


			string postData = "{ \"access_token\" : \"" + access_token + "\"}";
			Debug.Log (postData);

			CloudBread.Request (url, postData, callback_, errorCallback_);

		}


		public class FacebookData
		{
			[Serializable]
			public struct Post
			{
				[SerializeField]
				public string access_token;
			}

			[Serializable]
			public struct Receive
			{
				[SerializeField]
				public string authenticationToken;

				[SerializeField]
				public User user;
			}

			[Serializable]
			public struct User
			{
				[SerializeField]
				public string userId;
			}
		}
	}
}

