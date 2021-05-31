# Shot glass

*single asterisks*
_single underscores_
**double asterisks**
__double underscores__
~~cancelline~~


<style>
r { color: Red }
o { color: Orange }
g { color: Green }
</style>

# TODOs:

- <r>TODO:</r> Important thing to do
- <o>TODO:</o> Less important thing to do
- <g>DONE:</g> Breath deeply and improve karma

**My Bold Text, in red color.**{: style="color: red; opacity: 0.80;" }


[play.google.com/console/developers]: https://play.google.com/console/developers
[github.com/playgameservices/play-games-plugin-for-unity/releases]: https://github.com/playgameservices/play-games-plugin-for-unity/releases

> ### [Google Play Console][play.google.com/console/developers]
>> - ##### [play-games-plugin-for-unity][github.com/playgameservices/play-games-plugin-for-unity/releases]
>>> ###### https://github.com/playgameservices/play-games-plugin-for-unity/tree/master/current-build
>> - ##### Unity3D 2021 SDK not found error.
>>> ###### Assets/GooglePlayGames/Editor/GPGSUtil.cs
```C#
        public static string GetAndroidSdkPath()
        {
            string sdkPath = EditorPrefs.GetString("AndroidSdkRoot");
448: #if UNITY_2019 || UNITY_2020 || UNITY_2021
            ...
```
>> - ##### Android setup 
```
<?xml version="1.0" encoding="utf-8"?>
<!--Google Play game services IDs. Save this file as res/values/games-ids.xml in your project.-->
<resources>
  <!--app_id-->
  <string name="app_id" translatable="false">234033842797</string>
  <!--package_name-->
  <string name="package_name" translatable="false"></string>
  <!--leaderboard shot glass-->
  <string name="leaderboard_shot_glass" translatable="false">CgkI7ZSE7OcGEAIQAQ</string>
</resources>

OAuth Client ID: 
  234033842797-9921q0jsh68obdqa5bqhu3d7viq4vahl.apps.googleusercontent.com
```

<img src="http://cfile6.uf.tistory.com/image/2426E646543C9B4532C7B0" width="450px" height="300px" title="px(픽셀) 크기 설정" alt="RubberDuck"></img><br/>
<img src="http://cfile6.uf.tistory.com/image/2426E646543C9B4532C7B0" width="40%" height="30%" title="%(비율) 크기 설정" alt="RubberDuck"></img>


[apps.admob.com/v2/home]: https://apps.admob.com/v2/home
[github.com/googleads/googleads-mobile-unity/releases/tag/v6.0.0]: https://github.com/googleads/googleads-mobile-unity/releases/tag/v6.0.0

[developers.google.com/admob/unity/banner]: https://developers.google.com/admob/unity/banner?hl=ko
[developers.google.com/admob/unity/rewarded]: https://developers.google.com/admob/unity/rewarded?hl=ko

> ### [Google AdMob][apps.admob.com/v2/home]
>> - ##### [Google Mobile Ads Unity Plugin v6.0.0][github.com/googleads/googleads-mobile-unity/releases/tag/v6.0.0]
>> - ##### adUnitId
```
          sample ID: ca-app-pub-3940256099942544~3347511713
          배너 광고 ID: ca-app-pub-3940256099942544/6300978111
          보상형 광고 ID: ca-app-pub-3940256099942544/5224354917
```
>> - ##### [배너 광고  |  Unity  |  Google Developers][developers.google.com/admob/unity/banner] - PASS
>> - ##### [보상형 광고  |  Unity  |  Google Developers][developers.google.com/admob/unity/rewarded] - PASS


[firebase.google.com]: https://console.firebase.google.com/?hl=ko
[firebase.google.com/download/unity]:https://firebase.google.com/download/unity?authuser=0
[firebase.google.com/docs/unity/setup]: https://firebase.google.com/docs/unity/setup?hl=ko

[firebase.google.com/docs/auth/unity/password-auth]: https://firebase.google.com/docs/auth/unity/password-auth?authuser=0
[firebase.google.com/docs/auth/unity/play-games]: https://firebase.google.com/docs/auth/unity/play-games?hl=ko

> ### [Firebase][firebase.google.com]
>> - ##### [Firebase Unity SDK][firebase.google.com/download/unity]
>> - ##### [Unity 프로젝트에 Firebase 추가][firebase.google.com/docs/unity/setup] - PASS
>> - ##### Firebase.Analytics - PASS
>> - ##### Firebase.Auth - PASS
>>> - ###### [Unity를 사용하여 비밀번호 기반 계정으로 Firebase에 인증][firebase.google.com/docs/auth/unity/password-auth] - PASS
>>> - ###### [Unity에서 Google Play 게임 서비스를 사용하여 인증  |  Firebase][firebase.google.com/docs/auth/unity/play-games] - PASS
>> - ##### Firebase.Messaging - PASS
>>> - ##### Token: fxA77rkpTl--6NFPVD0Ky7:APA91bGSV9eo35gHnr2JwS8gOAGjfF2oRZGHlrrcjkAEZUhiPUXFN4G6eL4y0s1ZzFIC9nGrTiXr1y08HeVpwGsX0qSw6gjp7m_nZVBYd7RvMvqjSQfnsAROdcKT9GYIOPfEEilwaRRP
>> - ##### Firebase.RemoteConfig - ????





## Android

```
$ vi ~/.bash_profile

export ANDROID_HOME=/Library/Android/android-sdk-macosx
export ANDROID_SDK_ROOT=/Library/Android/android-sdk-macosx
export NDK_ROOT=/Library/Android/android-ndk-r8e
export PATH=${PATH}:${ANDROID_SDK_ROOT}\tools:${ANDROID_SDK_ROOT}\platform-tools:${NDK_ROOT}

$ vi ~/.zshrc
source ~/.bash_profile
```

> - ##### debug keystore
```
$ keytool -keystore ~/.android/debug.keystore -list -v
Enter keystore password:  android

...

SHA-1
9E:BC:4F:FD:EC:6A:30:E2:5D:35:8A:93:51:14:0C:5D:24:07:07:9C
```

> - ##### sign verify
```  
$ keytool -printcert -jarfile aa.apk  
```

> - ##### adb remote
https://developer.android.com/studio/command-line/logcat?hl=ko

```  
ANDROID_SDK_ROOT= ~/Library/Android/sdk
PATH=$PATH:$ANDROID_SDK_ROOT/platform-tools/

$ adb shell ip -f inet addr show wlan0
$ adb tcpip 5555

$ adb connect 172.30.1.24
$ adb devices
List of devices attached
172.30.1.24:5555	device

$ adb -s 172.30.1.24 logcat -c   
$ adb -s 172.30.1.24 logcat --pid 2401 -b all -v color

$ adb shell ps | grep shotglass
u0_a177       1096  3439 2113008 264548 0                   0 S com.hunee.shotglass

$ adb logcat -c   
$ adb logcat --pid 2401 -v color | grep Unity


```
