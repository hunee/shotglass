using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UnityEngine.Events;
using UnityEngine.UI;

using Firebase;
using Firebase.Auth;


public class NewBehaviourScript : MonoBehaviour
{
    public Text _authCode;

    private void Awake()
    {            
      Debug.LogError("Awake");

      Debug.LogError("Application.platform: " + Application.platform.ToString());
      //if( Application.platform == RuntimePlatform.WindowsPlayer )
      {
      }

    }

    IEnumerator Start() {
    //async void Start() {

      Debug.LogError("Start");

      Screen.SetResolution(Screen.width / 2, Screen.height / 2, true);

      while (!FirebaseManager.firebaseInitialized) {
        yield return null;
      }

      StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Actually start the game, once we've verified that everything
    // is working and we have the firebase prerequisites ready to go.
    async void StartGame() {
      Debug.LogError("StartGame");

#if UNITY_EDITOR
      string email = "jhhunee@gmail.com";
      string password = SystemInfo.deviceUniqueIdentifier;;

      var newUser = await FirebaseManager.SignInWithEmailAndPassword(email, password);
      //var newUser = await FirebaseManager.CreateUserWithEmailAndPassword(email, password);
#else

#if GPGS
      var newUser = await FirebaseManager.SignIn();
#endif

#endif

      FirebaseManager.InitializeAnalytics();

      //Firebase.Auth.FirebaseUser newUser = FirebaseAuth.DefaultInstance.CurrentUser;
      Debug.LogFormat("------------------- User signed in successfully: {0} ({1})",
        newUser.DisplayName, newUser.UserId);

      _authCode.text = newUser.ProviderId;

      AnalyticsManager.Login();

      if (newUser != null) {
/*
  foreach (var profile in newUser.ProviderData) {
    // Id of the provider (ex: google.com)
    string providerId = profile.ProviderId;

    // UID specific to the provider
    string uid = profile.UserId;

    // Name, email address, and profile photo Url
    string name = profile.DisplayName;
    string email = profile.Email;
    System.Uri photoUrl = profile.PhotoUrl;
  }*/
  }
}

  void OnDestroy() {
    Debug.LogError("OnDestroy");
  }

}
