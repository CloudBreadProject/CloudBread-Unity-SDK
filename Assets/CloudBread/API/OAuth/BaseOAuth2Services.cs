using System;
using UnityEngine;

namespace CloudBread.OAuth
{
	public abstract class BaseOAuth2Services
	{
		
		public abstract void RequestToken(string access_token, Action<AzureZumoToken.Receive> callback, Action<string> errorCallback = null);

//		public abstract void RequestUserData();

		public class AzureZumoToken
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

		public void SessionConfirm(){}

		public void RequestUserData()
		{

		}

	}
}

