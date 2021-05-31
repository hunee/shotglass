using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Threading.Tasks;

using Firebase;
using Firebase.Auth;

#if UNITY_EDITOR
#else

#if UNITY_ANDROID

using GooglePlayGames;
using GooglePlayGames.BasicApi;

using UnityEngine.SocialPlatforms;


/// <summary>
/// Manages interactions with Google Play Games.
/// </summary>
public class GooglePlayServicesSignIn
{
  /// <summary>
  /// Initializes the Google Play Games Client.
  /// </summary>
  public static void InitializeGooglePlayGames()
  {
    Debug.LogError("InitializeGooglePlayGames");

    PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
      .RequestServerAuthCode(false /*forceRefresh*/)
      .Build();

    PlayGamesPlatform.InitializeInstance(config);
    PlayGamesPlatform.DebugLogEnabled = true;
    PlayGamesPlatform.Activate();
  }

  /// <summary>
  /// Signs out of GPGS.
  /// </summary>
  public static void SignOut() {
    Debug.LogError("SignOut");

    PlayGamesPlatform.Instance.SignOut();
  }


  /// <summary>
  /// Private helper that signs the into Google Play Games and performs
  /// a Firebase account operation.
  /// </summary>
  private static Task<FirebaseUser> SignIntoGooglePlayServices(
    Action<Firebase.Auth.Credential,
    TaskCompletionSource<FirebaseUser>> operation)
  {
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
}

#endif //UNITY_ANDROID
#endif //UNITY_EDITOR

