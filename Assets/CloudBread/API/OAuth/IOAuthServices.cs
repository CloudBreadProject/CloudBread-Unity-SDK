using System;

namespace CloudBread.OAuth
{
	public interface IOAuthServices
	{

		void RequestToken(string a, Action<BaseOAuth2Services.AzureZumoToken.Receive> callback, Action<string> errorCallback = null);

//		void SessionConfirm();
//		void RecreateToken();
	}
}

