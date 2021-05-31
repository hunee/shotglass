--- Shot glass

com.hunee.shotglass

-- GooglePlayGames
234033842797-9921q0jsh68obdqa5bqhu3d7viq4vahl.apps.googleusercontent.com
PBQxVyB2HaXoYj3utzJHcznf

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


-- Android
GoogleMobileAds
GooglePlayGames

Firebase.Analytics
Firebase.Auth
Firebase.Messaging
Firebase.RemoteConfig


-- MAC
$vi ~/.bash_profile
export ANDROID_HOME=/Library/Android/android-sdk-macosx
export ANDROID_SDK_ROOT=/Library/Android/android-sdk-macosx
export NDK_ROOT=/Library/Android/android-ndk-r8e

export PATH=${PATH}:${ANDROID_SDK_ROOT}\tools:${ANDROID_SDK_ROOT}\platform-tools:${NDK_ROOT}

$vi ~/.zshrc
source ~/.bash_profile


-- debug keystore
keytool -keystore ~/.android/debug.keystore -list -v
Enter keystore password:  android

SHA-1
9E:BC:4F:FD:EC:6A:30:E2:5D:35:8A:93:51:14:0C:5D:24:07:07:9C


-- sign verify
keytool -printcert -jarfile aa.apk  

-- GoogleMobileAds
sample ID: ca-app-pub-3940256099942544~3347511713
배너 광고 ID: ca-app-pub-3940256099942544/6300978111
리워드 광고 ID: ca-app-pub-3940256099942544/5224354917
 
-- adb remote
ANDROID_SDK_ROOT= ~/Library/Android/sdk
PATH=$PATH:$ANDROID_SDK_ROOT/platform-tools/

adb shell ip -f inet addr show wlan0
adb tcpip 5555

adb connect 172.30.1.24
adb devices
