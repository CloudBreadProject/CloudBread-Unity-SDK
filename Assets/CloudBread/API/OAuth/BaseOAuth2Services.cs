using System;

namespace CloudBread.OAuth
{
	public class BaseOAuth2Services
	{
		public enum OAuthServices
		{
			facebook,
			google
		}

		public static BaseOAuth2Services GetServices(OAuthServices services){

			switch (services) {

			case OAuthServices.facebook:
				if (OAuth2Setting.UseFacebook) {
					return new FaceBookServices ();
				} else {
					return null;
				}

			case OAuthServices.google:
				if (OAuth2Setting.UseGooglePlay) {
					return new GooglePlayServices ();
				} else {
					return null;
				}

			default:
				return null;
				break;
			}

			return new BaseOAuth2Services();
		}

//		public abstract void RequestToken(FacebookData.Post postData_, System.Action<FacebookData.Receive[]> callback_, System.Action<string> errorCallback_ = null)

		public void SessionConfirm(){}

		public void RequestToken()
		{

		}

		public void RequestUserData()
		{

		}

	}
}

