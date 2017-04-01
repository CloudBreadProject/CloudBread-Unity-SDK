using System;
using UnityEngine;

namespace CloudBread.OAuth
{
	public class GooglePlayServices : BaseOAuth2Services
	{
		public GooglePlayServices ()
		{
		}
		public override void RequestToken(string access_token, Action<AzureZumoToken.Receive> callback, Action<string> errorCallback = null)
		{

		}

		public void RequestToken(FacebookData.Post postData_, System.Action<FacebookData.Receive[]> callback_, System.Action<string> errorCallback_ = null)
		{
			string url = ".auth/login/googleplay";
			string postData;

			CloudBread.Request(CloudBread.MakeFullUrl(url), JsonUtility.ToJson(postData_), callback_, errorCallback_);
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

