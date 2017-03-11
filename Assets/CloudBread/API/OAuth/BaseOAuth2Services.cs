using System;

namespace CloudBread.OAuth
{
	public class BaseOAuth2Services
	{
		public BaseOAuth2Services ()
		{
			
		}

		public enum OAuthServices
		{
			FaceBook,
			Google
		}

		public static BaseOAuth2Services GetServices(OAuthServices services){

			switch (services) {

			case OAuthServices.FaceBook:
				return new FaceBookServices ();
				break;


			case OAuthServices.Google:
				break;

			default:
				break;
			}

			return new BaseOAuth2Services();
		}

		public void SessionConfirm(){}

		public void RequestToken()
		{

		}

		public void RequestUserData()
		{

		}

	}
}

