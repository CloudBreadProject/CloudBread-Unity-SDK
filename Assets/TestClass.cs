using UnityEngine;
using System.Collections;
using CloudBread.OAuth;

using UnityEngine.SocialPlatforms;

public class TestClass : MonoBehaviour {

	// Use this for initialization
	void Start () {
	

//		GooglePlayGames.
//		PlayGamesClientConfiguration.Builder.RequestServerAuthCode (false);

//		PlayGamesPlatform.Instance.GetServerAuthCode()
//
//		// post score 12345 to leaderboard ID "Cfji293fjsie_QA")
//		Social.ReportScore(12345, "Cfji293fjsie_QA", (bool success) => {
//			// handle success or failure
//			print(PlayGamesClientConfiguration.Builder.RequestIdToken());
//			print(((PlayGamesLocalUser) Social.localUser).GetIdToken());
//
//		});
//





//		FaceBookServices a = new FaceBookServices ();
//		a.reque

//		Debug.Log (OAuth2Setting.Instance._faceBookRedirectAddress);

		string accessToken = "EAAC8oNCMYl0BACUQ9J7xjFTn2uANac3uIWYhsyhdrDhZBnW4URCjU7C7wS6VeZBIYJm8NO4OPbVugUj6yDXIZC8PWSmLZA6EL9mEaoNwBXyhV9qkABArTlm5UfZAFo4k3Q6QAHyJYYzKueNte7zG5DMaOzFbmy2UzN2m0HH3qeMThyNXE920Aa5b8qcIGmtRZCL24NFbh2gGexUZAXLCIoV3w769slma8cZD";

//		FaceBookServices fbServices = new FaceBookServices ();
//		fbServices.RequestToken (accessToken, (FaceBookServices.FacebookData.Receive obj) => {
//			Debug.Log(obj.user.userId);
//		});
//
//		CloudBread.AMAAuthentication.Request (new CloudBread.AMAAuthentication.Post {
//			access_token = "EAAC8oNCMYl0BAHdT9TpIOupGdjg47CvxhPADWpZAT2VT6NYJ6VCGjrWFcC3eZA1SW9Kq8AOtiA4AxAbtWiY8U2lEBOYYwaSJSLZAGR1RaUZCEyZCJMw7BYk4KObA9UyvVcoRolmbu5ZAREBtdApEJD7ZBd3osIE2eoOMk0rWIWj0Ms3G8SeeadhqBy90IULaORMdJnwoGBJKBACI07Plx41r4UiDovedngZD"
//		}, (CloudBread.AMAAuthentication.Receive obj) => {
//			Debug.Log(obj.authenticationToken);
//		});

//		FaceBookServices oAuthService = BaseOAuth2Services.GetServices (BaseOAuth2Services.OAuthServices.facebook) as FaceBookServices;
//		oAuthService.RequestToken (accessToken, (FaceBookServices.FacebookData.Receive obj) => {
//			Debug.Log(obj.user.userId);
//		});

//		FaceBookServices oAuthService2 = OAuthManager.GetServices(OAuthManager.OAuthServices.facebook) as FaceBookServices;
//		oAuthService2.RequestToken (accessToken, (FaceBookServices.FacebookData.Receive obj) => {
//			Debug.Log(obj.user.userId);
//		});

//		FaceBookServices oAuthFacebook =  OAuthManager.GetServices (OAuthManager.OAuthServices.facebook) as FaceBookServices;
//		oAuthFacebook.RequestToken (accessToken, (BaseOAuth2Services.AzureZumoToken.Receive obj) => {
//			Debug.Log(obj.user.userId);
//		});
//
//		oAuthFacebook.RequestUser (accessToken, (FaceBookServices.FacebookUserData obj) => {
//			Debug.Log(obj.name);
//		});

		string idtoken = "eyJhbGciOiJSUzI1NiIsImtpZCI6ImY3ZDRlODdjN2I2NmVjZjMzMmYwNjBhOTdhNTlkMzE0OWM2YmY3MzUifQ.eyJhenAiOiIzMjMyMzg2OTcwOTktcWE2ZGs0MzJnMzAzb2p2cDRncHQ0N3JodnRldHF1Nm8uYXBwcy5nb29nbGV1c2VyY29udGVudC5jb20iLCJhdWQiOiIzMjMyMzg2OTcwOTktYm9rYmZiYzdzOGNwMmZvYW9xZWVlbHZpNHFvaWkycnMuYXBwcy5nb29nbGV1c2VyY29udGVudC5jb20iLCJzdWIiOiIxMTYxODAxMjg4OTE1MTI1NDk2OTMiLCJlbWFpbCI6Inlvb25zZW9rNDE3QGdtYWlsLmNvbSIsImVtYWlsX3ZlcmlmaWVkIjp0cnVlLCJpc3MiOiJodHRwczovL2FjY291bnRzLmdvb2dsZS5jb20iLCJpYXQiOjE0OTc4MDM5MzgsImV4cCI6MTQ5NzgwNzUzOCwibmFtZSI6Iu2ZjeycpOyEnSIsImdpdmVuX25hbWUiOiLsnKTshJ0iLCJmYW1pbHlfbmFtZSI6Iu2ZjSJ9.IH9ZDFaGEhPAUsLobarBQSuZNBlHLbsv66tQGsNA24P99uEwpAQLfCLoWZOuloczFtyoBxG8y0OOCmycoo5zdkUcA8YWoxoJZoXXX0outaxOcSzoIlypG_A5PIKy2NOtSau4ad5CfP6KEaTa36DnVHA0iJMkpkmC-WCuKA5DQey6lRBdxgy0alv6NHSym0R7kgtCxoLZDZSy5xwYLqO3E3Cc60QLofdKq7rs8_ohwaekeWmf5UqLFWFmMk_rwx7BCoBa5tn5fcTvfakVpHfd6Xm8tpjDC4VPKCSZ0XMQ645oGK1n-hMPvgOsClknKQJhEsd8PU4RmobhwxjMr-emXw";
		string accesstoken = "4/V2b3GpjmNauKJ_Z6bzOEx0D35qHfCvtUq2K6wo0hXVY";

		Debug.Log (idtoken);

		GooglePlayServices oAuthGoogle = OAuthManager.GetServices (OAuthManager.OAuthServices.google) as GooglePlayServices;
		oAuthGoogle.RequestToken (idtoken, accessToken, (BaseOAuth2Services.AzureZumoToken.Receive obj) =>  {
			Debug.Log (obj.user.userId);

		});

//		GooglePlayServices oAuthGoogle = OAuthManager.GetServices(OAuthManager.OAuthService.
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
