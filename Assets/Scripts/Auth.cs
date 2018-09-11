using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Auth;
using Firebase.Unity.Editor;
using Facebook.Unity;

public class Auth : MonoBehaviour {

	void Awake ()
	{
		if (!FB.IsInitialized) {
			// Initialize the Facebook SDK
			FB.Init(InitCallback, OnHideUnity);
		} else {
			// Already initialized, signal an app activation App Event
			FB.ActivateApp();
		}
	}

	private void InitCallback ()
	{
		if (FB.IsInitialized) {
			// Signal an app activation App Event
			FB.ActivateApp();
			// Continue with Facebook SDK
			// ...
			var perms = new List<string>(){"public_profile", "email"};
			FB.LogInWithReadPermissions(perms, AuthCallback);
		} else {
			Debug.Log("Failed to Initialize the Facebook SDK");
		}
	}

	private void AuthCallback (ILoginResult result) {
		if (FB.IsLoggedIn) {
			// AccessToken class will have session details
			var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
			// Print current access token's User ID
			// Debug.Log(aToken.TokenString);
			this.AuthFirebase();
		} else {
			Debug.Log("User cancelled login");
		}
	}

	private void AuthFirebase() {
		
		Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
		Firebase.Auth.Credential credential = Firebase.Auth.FacebookAuthProvider.GetCredential(Facebook.Unity.AccessToken.CurrentAccessToken.TokenString);
		auth.SignInWithCredentialAsync(credential).ContinueWith(task => {
			if (task.IsCanceled) {
				Debug.LogError("SignInWithCredentialAsync was canceled.");
				return;
			}
			if (task.IsFaulted) {
				Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
				return;
			}
			
			Firebase.Auth.FirebaseUser user = auth.CurrentUser;
			if (user != null) {
				string name = user.DisplayName;
				string email = user.Email;
				System.Uri photo_url = user.PhotoUrl;
				// The user's Id, unique to the Firebase project.
				// Do NOT use this value to authenticate with your backend server, if you
				// have one; use User.TokenAsync() instead.
				string uid = user.UserId;
				SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
			}
		});
		
	}


	private void OnHideUnity (bool isGameShown)
	{
		if (!isGameShown) {
			// Pause the game - we will need to hide
			Time.timeScale = 0;
		} else {
			// Resume the game - we're getting focus again
			Time.timeScale = 1;
		}
	}
}
