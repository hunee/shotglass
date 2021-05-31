using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Firebase;
using Firebase.Auth;
using Firebase.Analytics;
using Firebase.Messaging;
using Firebase.RemoteConfig;
using Firebase.Extensions;

using System;
using System.Threading.Tasks;

public class FirebaseManager : MonoBehaviour
{
  public static bool firebaseInitialized;

  // Feature Flags
  public const string RemoteConfigGameplayRecordingEnabled = "feature_gameplay_recording";

  private void Awake()
  {            
    Debug.LogError("Awake");

    DontDestroyOnLoad(gameObject);
  }

  //void Start() {
  async void Start() {
    Debug.LogError("Start");

    InitializeFirebaseAndStart();
  }

  // Update is called once per frame
  void Update()
  {
      
  }

  // Sets the default values for remote config.  These are the values that will
  // be used if we haven't fetched yet.
  System.Threading.Tasks.Task InitializeRemoteConfig() {
    Debug.LogError("InitializeRemoteConfig");

    Dictionary<string, object> defaults = new Dictionary<string, object>();

    // Feature Flags
    defaults.Add(RemoteConfigGameplayRecordingEnabled, false);

    var remoteConfig = FirebaseRemoteConfig.DefaultInstance;
    return remoteConfig.SetDefaultsAsync(defaults)
      .ContinueWith(result => remoteConfig.FetchAndActivateAsync())
      .Unwrap();
  }

  // When the app starts, check to make sure that we have
  // the required dependencies to use Firebase, and if not,
  // add them if possible.
  void InitializeFirebaseAndStart() {
    Debug.LogError("InitializeFirebaseAndStart");

    FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
      var dependencyStatus = task.Result;
      if (dependencyStatus == DependencyStatus.Available) {
        InitializeFirebaseComponents();
      } else {
        Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);

        // Firebase Unity SDK is not safe to use here.
        Application.Quit();
      }
    });
  }

  Firebase.FirebaseApp app;
  async void InitializeFirebaseComponents() {
    Debug.LogError("InitializeFirebaseComponents");

    // Create and hold a reference to your FirebaseApp,
    // where app is a Firebase.FirebaseApp property of your application class.
    app = Firebase.FirebaseApp.DefaultInstance;

    // Set a flag here to indicate whether Firebase is ready to use by your app.
    //Firebase.AppOptions ops = new Firebase.AppOptions();
    //app = Firebase.FirebaseApp.Create(ops);

    SetFirebaseMessagingListeners();

    System.Threading.Tasks.Task.WhenAll(
        InitializeRemoteConfig()
      ).ContinueWith(task => { firebaseInitialized = true; });

  }

  // Start gathering analytic data.
  public static void InitializeAnalytics() {
    Firebase.Analytics.FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);

    // Set the user's sign up method.
    Firebase.Analytics.FirebaseAnalytics.SetUserProperty(
      Firebase.Analytics.FirebaseAnalytics.UserPropertySignUpMethod,
      "Google");

    //if (CommonData.currentUser != null)
    //  Firebase.Analytics.FirebaseAnalytics.SetUserId(CommonData.currentUser.data.id);
  }


  /// <summary>
  /// Signs into CreateUserWithEmailAndPassword (creating an account automatically
  /// if needed).
  /// </summary>
  /// <returns>The in.</returns>
  public static Task<FirebaseUser> CreateUserWithEmailAndPassword(string email, string password)
  {
    Debug.LogError("CreateUserWithEmailAndPassword");

    TaskCompletionSource<FirebaseUser> taskCompletionSource =
      new TaskCompletionSource<FirebaseUser>();

    FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(t => {
      if (!t.IsCompleted || t.IsCanceled) {
        taskCompletionSource.SetCanceled();
      } else if (t.IsFaulted) {
        taskCompletionSource.SetException(t.Exception);
      } else {
        //SignInState.SetState(SignInState.State.GooglePlayServices);
        taskCompletionSource.SetResult(t.Result);
      }
    });

    return taskCompletionSource.Task;
  }

  /// <summary>
  /// Signs into SignInWithEmailAndPassword (creating an account automatically
  /// if needed).
  /// </summary>
  /// <returns>The in.</returns>
  public static Task<FirebaseUser> SignInWithEmailAndPassword(string email, string password)
  {
    Debug.LogError("SignInWithEmailAndPassword");

    TaskCompletionSource<FirebaseUser> taskCompletionSource =
      new TaskCompletionSource<FirebaseUser>();

    FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(t => {
      if (!t.IsCompleted || t.IsCanceled) {
        taskCompletionSource.SetCanceled();
      } else if (t.IsFaulted) {
        taskCompletionSource.SetException(t.Exception);
      } else {
        //SignInState.SetState(SignInState.State.GooglePlayServices);
        taskCompletionSource.SetResult(t.Result);
      }
    });

    return taskCompletionSource.Task;
  }

  void OnDestroy() {
    Debug.LogError("OnDestroy");

    Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    auth.SignOut();
  }

    private void SetFirebaseMessagingListeners() {
      Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
      Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
    }

    public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token) {
      UnityEngine.Debug.Log("Received Registration Token: " + token.Token);
    }

    public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e) {
      UnityEngine.Debug.Log("Received a new message from: " + e.Message.From);
      
      UnityEngine.Debug.Log("From: " + e.Message.From);
      UnityEngine.Debug.Log("Message ID: " + e.Message.MessageId);      
/*
      UnityEngine.Debug.Log("Received a new message");
      if (e.Message.From.Length > 0)
        UnityEngine.Debug.Log("from: " + e.Message.From);
      if (e.Message.Data.Count > 0) {
        UnityEngine.Debug.Log("data:");
        foreach (System.Collections.Generic.KeyValuePair iter in
             e.Message.Data) {
          UnityEngine.Debug.Log("  " + iter.Key + ": " + iter.Value);
        }
      }*/
    }
}

