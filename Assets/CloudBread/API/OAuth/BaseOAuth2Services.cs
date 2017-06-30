using System;
using UnityEngine;

namespace CloudBread.OAuth
{
	public abstract class BaseOAuth2Services
	{
		
		public BaseOAuth2Services RequestBodyBuilder(){
			return this;
		}


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

