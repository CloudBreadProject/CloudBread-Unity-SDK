using UnityEngine;
using System.Collections;

namespace CloudBread.OAuth
{
    public class OAuthManager 
    {

		public static BaseOAuth2Services OAuthService;

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
					Debug.LogError ("You have to check 'use facebook' checkbox. It's in CloudBread-CBAuthentication, Facebook Tab");
					return null;
				}

			case OAuthServices.google:
				if (OAuth2Setting.UseGooglePlay) {
					return new GooglePlayServices ();
				} else {
					Debug.LogError ("You have to check 'use google play' checkbox. It's in CloudBread-CBAuthentication, Google Tab");
					return null;
				}

			default:
				return null;
			}
		}

        public OAuthManager()
        {

        }
			
    }

}