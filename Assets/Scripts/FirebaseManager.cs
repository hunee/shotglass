using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Firebase;
using Firebase.Auth;
using Firebase.Analytics;
using Firebase.Crashlytics;
using Firebase.Messaging;
using Firebase.RemoteConfig;
using Firebase.Extensions;

using GoogleMobileAds.Api;

#if GPGS && (UNITY_ANDROID && !UNITY_EDITOR)
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
#endif    

using System;
using System.Threading.Tasks;

using System.Security.Cryptography;

public class FirebaseManager : MonoBehaviour
{
  public static bool firebaseInitialized;

  // Feature Flags
  public const string RemoteConfigGameplayRecordingEnabled = "feature_gameplay_recording";

  private void Awake() {
    Debug.LogError("Awake");

    DontDestroyOnLoad(gameObject);
  }

  //void Start() {
  async void Start() {
    Debug.LogError("Start");

    // Initialize the Google Mobile Ads SDK.
    MobileAds.Initialize(initStatus => { });

#if GPGS && (UNITY_ANDROID && !UNITY_EDITOR)
    InitializeGooglePlayGames();
#endif

    InitializeFirebaseAndStart();
  }

  // Update is called once per frame
  void Update() {
      
  }

  void OnDestroy() {
    Debug.LogError("OnDestroy");

    Firebase.Auth.FirebaseAuth.DefaultInstance.SignOut();

#if GPGS && (UNITY_ANDROID && !UNITY_EDITOR)
    PlayGamesPlatform.Instance.SignOut();
#endif    
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


  //Firebase.FirebaseApp app;
  async void InitializeFirebaseComponents() {
    Debug.LogError("InitializeFirebaseComponents");

    // FirebaseApp is responsible for starting up Crashlytics, when the core app is started.
    // To ensure that the core of FirebaseApp has started, grab the default instance which
    // is lazily initialized.
    Firebase.FirebaseApp app = Firebase.FirebaseApp.DefaultInstance;

    // Set a flag here to indicate whether Firebase is ready to use by your app.
    //Firebase.AppOptions ops = new Firebase.AppOptions();
    //app = Firebase.FirebaseApp.Create(ops);

    SetFirebaseMessagingListeners();

    /*System.Threading.Tasks.Task.WhenAll(
        InitializeRemoteConfig()
      ).ContinueWith(task => { firebaseInitialized = true; });
*/
    await InitializeRemoteConfig();
    firebaseInitialized = true;
  }

  // Start gathering analytic data.
  public static async void InitializeAnalytics() {
    Firebase.Analytics.FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);

    // Set the user's sign up method.
    Firebase.Analytics.FirebaseAnalytics.SetUserProperty(
      Firebase.Analytics.FirebaseAnalytics.UserPropertySignUpMethod,
      "Google");

    // Set the user ID.
    var user = FirebaseAuth.DefaultInstance.CurrentUser;
    if (user != null) {
      Firebase.Analytics.FirebaseAnalytics.SetUserId(user.UserId);

      Crashlytics.SetUserId(user.UserId);      
    }

    // Set default session duration values.
    FirebaseAnalytics.SetSessionTimeoutDuration(new TimeSpan(0, 30, 0));

    var id = await DisplayAnalyticsInstanceId();
    Debug.LogError("DisplayAnalyticsInstanceId: " + id);
  }

  // Get the current app instance ID.
  public static Task<string> DisplayAnalyticsInstanceId() {
    Debug.LogError("DisplayAnalyticsInstanceId");

    return FirebaseAnalytics.GetAnalyticsInstanceIdAsync().ContinueWithOnMainThread(task => {
      if (task.IsCanceled) {
        Debug.Log("App instance ID fetch was canceled.");
      } else if (task.IsFaulted) {
        Debug.Log(String.Format("Encounted an error fetching app instance ID {0}",
                                task.Exception.ToString()));
      } else if (task.IsCompleted) {
        Debug.Log(String.Format("App instance ID: {0}", task.Result));
      }
      return task;
    }).Unwrap();
  }


  /// <summary>
  /// Signs into CreateUserWithEmailAndPassword (creating an account automatically
  /// if needed).
  /// </summary>
  /// <returns>The in.</returns>
  public static Task<FirebaseUser> CreateUserWithEmailAndPassword(string email, string password) {
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
  public static Task<FirebaseUser> SignInWithEmailAndPassword(string email, string password) {
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


#region PlayGamesPlatform

#if GPGS && (UNITY_ANDROID && !UNITY_EDITOR)

  /// <summary>
  /// Initializes the Google Play Games Client.
  /// </summary>
  public static void InitializeGooglePlayGames() {
    Debug.LogError("InitializeGooglePlayGames");

    PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
      .RequestServerAuthCode(false /*forceRefresh*/)
      .Build();

    PlayGamesPlatform.InitializeInstance(config);
    PlayGamesPlatform.DebugLogEnabled = true;
    PlayGamesPlatform.Activate();
  }

  /// <summary>
  /// Private helper that signs the into Google Play Games and performs
  /// a Firebase account operation.
  /// </summary>
  private static Task<FirebaseUser> SignIntoGooglePlayServices(
    Action<Firebase.Auth.Credential,
    TaskCompletionSource<FirebaseUser>> operation) {
    Debug.LogError("SignIntoGooglePlayServices");

    TaskCompletionSource<FirebaseUser> taskCompletionSource =
      new TaskCompletionSource<FirebaseUser>();

    UnityEngine.Social.localUser.Authenticate((bool success) => {
      if (success) {
        String authCode = PlayGamesPlatform.Instance.GetServerAuthCode();
        if (String.IsNullOrEmpty(authCode)) {
          Debug.LogError(@"Signed into Play Games Services but failed to get the server auth code,
          will not be able to sign into Firebase");
          taskCompletionSource.SetException(new AggregateException(
              new[] { new ApplicationException() }));
          return;
        }

        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        Firebase.Auth.Credential credential = Firebase.Auth.PlayGamesAuthProvider.GetCredential(authCode);

        operation(credential, taskCompletionSource);
      } else {
        Debug.LogError("Failed to sign into Play Games Services");
        taskCompletionSource.SetException(new AggregateException(
            new[] { new ApplicationException() }));
      }
    });

    return taskCompletionSource.Task;
  }


  /// <summary>
  /// Links the Google Play Games account to the current Firebase one (anonymous or email).
  /// </summary>
  public static Task<FirebaseUser> LinkAccount() {
    Debug.LogError("LinkAccount");

    return SignIntoGooglePlayServices(
        (Credential credential, TaskCompletionSource<FirebaseUser> taskCompletionSource) => {
          FirebaseAuth.DefaultInstance.CurrentUser.LinkWithCredentialAsync(
            credential).ContinueWith(t => {
              if (!t.IsCompleted || t.IsCanceled) {
                taskCompletionSource.SetCanceled();
              } else if (t.IsFaulted) {
                taskCompletionSource.SetException(t.Exception);
              } else {
                ///SignInState.SetState(SignInState.State.GooglePlayServices);
                taskCompletionSource.SetResult(t.Result);
              }
          });
    });
  }

  /// <summary>
  /// Signs into Google Play Games account and Firebase (creating an account automatically
  /// if needed).
  /// </summary>
  /// <returns>The in.</returns>
  public static Task<FirebaseUser> SignIn() {
    Debug.LogError("SignIn");

    return SignIntoGooglePlayServices(
      (Credential credential, TaskCompletionSource<FirebaseUser> taskCompletionSource) => {
        FirebaseAuth.DefaultInstance.SignInWithCredentialAsync(credential).ContinueWith(t => {
          if (!t.IsCompleted || t.IsCanceled) {
            taskCompletionSource.SetCanceled();
          } else if (t.IsFaulted) {
            taskCompletionSource.SetException(t.Exception);
          } else {
            //SignInState.SetState(SignInState.State.GooglePlayServices);
            taskCompletionSource.SetResult(t.Result);
          }
        });
    });
  }
#endif

#endregion 


#region Firebase.Messaging

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
  }
#endregion  
}

