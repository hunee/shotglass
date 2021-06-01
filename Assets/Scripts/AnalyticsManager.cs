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

using System;


public class AnalyticsManager : MonoBehaviour
{
  private void Awake()
  {            
    Debug.LogError("Awake");

    DontDestroyOnLoad(gameObject);
  }

  // Start is called before the first frame update
  void Start() {
  }

  // Update is called once per frame
  void Update() 
  {
  }

  public static void Login() {
    // Log an event with no parameters.
    Debug.LogError("Logging a login event.");
    FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLogin);

    Crashlytics.Log("AnalyticsManager.Login()");
    Crashlytics.SetCustomKey("guid", Guid.NewGuid().ToString());

    try {
      throw new Exception("Logging a login event.");
    }
    catch (Exception e) {
      Crashlytics.LogException(e);
    }
  }

  public void AnalyticsProgress() {
    // Log an event with a float.
    Debug.Log("Logging a progress event.");
    FirebaseAnalytics.LogEvent("progress", "percent", 0.4f);
  }

  public void AnalyticsScore() {
    // Log an event with an int parameter.
    Debug.Log("Logging a post-score event.");
    FirebaseAnalytics.LogEvent(
      FirebaseAnalytics.EventPostScore,
      FirebaseAnalytics.ParameterScore,
      42);
  }

  public void AnalyticsGroupJoin() {
    // Log an event with a string parameter.
    Debug.Log("Logging a group join event.");
    FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventJoinGroup, FirebaseAnalytics.ParameterGroupId,
      "spoon_welders");
  }

  public void AnalyticsLevelUp() {
    // Log an event with multiple parameters.
    Debug.Log("Logging a level up event.");
    FirebaseAnalytics.LogEvent(
      FirebaseAnalytics.EventLevelUp,
      new Parameter(FirebaseAnalytics.ParameterLevel, 5),
      new Parameter(FirebaseAnalytics.ParameterCharacter, "mrspoon"),
      new Parameter("hit_accuracy", 3.14f));
  }

  // Reset analytics data for this app instance.
  public void ResetAnalyticsData() {
    Debug.Log("Reset analytics data.");
    FirebaseAnalytics.ResetAnalyticsData();
  }
}
