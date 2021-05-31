# Shot glass

*single asterisks*
_single underscores_
**double asterisks**
__double underscores__
~~cancelline~~




> ### [Google Play Console][play.google.com/console/developers]
[play.google.com/console/developers]: https://play.google.com/console/developers
>> - ##### [play-games-plugin-for-unity][github.com/playgameservices/play-games-plugin-for-unity/releases]
[github.com/playgameservices/play-games-plugin-for-unity/releases]: https://github.com/playgameservices/play-games-plugin-for-unity/releases
>>>> ###### https://github.com/playgameservices/play-games-plugin-for-unity/tree/master/current-build
>> - ##### Unity3D 2021 SDK not found.
>>>> ###### Assets/GooglePlayGames/Editor/GPGSUtil.cs
```C#
        public static string GetAndroidSdkPath()
        {
            string sdkPath = EditorPrefs.GetString("AndroidSdkRoot");
448: #if UNITY_2019 || UNITY_2020 || UNITY_2021
            ...
```



> ### [Google AdMob][apps.admob.com/v2/home]

[apps.admob.com/v2/home]: https://apps.admob.com/v2/home
>> - ##### [Google Mobile Ads Unity Plugin v6.0.0][github.com/googleads/googleads-mobile-unity/releases/tag/v6.0.0]

[github.com/googleads/googleads-mobile-unity/releases/tag/v6.0.0]: https://github.com/googleads/googleads-mobile-unity/releases/tag/v6.0.0

>> - ##### adUnitId
```
sample ID: ca-app-pub-3940256099942544~3347511713
배너 광고 ID: ca-app-pub-3940256099942544/6300978111
보상형 광고 ID: ca-app-pub-3940256099942544/5224354917
```


>> - ##### [배너 광고  |  Unity  |  Google Developers][developers.google.com/admob/unity/banner]

[developers.google.com/admob/unity/banner]: https://developers.google.com/admob/unity/banner?hl=ko


>> - ##### [보상형 광고  |  Unity  |  Google Developers][developers.google.com/admob/unity/rewarded]

[developers.google.com/admob/unity/rewarded]: https://developers.google.com/admob/unity/rewarded?hl=ko


> ### [Firebase][firebase.google.com]

[firebase.google.com]: https://console.firebase.google.com/?hl=ko


>> - ##### [Firebase Unity SDK][firebase.google.com/download/unity]

[firebase.google.com/download/unity]:https://firebase.google.com/download/unity?authuser=0

>> - ##### [Unity 프로젝트에 Firebase 추가][firebase.google.com/docs/unity/setup]

[firebase.google.com/docs/unity/setup]: https://firebase.google.com/docs/unity/setup?hl=ko

>> - ##### Firebase.Analytics

>> - ##### Firebase.Auth

>>> - ###### [Unity를 사용하여 비밀번호 기반 계정으로 Firebase에 인증][firebase.google.com/docs/auth/unity/password-auth]

[firebase.google.com/docs/auth/unity/password-auth]: https://firebase.google.com/docs/auth/unity/password-auth?authuser=0

>>> - ###### [Unity에서 Google Play 게임 서비스를 사용하여 인증  |  Firebase][firebase.google.com/docs/auth/unity/play-games]

[firebase.google.com/docs/auth/unity/play-games]: https://firebase.google.com/docs/auth/unity/play-games?hl=ko

>>> - ###### Firebase.Messaging

>>> - ###### Firebase.RemoteConfig





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
```  
ANDROID_SDK_ROOT= ~/Library/Android/sdk
PATH=$PATH:$ANDROID_SDK_ROOT/platform-tools/

  $ adb shell ip -f inet addr show wlan0
  $ adb tcpip 5555

  $ adb connect 172.30.1.24
  $ adb devices
```
